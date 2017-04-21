using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMapButton : ProfileButton {
    public override void ClickEvent() {
        LevelCluster.Instance.UnlockAll();
    }
}
