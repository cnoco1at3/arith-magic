using UnityEngine.SceneManagement;

public class ProfileNextButton : ProfileButton {

    public override void ClickEvent() {
        GameController.SignInById(ProfileDisplay.last_selected);
        SceneManager.LoadScene("Start");
    }
}
