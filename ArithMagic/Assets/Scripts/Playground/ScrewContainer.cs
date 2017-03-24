using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;

public class ScrewContainer : Clickable {

    [SerializeField]
    private Transform[] slots_;

    [SerializeField]
    private const float scale_factor_ = 0.5f;

    [SerializeField]
    private int container_id_;

    private ScrewBehaviour[] buckets_;

    private int slot_index_ = -1;

    // NOTE: We got two options here
    // 1. Align all the slots manually
    // 2. Procedurally align them

    void Start() {
        buckets_ = new ScrewBehaviour[slots_.Length];
    }

    public bool IsFull() {
        return slot_index_ == slots_.Length - 1;
    }

    public bool IsEmpty() {
        return slot_index_ == -1;
    }

    public int ObtainSlot(ScrewBehaviour screw) {
        if (IsFull())
            return -1;
        buckets_[++slot_index_] = screw;
        return slot_index_;
    }

    public Vector3 GetNextSlotPosition() {
        int next_index = slot_index_ + 1;
        return next_index >= slots_.Length ? Vector3.zero : slots_[next_index].position;
    }

    public void ClearSlots() {
        for (int i = 0; i <= slot_index_; ++i)
            buckets_[i] = null;
        slot_index_ = -1;
    }

    public override void ClickEvent() {
        if (IsFull()) {
            Regroup();
        }
    }

    private void Regroup() {
        ScrewContainer next = ToolBoxBehavior.Instance.GetContainerById(container_id_ + 1);
        if (next == null)
            return;

        next.GetNextSlotPosition();
    }

    private ScrewBehaviour ReleaseSlot() {
        if (IsEmpty())
            return null;
        ScrewBehaviour tmp = buckets_[slot_index_];
        buckets_[slot_index_--] = null;
        return tmp;
    }
}
