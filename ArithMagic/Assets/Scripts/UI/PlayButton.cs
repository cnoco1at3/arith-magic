using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SoundLib;

public class PlayButton : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    private Button playButton;

    private AudioSource audi;

    public AudioClip clip;
    public AudioClip backgroundSound;

    public void StartButton()
    {
        SoundManager.Instance.PlaySFX(clip, false);
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        playButton.interactable = false;
        //audi.Play();
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(nextSceneName);
    }

    void Start()
    {
        SoundManager.Instance.PlayBGM(backgroundSound);
        audi = GetComponent<AudioSource>(); 
        playButton = GetComponent<Button>();
    }
}
