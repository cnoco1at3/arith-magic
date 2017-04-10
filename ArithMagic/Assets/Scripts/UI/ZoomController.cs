using UnityEngine;
using System;
using System.Collections.Generic;
using InteractLib;
using DG.Tweening;

// TODO: The level control for each part might (or might not) needs to be implemented here
public class ZoomController : MonoBehaviour, IInteractable {
    public List<GameObject> trigger_obj_;

    [SerializeField]
    private bool reverse_ = false;

    [SerializeField]
    private Transform anim_obj_;

    [SerializeField]
    private Vector3 move_dist_;

    [SerializeField]
    private Transform target_;

    private GameObject scene_;

    private static ZoomController current_;

    // NOTE: Some constants that need to be filled in correctly
    private const string kCameraEase = "CameraEase";
    private const string kAnimObj = "SceneObjects";

    // Find the animator here
    void Start() {
        if (target_ == null)
            target_ = transform;
        try {
            scene_ = GameObject.Find(kAnimObj);
            current_ = GameObject.Find("Background").GetComponent<ZoomController>();
        } catch (Exception e) {
            Debug.LogException(e);
        }
        if (reverse_)
            anim_obj_.transform.localPosition = move_dist_;
        //else
        //foreach (GameObject trigger in trigger_obj_)
        //trigger.SetActive(false);
    }

    // NOTE: When a touch start, we need to do
    // 1. start moving camera to target position
    // 2. trigger correct animation
    public void OnTouchEnter(Vector3 touch_pos) {
        Vector3 pos = new Vector3(target_.position.x, target_.position.y);
        Camera.main.transform.DOMove(pos, (float)ConstantTweakTool.Instance[kCameraEase]);
        scene_.transform.DOScale(target_.localScale,
            (float)ConstantTweakTool.Instance[kCameraEase]);

        // NOTE: here might bring problem when the sequence of transition in and out animation is important
        if (current_ != this) {
            current_.OnTransitOut();
            current_ = this;
            current_.OnTransitIn();
        }
    }

    // Useless
    public void OnTouchStay(Vector3 touch_pos) { }
    public void OnTouchExit(Vector3 touch_pos) { }

    public virtual void OnTransitIn() {
        if (anim_obj_ != null)
            anim_obj_.DOLocalMove(move_dist_, (float)ConstantTweakTool.Instance[kCameraEase]);
        if (trigger_obj_ != null)
            foreach (GameObject trigger in trigger_obj_)
                trigger.SetActive(true);
    }

    public virtual void OnTransitOut() {
        if (anim_obj_ != null)
            anim_obj_.DOLocalMove(Vector3.zero, (float)ConstantTweakTool.Instance[kCameraEase]);
        if (trigger_obj_ != null)
            foreach (GameObject trigger in trigger_obj_)
                trigger.SetActive(false);
    }
}