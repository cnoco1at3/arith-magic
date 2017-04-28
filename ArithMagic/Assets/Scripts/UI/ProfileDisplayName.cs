﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileDisplayName : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string name = "";
        try {
            name = GameController.UserProf.name;
        } catch (Exception) { }
        GetComponent<Text>().text = name;
		
	}
}
