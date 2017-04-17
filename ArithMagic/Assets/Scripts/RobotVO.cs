using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotVO : MonoBehaviour
{
    public AudioSource robotAudio_;

    public List<AudioClip> fixedClips_ = new List<AudioClip>();
    public List<AudioClip> brokenClips_ = new List<AudioClip>();


	// Use this for initialization
	void Start ()
    {
        robotAudio_ = GetComponent<AudioSource>();
    }
	
}
