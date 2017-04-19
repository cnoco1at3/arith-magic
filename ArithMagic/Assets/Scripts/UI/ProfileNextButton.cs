using UnityEngine.SceneManagement;

public class ProfileNextButton : ProfileButton {

    public static int id = -1;

    public override void ClickEvent() {
        GameController.SignInById(id);
        SceneManager.LoadScene("Start");
    }
}
