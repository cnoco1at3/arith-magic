using DG.Tweening;
using SoundLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using UnityEngine.UI;

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

    //particle
    [SerializeField]
    private GameObject confetti;
    //Fixed part sprite
    [SerializeField]
    private Sprite fixedSprite_;

    //progressbar
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private Sprite[] progressSprites;
    private int numberFixed = -1;
    [SerializeField]
    private Text fixedText;

    public bool IsFinished {
        get {
            return parts_.Count == 0;
        }
        private set { }
    }

    public void CheckParts(bool remove_flag = true) {
        if (remove_flag) {
            try {
                parts_.Remove(part_ptr_);
                part_ptr_.GetComponent<CircleCollider2D>().enabled = false;
                part_ptr_.GetComponent<SpriteRenderer>().sprite = fixedSprite_;
                bgmTrack = Mathf.Clamp(bgmTrack + 1, 0, bgm.Length - 1);
                SoundManager.Instance.PlayBGM(bgm[bgmTrack], 0.5f);
                numberFixed++;
                fixedText.text = numberFixed + 1 + "/5";
                if (progressBar.transform.parent.gameObject.activeSelf) {
                    progressBar.sprite = progressSprites[numberFixed];
                }
            } catch (IndexOutOfRangeException) { }
        }

        if (parts_.Count == 0) {
            robot_.GetComponent<Animator>().SetBool("isDancing", true);
            back_button_.SetActive(true);
            back_button_.GetComponent<Animator>().SetTrigger("Scale");
            confetti.SetActive(true);

            try {
                roboVO.robotAudio_.clip = roboVO.fixedClips_[UnityEngine.Random.Range(0, roboVO.fixedClips_.Count)];
                roboVO.robotAudio_.Play();
            } catch (Exception) { }
        } else {
            SetXRayCameraActive(true);
            try {
                int clipNumber = UnityEngine.Random.Range(0, roboVO.brokenClips_.Count);
                roboVO.robotAudio_.clip = roboVO.brokenClips_[clipNumber];
                roboVO.brokenClips_.RemoveAt(clipNumber);
                roboVO.robotAudio_.Play();
            } catch (Exception) { }
        }
    }

    public void ClearParts() {
        parts_.Clear();
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
                PopsUpToolBox((MapRobotBehavior.GetDockedId() + 1) % 3 == 0);
                part_ptr_ = other.gameObject;
                SoundManager.Instance.PlaySFX(sfx_a_scan);
            }
        } else if (other.gameObject.tag == "Part")
            SetDetectStatus(other);
    }

    //Exit with part, stops and resets countdown
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Part") {
            SetDetectStatus(null);
            SoundManager.Instance.StopSFX(sfx_detect);
        }
    }

    private void SetDetectStatus(Collider2D other) {
        detect_time_ = detect_thres_;

        mesh_animator_.SetBool("hit", other != null);
        is_entered_ = other != null;

        if (is_entered_) {
            SoundManager.Instance.PlaySFX(sfx_detect, false, 0.5f);
        }
    }

    private void PopsUpToolBox(bool time_mode) {
        //SoundManager.Instance.PlaySFX(afterScannnigDetected, false);
        SetXRayCameraActive(false);
        ToolBoxBehavior.Instance.PopulateProblem(category_, time_mode);
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

    private void Awake() {
        try {
            robot_ = Instantiate(RobotCluster.Instance.GetRobotById(MapRobotBehavior.GetDockedId()));
        } catch (NullReferenceException) { }
    }

    // Use this for initialization
    void Start() {
        SoundManager.Instance.SwitchScene(bgm[bgmTrack], sfx_b_scan, 0.5f);

        detect_thres_ = (float)ConstantTweakTool.Instance["DetectThreshold"];
        detect_time_ = detect_thres_;

        render_mesh_ = transform.GetChild(0).gameObject;
        mesh_animator_ = render_mesh_.GetComponent<Animator>();
        collider_ = GetComponent<BoxCollider2D>();

        int wrap = GameController.add ? 6 : 5;
        int offset = GameController.add ? 1 : 7;
        category_ = ProblemRuler.GetCategory(MapRobotBehavior.GetDockedId(), (int)GameController.GetCurrentProfileGrade());
        category_ = Mathf.Clamp(category_, 0, wrap) + offset;

        try {
            roboVO = robot_.GetComponent<RobotVO>();
        } catch (NullReferenceException) { }

        parts_ = new List<GameObject>(GameObject.FindGameObjectsWithTag("Part"));

        scale_factor_ = transform.localScale;
        transform.localScale = Vector3.zero;

        if ((MapRobotBehavior.GetDockedId() + 1) % 3 == 0)
            progressBar.transform.parent.gameObject.SetActive(false);
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