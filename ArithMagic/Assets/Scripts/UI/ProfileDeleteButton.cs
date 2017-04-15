
public class ProfileDeleteButton : ProfileButton {

    public override void ClickEvent() {
        GameController.RemoveProfile(ProfileInfo.Instance.prof);
        ProfileGuide.Instance.MoveToScreenById(0);
        ProfileDisplay.Instance.UpdateDisplay();
    }
}
