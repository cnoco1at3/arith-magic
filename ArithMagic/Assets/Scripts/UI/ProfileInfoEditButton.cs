using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileInfoEditButton : ProfileButton {

    public override void ClickEvent() {
        ProfileGuide.Instance.MoveToScreenById(2);
        ProfileEdit.Instance.OnEnterEditPanel(ProfileInfo.Instance.Profile, 1);
    }
}
