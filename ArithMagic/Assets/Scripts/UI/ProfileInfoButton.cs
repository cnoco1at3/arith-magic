using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileInfoButton : ProfileButton {
    public override void ClickEvent() {
        ProfileGuide.Instance.MoveToScreenById(1);
        ProfileInfo.Instance.OnEnterInfoPanel(GameController.GetProfileById(ProfileDisplay.last_selected));
    }
}
