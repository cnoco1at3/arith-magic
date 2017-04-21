using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProfileDeleteButton : ProfileButton {

    [SerializeField]
    private Transform popsup_;

    public override void ClickEvent() {
        GameController.RemoveProfile(ProfileInfo.Instance.prof);
        ProfileGuide.Instance.MoveToScreenById(0);
        ProfileDisplay.Instance.UpdateDisplay();
        ProfileInfo.Instance.OnExitInfoPanel();
        popsup_.DOLocalMoveY(-800.0f, 0.5f);
    }
}
