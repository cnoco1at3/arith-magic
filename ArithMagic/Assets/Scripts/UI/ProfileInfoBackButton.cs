using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileInfoBackButton : ProfileButton {

    public override void ClickEvent() {
        ProfileGuide.Instance.MoveToScreenById(0);
        ProfileInfo.Instance.OnExitInfoPanel();
    }
}
