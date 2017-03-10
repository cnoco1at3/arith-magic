using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewContainer : MonoBehaviour {

    [SerializeField]
    private int width_ = 5;
    [SerializeField]
    private int height_ = 2;

    [SerializeField]
    private const float scale_factor_ = 0.5f;

    private int slot_index_ = -1;

    // NOTE: We got two options here
    // 1. Align all the slots manually
    // 2. Procedurally align them

    public bool IsFull() {
        return slot_index_ == width_ * height_ - 1;
    }

    public bool IsEmpty() {
        return slot_index_ == -1;
    }

    public int ObtainSlot() {
        slot_index_++;
        return slot_index_ - 1;
    }

    public Vector3 GetNextSlotPosition() {
        int next_index = slot_index_ + 1;
        return new Vector3((next_index % width_) * scale_factor_, (next_index / width_) * scale_factor_);
    }

}
