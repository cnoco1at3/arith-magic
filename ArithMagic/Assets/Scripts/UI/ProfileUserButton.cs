using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProfileUserButton : ProfileButton {

    public static GameObject selected_;
    public static Button next_button_;
    public int id_ = -1;

    public override void ClickEvent() {
        Vector3 pos = transform.localPosition;
        pos.y -= 45.0f;
        selected_.transform.DOLocalMove(pos, 0.5f);
        next_button_.interactable = true;
        ProfileNextButton.id = id_;
    }
}
