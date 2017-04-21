using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProfileDeletePopsUp : ProfileButton {

    [SerializeField]
    private Transform popsup_;

    public override void ClickEvent() {
        popsup_.DOLocalMoveY(0, 0.5f);
        ProfileInfo.Instance.SetButtonActive(false);
    }
}
