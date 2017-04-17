using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCheatButton : ProfileButton {
    [SerializeField]
    private int id_;

    public override void ClickEvent() {
        GameController.sequence = (GameController.sequence * 10 + id_) % 10000000000;
        GameController.Instance.CheckCheat();
    }
}
