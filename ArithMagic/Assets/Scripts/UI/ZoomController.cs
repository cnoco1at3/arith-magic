using UnityEngine;
using System;
using InteractLib;
using DG.Tweening;

// TODO: The level control for each part might (or might not) needs to be implemented here
public class ZoomController : MonoBehaviour, IInteractable {

    [SerializeField]
    private Transform target_;
    [SerializeField]
    private string trigger_;

    private static Animator anim;

    // NOTE: Some constants that need to be filled in correctly
    private const string kCameraEase = "CameraEase";
    private const string kAnimObj = "Robot 1";

    // Find the animator here
    void Start() {
        if (anim == null)
            try {
                anim = GameObject.Find(kAnimObj).GetComponent<Animator>();
            }
            catch (Exception e) {
                Debug.LogException(e);
            }
    }

    // NOTE: When a touch start, we need to do
    // 1. start moving camera to target position
    // 2. trigger correct animation
    public void OnTouchEnter(Vector3 touch_pos) {
        Camera.main.transform.DOMove(target_.position, (float)ConstantTweakTool.Instance.const_dict[kCameraEase]);
        anim.SetTrigger(trigger_);
        if (trigger_ == "Reverse") {
            FoldMenu.Instance.OnUIFold();
        }
        else {
            FoldMenu.Instance.OnUIUnfold();
        }
    }

    // Useless
    public void OnTouchStay(Vector3 touch_pos) { }
    public void OnTouchExit(Vector3 touch_pos) { }
}