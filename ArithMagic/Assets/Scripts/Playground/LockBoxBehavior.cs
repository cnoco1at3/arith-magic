﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using SoundLib;

public class LockBoxBehavior : Clickable, IComparable {

    [SerializeField]
    private Vector3 target_pos_;

    [SerializeField]
    private Sprite unlocked_pic_;

    private static Sprite unlocked_pic__;

    private bool unlocked_ = false;

    private int id_ = -1;

    public AudioClip touchBoxSound;
    public AudioClip moveRobotSound;

    void Start() {
        if (unlocked_pic_ != null)
            unlocked_pic__ = unlocked_pic_;
    }

    public int CompareTo(object obj) {
        if (obj.GetType() != typeof(LockBoxBehavior))
            return -1;
        LockBoxBehavior other = (LockBoxBehavior)obj;
        return other.transform.position.y.CompareTo(transform.position.y);
    }

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
        GetComponent<SpriteRenderer>().sprite = unlocked_pic__;
        /*
        if (robot_pic_ != null) {
            GameObject robot = Instantiate(robot_pic_, transform);
            robot.GetComponent<Animator>().enabled = false;
            robot.transform.localPosition = Vector3.zero;
            robot.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        */
        unlocked_ = true;
    }

    public void SetLockBoxId(int id) { id_ = id; }

    public int GetLockBoxId() { return id_; }

    public Vector3 GetTargetLocalPosition() {
        return transform.localPosition + target_pos_;
    }

    private void MoveRobot() {
        //SoundManager.Instance.PlaySFX(moveRobotSound, false);
        GetComponent<Collider>().enabled = false;
        MapRobotBehavior.Instance.MoveToPosition(this);
    }

}
