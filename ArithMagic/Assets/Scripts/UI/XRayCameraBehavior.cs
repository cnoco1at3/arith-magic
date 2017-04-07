using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;
using SoundLib;
using DG.Tweening;

public class XRayCameraBehavior : GenericSingleton<XRayCameraBehavior> {

    private int category_;

    private List<GameObject> parts_;
    private GameObject part_ptr_;

    private const int kDetectTimeThreshold = 2;
    private int detect_time_;
    private Coroutine detect_coroutine_;

    [SerializeField]
    private ToolBoxBehavior tool_box_;

    [SerializeField]
    private GameObject robot_;

    [SerializeField]
    private GameObject button_;

    private AudioSource sfx_src_;

    private GameObject render_mesh_;
    private BoxCollider2D collider_;
    private Vector3 scale_factor_;
    private bool is_entered_ = false;

    public AudioClip scannerSound;
    public AudioClip defectDetectionSound;
    public AudioClip scannerBackground;
    public AudioClip beforeScanningSound;
    public AudioClip afterScannnigDetected;

    //Robot VoiceOver
    private RobotVO roboVO;

    public void CheckParts(bool remove) {
        if (remove) {
            parts_.Remove(part_ptr_);
            Destroy(part_ptr_);
        }

        if (parts_.Count == 0) {
            robot_.GetComponent<Animator>().SetBool("isDancing", true);
            button_.SetActive(true);
            try {
                roboVO.robotAudio_.clip = roboVO.fixedClips_[UnityEngine.Random.Range(0, roboVO.fixedClips_.Length - 1)];
                roboVO.robotAudio_.Play();
            } catch (Exception) { }
        } else {
            SetXRayCameraActive(true);
            roboVO.robotAudio_.clip = roboVO.brokenClips_[UnityEngine.Random.Range(0, roboVO.brokenClips_.Length - 1)];
            roboVO.robotAudio_.Play();
            Debug.Log("Playing BrokenAudio");
            Debug.Log(roboVO.robotAudio_.clip.name);
        }
    }

    public bool IsFinished() {
        return parts_.Count == 0;
    }

    //OnTriggerEnter with broken part, starts a coroutine countdown
    void OnTriggerEnter2D(Collider2D other) {
        if (!InteractManager.Instance.IsTouched)
            return;

        SoundManager.Instance.PlaySFX(scannerSound, false);
        if (other.gameObject.tag == "Part") {
            SoundManager.Instance.StopSFX(scannerSound);
            SoundManager.Instance.PlaySFX(defectDetectionSound, false);

            detect_time_ = kDetectTimeThreshold;
            part_ptr_ = other.gameObject;
            if (detect_coroutine_ != null)
                StopCoroutine(detect_coroutine_);
            detect_coroutine_ = StartCoroutine(DetectPart());

            render_mesh_.GetComponent<Animator>().SetTrigger("ScannerAnim");
            render_mesh_.GetComponent<Animator>().ResetTrigger("Null");
            is_entered_ = true;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Part" && !is_entered_) {
            SoundManager.Instance.StopSFX(scannerSound);
            SoundManager.Instance.PlaySFX(defectDetectionSound, false);

            detect_time_ = kDetectTimeThreshold;
            part_ptr_ = other.gameObject;
            if (detect_coroutine_ != null)
                StopCoroutine(detect_coroutine_);
            detect_coroutine_ = StartCoroutine(DetectPart());

            render_mesh_.GetComponent<Animator>().SetTrigger("ScannerAnim");
            render_mesh_.GetComponent<Animator>().ResetTrigger("Null");
            is_entered_ = true;

        }
    }

    //Exit with part, stops and resets countdown
    void OnTriggerExit2D(Collider2D other) {
        if (!InteractManager.Instance.IsTouched)
            return;

        if (other.gameObject.tag == "Part") {
            if (detect_coroutine_ != null)
                StopCoroutine(detect_coroutine_);
            detect_time_ = kDetectTimeThreshold;

            render_mesh_.GetComponent<Animator>().SetTrigger("Null");
            render_mesh_.GetComponent<Animator>().ResetTrigger("ScannerAnim");

            is_entered_ = false;
        }
    }

    //Coroutine countdown for detecting broken part
    private IEnumerator DetectPart() {

        if (sfx_src_) {
            sfx_src_.Play();
        }
        while (detect_time_ > 0) {
            detect_time_--;
            yield return new WaitForSeconds(1);
        }
        PopsUpToolBox();
    }

    private void PopsUpToolBox() {
        //SoundManager.Instance.PlaySFX(afterScannnigDetected, false);
        SoundManager.Instance.StopSFX(scannerSound);
        SetXRayCameraActive(false);
        tool_box_.PopulateProblem(category_);
    }

    private void SetXRayCameraActive(bool flag) {
        collider_.enabled = flag;
        render_mesh_.SetActive(flag);
        button_.SetActive(flag);

        if (flag)
            SoundManager.Instance.PlaySFX(scannerBackground);
        else
            SoundManager.Instance.StopSFX(scannerBackground);
        StopCoroutine(detect_coroutine_);
    }

    // Use this for initialization
    void Start() {

        SoundManager.Instance.PlaySFX(beforeScanningSound, false);
        SoundManager.Instance.PlayBGM(null);
        sfx_src_ = GetComponent<AudioSource>();
        detect_time_ = kDetectTimeThreshold;

        render_mesh_ = transform.GetChild(0).gameObject;
        collider_ = GetComponent<BoxCollider2D>();

        // TODO wrap category
        category_ = Mathf.RoundToInt(Mathf.Repeat((float)MapRobotBehavior.GetDockedId(), 6.0f) + 1);
        // TODO wrap robots
        try {
            robot_ = Instantiate(RobotCluster.Instance.GetRobotById(MapRobotBehavior.GetDockedId()));
            roboVO = robot_.GetComponent<RobotVO>();
        } catch (NullReferenceException) { }

        parts_ = new List<GameObject>(GameObject.FindGameObjectsWithTag("Part"));

        scale_factor_ = transform.localScale;
    }

    void Update() {
        if (InteractManager.Instance.IsTouched || is_entered_)
            transform.DOScale(scale_factor_, 0.5f);
        else
            transform.DOScale(Vector3.zero, 0.5f);

        Vector2 pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = new Vector3(pos.x, pos.y, -1);
    }
}