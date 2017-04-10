using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InteractLib;

public class BackButton : Clickable {
    [SerializeField]
    private const string kMapScene = "Map";

    public override void ClickEvent() {
        if (XRayCameraBehavior.Instance.IsFinished())
            SceneManager.LoadScene(kMapScene);
        else
            GameController.ReturnToPreviousLevel();
    }
}
