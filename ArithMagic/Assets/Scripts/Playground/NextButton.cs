using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InteractLib;

public class NextButton : MonoBehaviour, IInteractable {

    [SerializeField]
    private string next_scene_;

    public void OnTouchEnter(Vector3 touch_pos) {
        SceneManager.LoadScene(next_scene_);
    }

    public void OnTouchStay(Vector3 touch_pos) { }
    public void OnTouchExit(Vector3 touch_pos) { }
}
