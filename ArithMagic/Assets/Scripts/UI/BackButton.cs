using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InteractLib;

public class BackButton : Clickable {
    [SerializeField]
    private const string kMapScene = "Map";

    public override void ClickEvent() {
        if (XRayCameraBehavior.Instance.is_finished && (MapRobotBehavior.GetDockedId() == GameController.GetCurrentLevel() + 1))
            GameController.AdvanceToNextLevel();
        SceneManager.LoadScene(kMapScene);
    }
}
