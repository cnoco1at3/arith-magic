using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileEditBackButton : ProfileButton {

    public override void ClickEvent() {
        ProfileGuide.Instance.MoveToScreenById(ProfileEdit.Instance.from);
        ProfileEdit.Instance.OnExitEditPanel();
    }
}
