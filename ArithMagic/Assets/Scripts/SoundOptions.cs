using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundLib;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    [SerializeField]
    private Sprite on;
    [SerializeField]
    private Sprite off;

    [SerializeField]
    private bool isMusic;

    private Image Sprite; 

	// Use this for initialization
	void Start ()
    {
        Sprite = GetComponent<Image>();
        SetSprite();
	}

    private void SetSprite()
    {
        if (isMusic)
        {
            if (SoundManager.Instance.CheckMusicState())
                Sprite.sprite = on;
            else if (!SoundManager.Instance.CheckMusicState())
                Sprite.sprite = off;
        }

        else if (!isMusic)
        {
            if (SoundManager.Instance.CheckSoundState())
                Sprite.sprite = on;
            else if (!SoundManager.Instance.CheckSoundState())
                Sprite.sprite = off; 
        }
    }

    public void ChangeMusic()
    {
        SoundManager.Instance.MuteMusic();
        SetSprite();
    }

    public void ChangeSound()
    {
        SoundManager.Instance.MuteSound();
        SetSprite();
    }

}
