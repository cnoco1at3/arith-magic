using DG.Tweening;
using SoundLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class XRayCameraBehavior : GenericSingleton<XRayCameraBehavior> {

    public AudioClip sfx_detect;
    public AudioClip sfx_scanner;
    public AudioClip sfx_b_scan;
    public AudioClip sfx_a_scan;

    public AudioClip[] bgm;
    private int bgmTrack = 0; 

    [SerializeField]
    private GameObject back_button_;

    private GameObject robot_;
    private int category_;

    private List<GameObject> parts_;
    private GameObject part_ptr_;

    private float detect_thres_;
    private float detect_time_;

    private GameObject render_mesh_;
    private Animator mesh_animator_;
    private BoxCollider2D collider_;
    private Vector3 scale_factor_;
    private bool is_entered_ = false;

    //Robot VoiceOver
    private RobotVO roboVO;

    public bool is_finished {
        get {
            return parts_.Count == 0;
        }
        private set { }
    }

    public void CheckParts(bool remove) {
        if (remove) {
            parts_.Remove(part_ptr_);
            Destroy(part_ptr_);
            bgmTrack += 1;
            SoundManager.Instance.PlayBGM(bgm[bgmTrack]);
        }

        if (parts_.Count == 0) {
            robot_.GetComponent<Animator>().SetBool("isDancing", true);
            back_button_.SetActive(true);
            try {
                roboVO.robotAudio_.clip = roboVO.fixedClips_[UnityEngine.Random.Range(0, roboVO.fixedClips_.Length - 1)];
                roboVO.robotAudio_.Play();
            } catch (Exception) { }
        } else {
            SetXRayCameraActive(true);
            try {
                roboVO.robotAudio_.clip = roboVO.brokenClips_[UnityEngine.Random.Range(0, roboVO.brokenClips_.Length - 1)];
                roboVO.robotAudio_.Play();
            } catch (Exception) { }
        }
    }


    //OnTriggerEnter with broken part, starts a coroutine countdown
    void OnTriggerEnter2D(Collider2D other) {
        if (!InteractManager.Instance.is_touched) {
            SetDetectStatus(null);
            return;
        }

        if (other.gameObject.tag == "Part")
            SetDetectStatus(other);
    }

    void OnTriggerStay2D(Collider2D other) {
        if (!InteractManager.Instance.is_touched && !is_entered_) {
            SetDetectStatus(null);
            return;
        }

        if (is_entered_) {
            detect_time_ -= Time.fixedDeltaTime;

            if (detect_time_ <= 0) {
                PopsUpToolBox();
                part_ptr_ = other.gameObject;
                SoundManager.Instance.PlaySFX(sfx_a_scan);
            }
        } else if (other.gameObject.tag == "Part")
            SetDetectStatus(other);
    }

    //Exit with part, stops and resets countdown
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Part")
            SetDetectStatus(null);
    }

    private void SetDetectStatus(Collider2D other) {
        detect_time_ = detect_thres_;

        mesh_animator_.SetBool("hit", other != null);
        is_entered_ = other != null;

        if (is_entered_)
            SoundManager.Instance.PlaySFX(sfx_detect);
    }

    private void PopsUpToolBox() {
        //SoundManager.Instance.PlaySFX(afterScannnigDetected, false);
        SetXRayCameraActive(false);
        ToolBoxBehavior.Instance.PopulateProblem(category_);
    }

    private void SetXRayCameraActive(bool active) {
        collider_.enabled = active;
        render_mesh_.SetActive(active);
        back_button_.SetActive(active);

        if (active)
            SoundManager.Instance.PlaySFX(sfx_scanner);

        is_entered_ = false;
        transform.localScale = Vector3.zero;
    }

    // Use this for initialization
    void Start() {
        SoundManager.Instance.SwitchScene(null, sfx_b_scan);
        SoundManager.Instance.PlayBGM(bgm[bgmTrack]);

        detect_thres_ = (float)ConstantTweakTool.Instance["DetectThreshold"];
        detect_time_ = detect_thres_;

        render_mesh_ = transform.GetChild(0).gameObject;
        mesh_animator_ = render_mesh_.GetComponent<Animator>();
        collider_ = GetComponent<BoxCollider2D>();

        // TODO wrap category
        float wrapper = GameController.add ? 6.0f : 5.0f;
        int sub_offset = GameController.add ? 0 : 6;
        category_ = Mathf.RoundToInt(Mathf.Repeat(MapRobotBehavior.GetDockedId(), 5.0f) + 1) + sub_offset;
        // TODO wrap robots
        try {
            robot_ = Instantiate(RobotCluster.Instance.GetRobotById(MapRobotBehavior.GetDockedId()));
            roboVO = robot_.GetComponent<RobotVO>();
        } catch (NullReferenceException) { }

        parts_ = new List<GameObject>(GameObject.FindGameObjectsWithTag("Part"));

        scale_factor_ = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Update() {
        if (InteractManager.Instance.is_touched || is_entered_)
            transform.DOScale(scale_factor_, 0.5f);
        else
            transform.DOScale(Vector3.zero, 0.5f);

        Vector2 pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = new Vector3(pos.x, pos.y, -1);
    }
}