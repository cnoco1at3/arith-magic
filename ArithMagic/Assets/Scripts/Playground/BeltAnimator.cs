using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeltAnimator : MonoBehaviour {

    [SerializeField]
    private GameObject[] gears;

    public void RotateGears() {
        foreach(GameObject gear in gears) {
            Vector3 current = gear.transform.localEulerAngles;
            current -= new Vector3(0, 0, 180);
            gear.transform.DOLocalRotate(current, 2);
        }
    }
}
