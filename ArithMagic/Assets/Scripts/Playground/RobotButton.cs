using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using UnityEngine.SceneManagement;

public class RobotButton : Clickable {

    public override void ClickEvent() {
        Debug.Log("click");
        SceneManager.LoadScene("Game");
    }
}
