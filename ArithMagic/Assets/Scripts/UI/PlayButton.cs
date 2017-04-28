using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SoundLib;

public class PlayButton : MonoBehaviour {
    [SerializeField]
    private string nextSceneName;
    private Button playButton;

    [SerializeField]
    private bool add = false;

    public AudioClip clip;
    //public AudioClip backgroundSound;

    public void StartButton() {
        SoundManager.Instance.PlaySFX(clip);
        playButton.interactable = false;
        //SoundManager.Instance.StopBGM();
        SceneManager.LoadScene(nextSceneName);

        GameController.add = this.add;
    }

    void Start() {
        //SoundManager.Instance.PlayBGM(backgroundSound);
        playButton = GetComponent<Button>();
    }
}
