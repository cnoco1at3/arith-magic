using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AvatarLib;

public class ProfileEditConfirm : ProfileButton {

    public override void ClickEvent() {
        ProfileGuide.Instance.MoveToScreenById(ProfileEdit.Instance.FromIndex);
        ProfileEdit.Instance.SaveOrAddProfileToData();
        ProfileEdit.Instance.OnExitEditPanel();
        if (ProfileEdit.Instance.FromIndex == 0) {
            ProfileDisplay.last_selected = -1;
            ProfileDisplay.Instance.UpdateDisplay();
        }
        if (ProfileEdit.Instance.FromIndex == 1) {
            ProfileInfo.Instance.UpdateDisplay();
            ProfileDisplay.Instance.UpdateDisplay();
        }
    }
}
