using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplerTest : MonoBehaviour {

    // Use this for initialization
    void Start() {
        int[] res = new int[10];
        for (int i = 0; i < 10000; ++i) {
            res[RandomSampler.Sample10(1, 9)] += 1;
        }
        for (int i = 0; i < res.Length; ++i)
            Debug.Log(i + ": " + res[i]);
    }

    // Update is called once per frame
    void Update() {

    }
}
