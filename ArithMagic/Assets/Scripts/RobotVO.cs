using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotVO : MonoBehaviour
{
    public AudioSource robotAudio_;

    public AudioClip[] fixedClips_;
    public AudioClip[] brokenClips_;


	// Use this for initialization
	void Start ()
    {
        robotAudio_ = GetComponent<AudioSource>();
    }
	
}
