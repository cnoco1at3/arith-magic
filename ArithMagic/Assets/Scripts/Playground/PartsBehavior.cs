using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Util;
using InteractLib;
using DG.Tweening;

public class PartsBehavior : Dragable {

    // NOTE: ADD THESE TWO CONSTANT INTO CONSTANT TWEAK TOOL
    private const string kSnapDistance = "SnapDist";
    private const string kSnapEaseIn = "SnapEaseIn";
    private const string kSnapEaseOut = "SnapEaseOut";

    private Vector3 from_pos_;

<<<<<<< HEAD
    // Use this for initialization
    void Start() {
        from_pos = transform.position;
        
    }
=======
    private bool is_accepted_ = false;
    private PartsAcceptor curr = null;
>>>>>>> d25218b33e2acddc2c8f174bcfc3cf3ede8e4111

    [SerializeField]
    private int part_id_ = 0;

    // Use this for initialization
    void Start() {
        from_pos_ = transform.position;
    }

    // Lazy update of the from position
    public override void OnTouchEnter() {
<<<<<<< HEAD
        from_pos = transform.position;
        
=======
        from_pos_ = transform.position;
>>>>>>> d25218b33e2acddc2c8f174bcfc3cf3ede8e4111
    }

    public override void OnTouchStay(Vector3 touch_pos) {
        base.OnTouchStay(touch_pos);
    }

    // NOTE: this check is seperated into two steps
    // 1. check if there is anything it could snap on, if so, snap on it, otherwise, return to where it from
    // 2. TODO check if this is a correct solution for the problem
    public override void OnTouchExit() {
        PartsAcceptor[] acceptors = FindObjectsOfType<PartsAcceptor>();
        PartsAcceptor nearest = null;
        float nearest_dist = Mathf.Infinity;
        Debug.Log(acceptors[0]);
        foreach (PartsAcceptor acceptor in acceptors) {
            float dist = Vector3.Distance(transform.position, acceptor.accept_point.position);
            if (dist < nearest_dist) {
                nearest_dist = dist;
                nearest = acceptor;
            }
        }

        try {
            // case 1 find a valid acceptor
            if (nearest_dist < ConstantTweakTool.Instance.const_dict[kSnapDistance] && nearest != null && !nearest.IsOccupied()) {
                transform.DOMove(nearest.accept_point.position, (float)ConstantTweakTool.Instance.const_dict[kSnapEaseIn]);
                CheckAnswer(nearest);
                is_accepted_ = true;

                // occupy this acceptor
                if (curr != null)
                    curr.OnPartExit(this);
                curr = nearest;
                curr.OnPartEnter(this);
            }
            // case 2 invalid or did not find any acceptor
            else {
                transform.DOMove(from_pos_, (float)ConstantTweakTool.Instance.const_dict[kSnapEaseOut]);
                if (!is_accepted_)
                    StartCoroutine(DelayDestroy());
            }
        }
        catch (KeyNotFoundException) {
            ScriptDebug.Log(this, 70, "Could not found constant for snap, did you add them to ConstantTweakTool?");
        }
    }

    // TODO check if it's the right answer
    private void CheckAnswer(PartsAcceptor acc) {
        if (IsCorrectAnswer(acc)) {
            // do it
        }
        else {
            // do something else
        }
    }

    // TODO is the answer correct?
    private bool IsCorrectAnswer(PartsAcceptor acc) {
        return true;
    }

    private IEnumerator DelayDestroy() {
        yield return new WaitForSeconds((float)ConstantTweakTool.Instance.const_dict[kSnapEaseOut]);
        Destroy(gameObject);
    }
}
