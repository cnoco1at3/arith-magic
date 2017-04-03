using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessProfile : MonoBehaviour {

	public GameObject profilePassword;
	//public GameObject profilePasswordCancelIcon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		profilePassword.SetActive (true);
		//profilePasswordCancelIcon.SetActive (true);
	}
}
