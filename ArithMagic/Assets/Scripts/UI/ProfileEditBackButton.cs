using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProfileEditBackButton : ProfileButton {
    [SerializeField]
    Transform popsup_;

    public override void ClickEvent() {
        if (ProfileEdit.Instance.ProfileChanged()) {
            ProfileEdit.Instance.SetButtonActive(false);
            popsup_.DOLocalMoveY(0.0f, 0.5f);
        } else {
            ProfileGuide.Instance.MoveToScreenById(ProfileEdit.Instance.from);
            ProfileEdit.Instance.OnExitEditPanel();
        }
    }
}
