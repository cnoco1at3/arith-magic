using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRay : MonoBehaviour {
    [SerializeField]
    private GameObject[] brokenParts;
    [SerializeField]
    private int totalParts; 
    public GameObject currentPart;
    [SerializeField]
    private int detectTime;
    private int detectTimeStart;
    private Coroutine detectCo;

    [SerializeField]
    private GameObject tool_box_;

    [SerializeField]
    private GameObject robot;

    private AudioSource soundEffect;

    //OnTriggerEnter with broken part, starts a coroutine countdown
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Part") {
            Debug.Log("Enter");
            detectTime = detectTimeStart;
            currentPart = other.gameObject;
            detectCo = StartCoroutine(DetectPart());
        }
    }

    //Exit with part, stops and resets countdown
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Part") {
            StopCoroutine(detectCo);
            Debug.Log("Exit");
            detectTime = detectTimeStart;
        }
    }

    //Coroutine countdown for detecting broken part
    private IEnumerator DetectPart() {
        if (soundEffect) {
            soundEffect.Play();
        }
        while (true) {
            if (detectTime > 0) {
                Debug.Log("DetectTime - 1");
                detectTime -= 1;
                yield return new WaitForSeconds(1);
            }

            //at 0 starts funciton to spawn math problem
            else if (detectTime == 0) {
                Debug.Log("DetectTime = 0");
                StartMath();
                yield return null;
            }

        }

    }

    private void StartMath() {
        //do math stuff
        Debug.Log("StartMath");
        StopCoroutine(detectCo);
        Instantiate(tool_box_);
        MousePosition.Instance.gameObject.SetActive(false);
    }

    public void CheckParts()
    {
        brokenParts = GameObject.FindGameObjectsWithTag("Part");
        totalParts = brokenParts.Length;
        if (totalParts == 0)
        {
            MousePosition.Instance.gameObject.SetActive(false);
            robot.GetComponent<Animator>().SetBool("isDancing", true);
            robot.GetComponent<AudioSource>().Play();
        }
        else if (totalParts > 0)
        {
            MousePosition.Instance.gameObject.GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true; 
        }
    }

    // Use this for initialization
    void Start() {
        soundEffect = GetComponent<AudioSource>();
        robot = GameObject.FindGameObjectWithTag("Robot");
        detectTimeStart = detectTime;
        brokenParts = GameObject.FindGameObjectsWithTag("Part");
        totalParts = brokenParts.Length; 
    }

}