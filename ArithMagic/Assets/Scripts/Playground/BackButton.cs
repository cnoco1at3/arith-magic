using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InteractLib;

public class BackButton : Clickable {

    public override void ClickEvent() {
        if (XRayCameraBehavior.Instance.IsFinished())
            SceneManager.LoadScene("Map");
        else
            LevelController.BackLevel();
    }
}
