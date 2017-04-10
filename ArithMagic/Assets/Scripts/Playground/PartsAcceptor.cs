using UnityEngine;
using System;
using System.Collections;
using SoundLib;

public class PartsAcceptor : MonoBehaviour {

    public bool active = true;

    [SerializeField]
    private int acc_part_id = 0;

    public AudioClip dropBatterySound;
    public AudioClip rightAnswerSound;

    // NOTE: here we need to set the point where we want the screw snap to
    [SerializeField]
    private Transform accept_point_;

    private PartsBehavior pb;

    private bool is_occupied = false;

    void Start() {
        if (accept_point_ == null)
            accept_point_ = transform;
    }

    public virtual void ClearSlot() {
        if (pb != null)
            Destroy(pb.gameObject);
        pb = null;
    }

    public virtual void SetAccPartId(int id) {
        acc_part_id = id;
    }

    // NOTE: if we didn't define a certain position we want the screw move to, it will stay where it is
    public virtual Vector3 GetAcceptPoint(Vector3 position = default(Vector3)) {
        return accept_point_.position;
    }

    public virtual Vector3 GetScaleFactor() {
        return new Vector3(0.5f, 0.5f, 0.5f);
    }

    public virtual bool IsFinished() {
        return is_occupied;
    }

    public virtual void OnPartEnter(PartsBehavior part) {
        SoundManager.Instance.PlaySFX(dropBatterySound, false);
        is_occupied = true;

        if (pb != null)
            pb.MoveBackToBox();
        pb = part;

        ToolBoxBehavior.Instance.CheckActivateStatus();
    }

    public virtual void OnPartExit(PartsBehavior part) {
        is_occupied = false;
        pb = null;
        ToolBoxBehavior.Instance.CheckActivateStatus();
    }

    public bool IsOccupied() {
        return is_occupied;
    }

    public bool IsSolved() {
        try {
            if (pb != null)
                return pb.part_id == acc_part_id;
        } catch (Exception e) {
            Debug.LogException(e);
            return false;
        }
        return false;
    }

}
