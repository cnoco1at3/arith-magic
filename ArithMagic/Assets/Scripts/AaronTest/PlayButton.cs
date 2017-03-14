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

    private AudioSource audi;

    public void StartButton()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        playButton.interactable = false;
        audi.Play();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextSceneName);
    }

    void Start()
    {
        audi = GetComponent<AudioSource>(); 
        playButton = GetComponent<Button>();
    }
}
