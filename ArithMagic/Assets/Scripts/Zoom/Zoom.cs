using UnityEngine;
using System;
using Util;
using InteractLib;
using DG.Tweening;

public class Zoom : MonoBehaviour, IInteractable {

    /*
    public GameObject gameCamera;
    public bool zoom = false;
    public bool zoomHead = false;
    public bool zoomChest = false;
    public bool zoomLeftHand = false;
    public bool zoomBackground = false;
    public float zoomZPosition = 0;
    */

    [SerializeField]
    private Transform target_;
    [SerializeField]
    private string trigger_;

    private static Animator anim;

    private const string kCameraEase = "CameraEase";
    private const string kAnimObj = "ff2";

    // Use this for initialization
    void Start() {
        if (anim == null)
            try {
                anim = GameObject.Find(kAnimObj).GetComponent<Animator>();
            }
            catch (Exception e) {
                Debug.LogException(e);
            }
    }

    // Update is called once per frame
    void Update() {
        /*

        if (zoomHead) {
            Debug.Log("zoomhead");
            if (gameCamera.transform.position.z < -3.6f && zoomZPosition < 9f) {
                gameCamera.transform.position += new Vector3(-1, 2.5f, zoomZPosition) * Time.deltaTime;
                zoomZPosition = zoomZPosition + 0.1f;
            }
            else {
                anim.SetTrigger("HeadTrigger");
                gameCamera.transform.position += new Vector3(-1, 2.5f, -3.6f) * Time.deltaTime;
                zoomHead = false;
                zoomZPosition = 0;
            }
        }
        else if (zoomChest) {
            Debug.Log("zoomchest");
            if (gameCamera.transform.position.z < -4.8f && zoomZPosition < 8.2f) {
                gameCamera.transform.position += new Vector3(-1.6f, -0.81f, zoomZPosition) * Time.deltaTime;
                zoomZPosition = zoomZPosition + 0.1f;
            }
            else {
                anim.SetTrigger("ChestTrigger");
                gameCamera.transform.position += new Vector3(-1.6f, -0.81f, -4.8f) * Time.deltaTime;
                zoomChest = false;
                zoomZPosition = 0;
            }
        }
        else if (zoomLeftHand) {
            if (gameCamera.transform.position.z < -2f && zoomZPosition < 10f) {
                gameCamera.transform.position += new Vector3(-3f, 0, zoomZPosition) * Time.deltaTime;
                zoomZPosition = zoomZPosition + 0.1f;
            }
            else {
                anim.SetTrigger("LeftHandTrigger");
                gameCamera.transform.position += new Vector3(-3f, 0, -2f) * Time.deltaTime;
                zoomLeftHand = false;
                zoomZPosition = 0;
            }
        }
        else if (zoomBackground) {
            anim.SetTrigger("Reverse");
            gameCamera.transform.position = new Vector3(0, 0, -10);
            zoomBackground = false;
        }
        */
    }

    // NOTE: here we need to do
    // 1. start moving camera to target position
    // 2. trigger correct animation
    public void OnTouchEnter() {
        /*
        Debug.Log(gameObject.tag);
        if (gameObject.tag == "Head") {
            zoomHead = true;
        }
        else if (gameObject.tag == "Chest") {
            zoomChest = true;
        }
        else if (gameObject.tag == "LeftHand") {
            zoomLeftHand = true;
        }
        else if (gameObject.tag == "Background") {
            zoomBackground = true;
        }
        */
        Camera.main.transform.DOMove(target_.position, (float)ConstantTweakTool.Instance.const_dict[kCameraEase]);
        anim.SetTrigger(trigger_);
    }

    public void OnTouchStay(Vector3 touch_pos) { }

    public void OnTouchExit() { }
}