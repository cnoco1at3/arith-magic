using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProfileDiscardPopsup : ProfileButton {
    [SerializeField]
    Transform popsup_;

    public override void ClickEvent() {
        popsup_.DOLocalMoveY(-800.0f, 0.5f);
        ProfileGuide.Instance.MoveToScreenById(ProfileEdit.Instance.FromIndex);
        ProfileEdit.Instance.OnExitEditPanel();
    }

}
