﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundLib;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    private AudioClip introMusic; 

	// Use this for initialization
	void Start ()
    {
        
        StartCoroutine(StartIntro());
	}

    private IEnumerator StartIntro()
    {
        yield return new WaitForEndOfFrame();
        SoundManager.Instance.PlayBGM(introMusic);
    }
}