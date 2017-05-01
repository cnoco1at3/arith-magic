using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using DG.Tweening;

public class ScrewContainer : Clickable {

    #region variables
    [SerializeField]
    private Transform[] slots_;

    [SerializeField]
    private ScrewCarrier carrier_;

    [SerializeField]
    private GameObject glow_;

    private GenericScrewBehavior[] buckets_;

    private int slot_index_ = -1;
    #endregion


    #region attributes
    public bool IsFull {
        get {
            return slot_index_ == slots_.Length - 1;
        }
        private set { }
    }

    public bool IsEmpty {
        get {
            return slot_index_ == -1;
        }
        private set { }
    }
    #endregion


    void Start() {
        buckets_ = new GenericScrewBehavior[slots_.Length];
    }

    public int ObtainSlot(GenericScrewBehavior screw) {
        if (IsFull)
            return -1;
        buckets_[++slot_index_] = screw;

        if (IsFull && ToolBoxBehavior.Instance.GetNextContainer(this) != null)
            glow_.SetActive(true);
        return slot_index_;
    }

    public GenericScrewBehavior ReleaseSlot() {
        if (IsEmpty)
            return null;
        GenericScrewBehavior tmp = buckets_[slot_index_];
        buckets_[slot_index_--] = null;
        return tmp;
    }

    public Vector3 GetNextSlotPosition() {
        int next_index = slot_index_ + 1;
        return next_index >= slots_.Length ? Vector3.zero : slots_[next_index].position;
    }

    public Vector3 GetLastSlotPosition() {
        return slots_[slot_index_].position;
    }

    public void ClearSlots() {
        for (int i = 0; i < buckets_.Length; ++i) {
            if (buckets_[i] != null)
                buckets_[i].LatentDestroy();
            buckets_[i] = null;
        }
        glow_.SetActive(false);
        slot_index_ = -1;
    }

    public override void ClickEvent() {
        if (IsFull && GameController.add) {
            glow_.SetActive(false);
            RegroupTo();
        } else if (!GameController.add) {
            glow_.SetActive(false);
            BorrowTo();
        }
    }


    private void RegroupTo() {

        ScrewContainer next_container = ToolBoxBehavior.Instance.GetNextContainer(this);

        if (next_container == null)
            return;
        if (next_container.IsFull)
            return;

        Vector3 next_pos = next_container.GetNextSlotPosition();

        for (int i = 0; i <= slot_index_; ++i)
            buckets_[i].transform.DOMove(next_pos, 0.5f);

        slot_index_ = -1;
        StartCoroutine(RegroupAnim(next_container));
    }

    private void BorrowTo() {

        if (carrier_ == null)
            return;
        if (!carrier_.IsEmpty)
            return;

        GenericScrewBehavior last = ReleaseSlot();
        carrier_.ObtainSlot(last);
        // ToolBoxBehavior.Instance.BorrowSpawn();
        last.transform.DOMove(carrier_.GetSlotPosition(), 0.5f);

    }

    private IEnumerator RegroupAnim(ScrewContainer next_container) {
        InteractManager.LockInteraction();

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i <= slot_index_; ++i) {
            buckets_[i].LatentDestroy();
            buckets_[i] = null;
        }
        ClearSlots();

        GameObject next_screw =
            Instantiate(ToolBoxBehavior.Instance.GetScrewByContainer(next_container),
            next_container.GetNextSlotPosition(), Quaternion.identity, transform.root);
        GenericScrewBehavior next_behavior = next_screw.GetComponent<GenericScrewBehavior>();
        Collider next_collider = next_screw.GetComponent<Collider>();
        next_collider.enabled = false;

        next_container.ObtainSlot(next_behavior);
        InteractManager.ReleaseInteraction();
    }
}
