using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    private Button playButton;

    [SerializeField]
    private AudioSource introductionBackground;

    public void StartButton()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        playButton.interactable = false; 
        //sound effect
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextSceneName);
    }

    void Start()
    {
        introductionBackground = Instantiate(introductionBackground);
        introductionBackground.Play();
        playButton = GetComponent<Button>();
    }
}
