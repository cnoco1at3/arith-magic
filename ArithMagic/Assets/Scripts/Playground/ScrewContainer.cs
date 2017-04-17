using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using DG.Tweening;

public class ScrewContainer : Clickable {

    #region variables
    [SerializeField]
    private Transform[] slots_;

    private GenericScrewBehavior[] buckets_;

    private int slot_index_ = -1;
    #endregion


    #region attributes
    public bool is_full {
        get {
            return slot_index_ == slots_.Length - 1;
        }
        private set { }
    }

    public bool is_empty {
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
        if (is_full)
            return -1;
        buckets_[++slot_index_] = screw;
        return slot_index_;
    }

    public GenericScrewBehavior ReleaseSlot() {
        if (is_empty)
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
                Destroy(buckets_[i].gameObject);
            buckets_[i] = null;
        }
        slot_index_ = -1;
    }

    public override void ClickEvent() {
        if (is_full)
            RegroupToNext();
        else
            BorrowToPrev();
    }

    public void SetWiggleBuckets(bool wiggle) {
        for (int i = 0; i <= slot_index_; ++i)
            buckets_[i].SetWiggle(wiggle);
    }

    public void SetWiggleLastBucket(bool wiggle) {
        buckets_[slot_index_].SetWiggle(wiggle);
    }

    private void RegroupToNext() {
        SetWiggleBuckets(false);

        ScrewContainer next_container = ToolBoxBehavior.Instance.GetNextContainer(this);

        if (next_container == null)
            return;
        if (next_container.is_full)
            return;

        Vector3 next_pos = next_container.GetNextSlotPosition();

        for (int i = 0; i <= slot_index_; ++i)
            buckets_[i].transform.DOMove(next_pos, 0.5f);

        slot_index_ = -1;
        StartCoroutine(RegroupAnim(next_container));
    }

    private void BorrowToPrev() {
        ScrewContainer prev_container = ToolBoxBehavior.Instance.GetPrevContainer(this);

        if (prev_container == null)
            return;
        if (!prev_container.is_empty)
            return;

        for (int i = 0; i < slots_.Length; ++i) {

            GameObject prev_screw = Instantiate(ToolBoxBehavior.Instance.GetScrewByContainer(prev_container),
                buckets_[slot_index_].transform.position, Quaternion.identity, transform.root);
            prev_screw.GetComponent<Collider>().enabled = false;
            GenericScrewBehavior prev_behavior = prev_screw.GetComponent<GenericScrewBehavior>();

            prev_screw.transform.DOMove(prev_container.GetNextSlotPosition(), 0.5f);
            prev_container.ObtainSlot(prev_behavior);
        }

        StartCoroutine(BorrowAnim(prev_container));
    }


    private IEnumerator BorrowAnim(ScrewContainer prev_container) {
        InteractManager.LockInteraction();

        GenericScrewBehavior tmp = ReleaseSlot();
        Destroy(tmp.gameObject);
        yield return new WaitForSeconds(0.5f);

        InteractManager.ReleaseInteraction();
    }

    private IEnumerator RegroupAnim(ScrewContainer next_container) {
        InteractManager.LockInteraction();

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i <= slot_index_; ++i) {
            Destroy(buckets_[i].gameObject);
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
