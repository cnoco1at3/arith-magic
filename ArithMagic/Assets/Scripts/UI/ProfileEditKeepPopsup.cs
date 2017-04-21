using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProfileEditKeepPopsup : ProfileButton {

    [SerializeField]
    Transform popsup_;

    public override void ClickEvent() {
        ProfileEdit.Instance.SetButtonActive(true);
        popsup_.DOLocalMoveY(-800.0f, 0.5f);
    }
}
