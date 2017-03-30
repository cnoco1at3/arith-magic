using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using SoundLib;

public class LockBoxBehavior : Clickable {

    [SerializeField]
    private Vector3 target_pos_;

    [SerializeField]
    private GameObject robot_pic_;

    private bool unlocked_ = false;

    private int id_ = -1;

    public AudioClip touchBoxSound;

    public override void ClickEvent() {
        SoundManager.Instance.PlaySFX(touchBoxSound, false);
        if (unlocked_)
            MoveRobot();
    }

    public void SetAnimation(bool flag) {
        try {
            Animator anim = GetComponent<Animator>();
            if (flag) {
                anim.SetTrigger("BoxAnim");
                anim.ResetTrigger("Null");
            } else {
                anim.SetTrigger("Null");
                anim.ResetTrigger("BoxAnim");
            }
        } catch (Exception) { }
    }

    public void SetUnlocked() {
        GetComponent<SpriteRenderer>().enabled = false;
        if (robot_pic_ != null) {
            GameObject robot = Instantiate(robot_pic_, transform);
            robot.transform.localPosition = Vector3.zero;
            robot.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
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
