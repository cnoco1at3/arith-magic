using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Util;
using InteractLib;
using DG.Tweening;
using SoundLib;

public class PartsBehavior : Dragable {

    public int part_id = 0;
    [HideInInspector]
    public bool is_accepted_ = false;
    [HideInInspector]
    public PartsAcceptor curr_acceptor_ = null;

    // NOTE: Add these constants into tweak tool
    private const string kRotateEaseIn = "RotateEaseIn";
    private const string kSnapEaseIn = "SnapEaseIn";
    private const string kSnapEaseOut = "SnapEaseOut";

    private Vector3 default_pos_;
    private Vector3 default_scale_;

    // Use this for initialization
    void Start() {
        default_pos_ = transform.localPosition;
        default_scale_ = transform.localScale;
    }

    public void MoveBackToBox() {
        /*
        transform.DOLocalMove(default_pos_,
            (float)ConstantTweakTool.Instance.const_dict[kSnapEaseIn]);
        transform.DOScale(default_scale_,
            (float)ConstantTweakTool.Instance.const_dict[kSnapEaseIn]);
        is_accepted_ = false;
        */
        Destroy(gameObject);
    }

    // NOTE: this check is seperated into two steps
    // 1. check if there is anything it could snap on, if so, snap on it, otherwise, return to where it from
    // 2. TODO check if this is a correct solution for the problem
    public override void OnTouchExit(Vector3 touch_pos) {
        // NOTE: this is the new method where it would directly check if the acceptor could be hit by ray from camera
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(touch_pos));

        PartsAcceptor acceptor = null;
        for (int i = 0; i < hits.Length && acceptor == null; ++i)
            acceptor = hits[i].collider.GetComponent<PartsAcceptor>();

        try {
            // case 1 find a valid acceptor
            if (acceptor != null) {
                is_accepted_ = true;

                // occupy this acceptor
                if (curr_acceptor_ != null)
                    curr_acceptor_.OnPartExit(this);
                curr_acceptor_ = acceptor;
                curr_acceptor_.OnPartEnter(this);
            }
            // case 2 invalid or did not find any acceptor

            // update scale and position
            UpdateStatus(curr_acceptor_, is_accepted_);
        } catch (KeyNotFoundException) {
            ScriptDebug.Log(this, 65, "Could not found constant for snap, did you add them to ConstantTweakTool?");
        }
    }

    private void UpdateStatus(PartsAcceptor acceptor, bool accepted) {
        if (accepted)
            transform.DOMove(acceptor.GetAcceptPoint(),
                (float)ConstantTweakTool.Instance.const_dict[kSnapEaseIn]);
        else
            Destroy(gameObject);
        /*
        transform.DOLocalMove(default_pos_,
            (float)ConstantTweakTool.Instance.const_dict[kSnapEaseIn]);
            */

        transform.DOScale(accepted ? acceptor.GetScaleFactor() : default_scale_,
            (float)ConstantTweakTool.Instance.const_dict[kSnapEaseIn]);
    }
}
