using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileAddButton : ProfileButton {

    public override void ClickEvent() {
        ProfileEdit.Instance.OnEnterEditPanel(null, 0);
        ProfileGuide.Instance.MoveToScreenById(2);
    }
}
