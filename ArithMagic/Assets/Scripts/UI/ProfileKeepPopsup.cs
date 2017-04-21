using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProfileKeepPopsup : ProfileButton {

    [SerializeField]
    Transform popsup_;

    public override void ClickEvent() {
        ProfileInfo.Instance.SetButtonActive(true);
        popsup_.DOLocalMoveY(-800.0f, 0.5f);
    }
}
