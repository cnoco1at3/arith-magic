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
        playButton = GetComponent<Button>();
    }
}
