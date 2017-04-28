﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InteractLib;

public class BackButton : ProfileButton {
    [SerializeField]
    private const string kMapScene = "Map";

    public override void ClickEvent() {
        if (XRayCameraBehavior.Instance.IsFinished && (MapRobotBehavior.GetDockedId() == GameController.GetCurrentLevel() + 1))
            GameController.AdvanceToNextLevel();
        SceneManager.LoadScene(kMapScene);
    }
}
