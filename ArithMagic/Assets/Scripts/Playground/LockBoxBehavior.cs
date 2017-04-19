using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoundLib;

public class LockBoxBehavior : ProfileButton, IComparable {

    private static Vector3 target_pos_ = new Vector3(0.0f, 180.0f);

    private int id_ = -1;

    private Button button_;

    public int CompareTo(object obj) {
        if (obj.GetType() != typeof(LockBoxBehavior))
            return -1;
        LockBoxBehavior other = (LockBoxBehavior)obj;
        return other.transform.position.y.CompareTo(transform.position.y);
    }

    public override void ClickEvent() {
        SoundManager.Instance.PlaySFX(LockBoxSingleton.Instance.touch_box, false);
        MapRobotBehavior.Instance.MoveToPosition(this);
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
        try {
            button_.interactable = true;
        } catch (NullReferenceException) {
            button_ = GetComponent<Button>();
            button_.interactable = true;
        }
    }

    public void SetLockBoxId(int id) { id_ = id; }

    public int GetLockBoxId() { return id_; }

    public Vector3 GetTargetLocalPosition() {
        return transform.localPosition + target_pos_;
    }
}
