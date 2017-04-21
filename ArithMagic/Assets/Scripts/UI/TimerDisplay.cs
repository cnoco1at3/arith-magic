using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDisplay : MonoBehaviour {

    void Awake() {
        Renderer rend = GetComponent<Renderer>();
        rend.sortingOrder = 200;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
