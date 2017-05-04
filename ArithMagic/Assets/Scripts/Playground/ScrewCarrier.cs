using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using DG.Tweening;

public class ScrewCarrier : Clickable {

    public bool IsEmpty { get; private set; }

    [SerializeField]
    private ScrewContainer container_;

    [SerializeField]
    private Transform slot_;

    private GenericScrewBehavior bucket_;

    private Collider collider_;

    private void Start() {
        IsEmpty = true;
        collider_ = GetComponent<Collider>();
        collider_.enabled = false;
    }

    public void ObtainSlot(GenericScrewBehavior screw) {
        if (!IsEmpty)
            return;
        bucket_ = screw;
        IsEmpty = false;
        collider_.enabled = true;
    }

    public GenericScrewBehavior ReleaseSlot() {
        if (IsEmpty)
            return null;

        IsEmpty = true;
        GenericScrewBehavior tmp = bucket_;
        bucket_ = null;
        if (collider_ != null)
            collider_.enabled = false;
        return tmp;
    }

    public void ClearSlot() {
        GenericScrewBehavior tmp = ReleaseSlot();
        if (tmp != null)
            tmp.LatentDestroy();
    }

    public Vector3 GetSlotPosition() {
        return slot_.position;
    }

    public override void ClickEvent() {

        if (GameController.add)
            AddCarry();
        else if (!GameController.add)
            SubtractCarry();    
    }

    private void SubtractCarry()
    {
        if (IsEmpty)
            return;

        if (!container_.IsEmpty)
            return;

        for (int i = 0; i < 10; ++i)
        {
            GameObject next_screw = Instantiate(ToolBoxBehavior.Instance.GetScrewByContainer(container_),
                slot_.position, Quaternion.identity, transform.root);
            next_screw.GetComponent<Collider>().enabled = false;
            GenericScrewBehavior next_behavior = next_screw.GetComponent<GenericScrewBehavior>();

            next_screw.transform.DOMove(container_.GetNextSlotPosition(), 0.5f);
            container_.ObtainSlot(next_behavior);
        }

        GenericScrewBehavior tmp = ReleaseSlot();
        tmp.LatentDestroy();
    }

    private void AddCarry()
    {
        if (IsEmpty)
            return;

        if (container_.IsFull)
            return;

        GameObject next_screw = Instantiate(ToolBoxBehavior.Instance.GetScrewByContainer(container_),
                slot_.position, Quaternion.identity, transform.root);
        next_screw.GetComponent<Collider>().enabled = false;
        GenericScrewBehavior next_behavior = next_screw.GetComponent<GenericScrewBehavior>();

        StartCoroutine(next_behavior.ClusterAnim(container_.GetNextSlotPosition()));

        //next_screw.transform.DOMove(container_.GetNextSlotPosition(), 0.5f);
        container_.ObtainSlot(next_behavior);

        GenericScrewBehavior tmp = ReleaseSlot();
        tmp.LatentDestroy();

    }

    public ScrewContainer GetContainer() {
        return container_;
    }
}
