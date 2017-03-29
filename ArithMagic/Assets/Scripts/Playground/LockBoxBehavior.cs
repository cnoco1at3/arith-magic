using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;

public class LockBoxBehavior : Clickable {

    [SerializeField]
    private Vector3 target_pos_;

    private bool unlocked_ = false;

    private int id_ = -1;

    public override void ClickEvent() {
        if (unlocked_)
            MoveRobot();
    }

    public void SetUnlocked() {
        GetComponent<SpriteRenderer>().enabled = false;
        Transform child = transform.GetChild(0);
        if (child != null)
            child.gameObject.SetActive(true);
        unlocked_ = true;
    }

    public void SetLockBoxId(int id) { id_ = id; }

    public int GetLockBoxId() { return id_; }

    public Vector3 GetTargetLocalPosition() {
        return transform.localPosition + target_pos_;
    }

    private void MoveRobot() {
        GetComponent<Collider>().enabled = false;
        MapRobotBehavior.Instance.MoveToPosition(this);
    }

}
