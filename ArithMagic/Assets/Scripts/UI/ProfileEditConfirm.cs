using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AvatarLib;

public class ProfileEditConfirm : ProfileButton {

    public override void ClickEvent() {
        ProfileGuide.Instance.MoveToScreenById(ProfileEdit.Instance.from);
        ProfileEdit.Instance.SaveOrAddProfileToData();
        ProfileEdit.Instance.OnExitEditPanel();
        if (ProfileEdit.Instance.from == 0) {
            ProfileDisplay.last_selected = -1;
            ProfileDisplay.Instance.UpdateDisplay();
        }
        if (ProfileEdit.Instance.from == 1) {
            ProfileInfo.Instance.UpdateDisplay();
            ProfileDisplay.Instance.UpdateDisplay();
        }
    }
}
