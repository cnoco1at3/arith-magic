using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRay : MonoBehaviour {
    public GameObject brokenPart;
    [SerializeField]
    private int detectTime;
    private int detectTimeStart;
    private Coroutine detectCo;

    [SerializeField]
    private GameObject tool_box_;

    //OnTriggerEnter with broken part, starts a coroutine countdown
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Part") {
            Debug.Log("Enter");
            detectTime = detectTimeStart;
            brokenPart = other.gameObject;
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
        //Destroy(brokenPart);
        Instantiate(tool_box_);
        MousePosition.Instance.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start() {
        detectTimeStart = detectTime;
    }

}