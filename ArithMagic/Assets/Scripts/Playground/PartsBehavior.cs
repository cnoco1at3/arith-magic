using UnityEngine;
using System;
using System.Collections.Generic;
using InteractLib;
using DG.Tweening;

public class PartsBehavior : Dragable {

    // NOTE: ADD THESE TWO CONSTANT INTO CONSTANT TWEAK TOOL
    private const string kSnapDistance = "SnapDist";
    private const string kSnapEaseIn = "SnapEaseIn";
    private const string kSnapEaseOut = "SnapEaseOut";

    private Vector3 from_pos;

    // Use this for initialization
    void Start() {
        from_pos = transform.position;
    }

    // Update is called once per frame
    void Update() {

    }

    public override void OnTouchEnter() {
        from_pos = transform.position;
    }

    public override void OnTouchStay(Vector3 touch_pos) {
        base.OnTouchStay(touch_pos);
    }

    public override void OnTouchExit() {
        PartsAcceptor[] acceptors = FindObjectsOfType<PartsAcceptor>();
        PartsAcceptor nearest = null;
        float nearest_dist = Mathf.Infinity;
        foreach (PartsAcceptor acceptor in acceptors) {
            float dist = Vector3.Distance(transform.position, acceptor.accept_point.position);
            if (dist < nearest_dist) {
                nearest_dist = dist;
                nearest = acceptor;
            }
        }

        try {
            if (nearest_dist < ConstantTweakTool.Instance.const_dict[kSnapDistance] && nearest != null) {
                transform.DOMove(nearest.accept_point.position, (float)ConstantTweakTool.Instance.const_dict[kSnapEaseIn]);
                CheckAnswer();
            }
            else {
                transform.DOMove(from_pos, (float)ConstantTweakTool.Instance.const_dict[kSnapEaseOut]);
            }
        }
        catch (KeyNotFoundException) {
            Debug.LogError("Could not found constant for snap, did you add them to ConstantTweakTool?");
        }
    }

    // TODO
    private void CheckAnswer() {
        if (IsCorrectAnswer()) {
            // do it
        }
        else {
            // do something else
        }
    }

    // TODO
    private bool IsCorrectAnswer() {
        return true;
    }
}
