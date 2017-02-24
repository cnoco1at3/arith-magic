using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Util;
using InteractLib;
using DG.Tweening;

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
    private const int default_layer_ = 102;

    private Vector3 default_pos_;
    private SpriteRenderer spr_ = null;
    private static GameObject scene_;

    // Use this for initialization
    void Start() {
        default_pos_ = transform.position;
        spr_ = GetComponent<SpriteRenderer>();
        scene_ = GameObject.Find("SceneObjects");
    }

    // Lazy update of the from position
    public override void OnTouchEnter(Vector3 touch_pos) {
        if (spr_ != null)
            spr_.sortingOrder = default_layer_;
    }

    // OnTouchStay is inherented from Draggable

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
            if (acceptor != null && acceptor.IsValid(this)) {
                transform.parent = scene_.transform;
                default_pos_ = acceptor.transform.position;
                is_accepted_ = true;

                // occupy this acceptor
                if (curr_acceptor_ != null)
                    curr_acceptor_.OnPartExit(this);
                curr_acceptor_ = acceptor;
                curr_acceptor_.OnPartEnter(this);
                UpdateStatus(curr_acceptor_, true);
            }
            // case 2 invalid or did not find any acceptor
            else {
                UpdateStatus(curr_acceptor_, false);

                if (!is_accepted_)
                    StartCoroutine(DelayDestroy());
            }
        }
        catch (KeyNotFoundException) {
            ScriptDebug.Log(this, 73, "Could not found constant for snap, did you add them to ConstantTweakTool?");
        }
    }

    private IEnumerator DelayDestroy() {
        yield return new WaitForSeconds((float)ConstantTweakTool.Instance.const_dict[kSnapEaseIn]);
        Destroy(gameObject);
    }

    private void UpdateStatus(PartsAcceptor acceptor, bool accepted) {
        if (spr_ != null)
            spr_.sortingOrder = acceptor != null ? acceptor.GetLayerOrder() : default_layer_;
        transform.DOMove(accepted ? acceptor.GetAcceptPoint(transform.position) : default_pos_,
            (float)ConstantTweakTool.Instance.const_dict[kSnapEaseIn]);
    }
}
