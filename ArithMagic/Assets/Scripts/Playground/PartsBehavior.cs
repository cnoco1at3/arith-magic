using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Util;
using InteractLib;
using DG.Tweening;

public class PartsBehavior : Dragable {

    public int part_id = 0;

    // NOTE: Add these constants into tweak tool
    private const string kRotateEaseIn = "RotateEaseIn";
    private const string kSnapEaseIn = "SnapEaseIn";
    private const string kSnapEaseOut = "SnapEaseOut";

    private Vector3 from_pos_;

    private bool is_accepted_ = false;
    private PartsAcceptor curr_ = null;

    // Use this for initialization
    void Start() {
        from_pos_ = transform.position;
    }

    // Lazy update of the from position
    public override void OnTouchEnter(Vector3 touch_pos) {
        from_pos_ = transform.position;
        transform.DORotate(new Vector3(90, 0, 0), (float)ConstantTweakTool.Instance.const_dict[kRotateEaseIn]);
        /*
        transform.DOMoveZ(Camera.main.transform.position.z + 1.5f,
            (float)ConstantTweakTool.Instance.const_dict[kSnapEaseOut]);
            */
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

        // NOTE: this is the old method where it will find the closet acceptor and see whether the dist(acceptor, part) is
        //       within tolerance.
        /*
        PartsAcceptor[] acceptors = FindObjectsOfType<PartsAcceptor>();
        PartsAcceptor nearest = null;
        float nearest_dist = Mathf.Infinity;
        foreach (PartsAcceptor acceptor in acceptors) {
            Vector2 pos2d = new Vector2(transform.position.x, transform.position.y);
            Vector2 acc2d = new Vector2(acceptor.accept_point.position.x, acceptor.accept_point.position.y);
            float dist = Vector2.Distance(pos2d, acc2d);
            if (dist < nearest_dist) {
                nearest_dist = dist;
                nearest = acceptor;
            }
        }
        */

        try {
            // case 1 find a valid acceptor
            if (acceptor != null && !acceptor.IsOccupied()) {
                transform.DOMove(acceptor.GetAcceptPoint(transform.position), (float)ConstantTweakTool.Instance.const_dict[kSnapEaseIn]);
                is_accepted_ = true;

                // occupy this acceptor
                if (curr_ != null)
                    curr_.OnPartExit(this);
                curr_ = acceptor;
                curr_.OnPartEnter(this);
            }
            // case 2 invalid or did not find any acceptor
            else {
                transform.DOMove(from_pos_, (float)ConstantTweakTool.Instance.const_dict[kSnapEaseOut]);
                if (!is_accepted_)
                    StartCoroutine(DelayDestroy());
            }
        }
        catch (KeyNotFoundException) {
            ScriptDebug.Log(this, 73, "Could not found constant for snap, did you add them to ConstantTweakTool?");
        }
    }

    // TODO: check if it's the right answer
    private void CheckAnswer(PartsAcceptor acc) {
        if (IsCorrectAnswer(acc)) {
            // do it
        }
        else {
            // do something else
        }
    }

    // TODO: is the answer correct?
    private bool IsCorrectAnswer(PartsAcceptor acc) {
        return true;
    }

    private IEnumerator DelayDestroy() {
        yield return new WaitForSeconds((float)ConstantTweakTool.Instance.const_dict[kSnapEaseOut]);
        Destroy(gameObject);
    }
}
