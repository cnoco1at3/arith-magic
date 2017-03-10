using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;

public class ScrewContainer : Clickable {

    [SerializeField]
    private int width_ = 5;
    [SerializeField]
    private int height_ = 2;

    [SerializeField]
    private const float scale_factor_ = 0.5f;

    private ScrewBehaviour[] buckets_;

    private int slot_index_ = -1;

    // NOTE: We got two options here
    // 1. Align all the slots manually
    // 2. Procedurally align them

    void Start() {
        buckets_ = new ScrewBehaviour[width_ * height_];
    }

    public bool IsFull() {
        return slot_index_ == width_ * height_ - 1;
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
        return new Vector3((next_index % width_) * scale_factor_, (next_index / width_) * scale_factor_);
    }

    public override void ClickEvent() {
        ScrewBehaviour tmp = ReleaseSlot();
        tmp.ReturnFromContainer();
    }

    private ScrewBehaviour ReleaseSlot() {
        if (IsEmpty())
            return null;
        ScrewBehaviour tmp = buckets_[slot_index_];
        buckets_[slot_index_--] = null;
        return tmp;
    }
}
