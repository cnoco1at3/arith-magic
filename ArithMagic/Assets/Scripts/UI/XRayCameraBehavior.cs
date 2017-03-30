using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;
using SoundLib;

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

    private AudioSource sfx_src_;

    private GameObject render_mesh_;
    private BoxCollider2D collider_;

    public AudioClip scannerSound;
    public AudioClip defectDetectionSound;

    public void CheckParts(bool remove) {
        if (remove) {
            parts_.Remove(part_ptr_);
            Destroy(part_ptr_);
        }

        if (parts_.Count == 0) {
            robot_.GetComponent<Animator>().SetBool("isDancing", true);
            robot_.GetComponent<AudioSource>().Play();
            StartCoroutine(BackToMap());
        } else
            SetXRayCameraActive(true);
    }

    //OnTriggerEnter with broken part, starts a coroutine countdown
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Part") {
            SoundManager.Instance.PlaySFX(defectDetectionSound);
            detect_time_ = kDetectTimeThreshold;
            part_ptr_ = other.gameObject;
            detect_coroutine_ = StartCoroutine(DetectPart());
            render_mesh_.GetComponent<Animator>().SetTrigger("ScannerAnim");
            render_mesh_.GetComponent<Animator>().ResetTrigger("Null");
        }
    }

    //Exit with part, stops and resets countdown
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Part") {
            StopCoroutine(detect_coroutine_);
            detect_time_ = kDetectTimeThreshold;
            render_mesh_.GetComponent<Animator>().SetTrigger("Null");
            render_mesh_.GetComponent<Animator>().ResetTrigger("ScannerAnim");
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
        //do math stuff
        Debug.Log("StartMath");
        SetXRayCameraActive(false);
        tool_box_.PopulateProblem(category_);
    }

    private void SetXRayCameraActive(bool flag) {
        collider_.enabled = flag;
        render_mesh_.SetActive(flag);
        StopCoroutine(detect_coroutine_);
    }

    private IEnumerator BackToMap() {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Map");
    }

    // Use this for initialization
    void Start() {
        
        sfx_src_ = GetComponent<AudioSource>();
        robot_ = GameObject.FindGameObjectWithTag("Robot");
        detect_time_ = kDetectTimeThreshold;
        parts_ = new List<GameObject>(GameObject.FindGameObjectsWithTag("Part"));

        render_mesh_ = transform.GetChild(0).gameObject;
        collider_ = GetComponent<BoxCollider2D>();

        // TODO wrap category
        category_ = MapRobotBehavior.GetDockedId() + 1;
    }

    void Update() {
        SoundManager.Instance.PlaySFX(scannerSound);
        Vector2 pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = new Vector3(pos.x, pos.y, -1);
    }
}