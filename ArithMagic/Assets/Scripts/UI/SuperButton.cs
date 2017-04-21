using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperButton : ProfileButton {


    public override void ClickEvent() {
        XRayCameraBehavior.Instance.ClearParts();
        XRayCameraBehavior.Instance.CheckParts(false);
    }
}
