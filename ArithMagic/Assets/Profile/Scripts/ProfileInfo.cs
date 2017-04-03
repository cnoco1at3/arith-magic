using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileInfo : MonoBehaviour {

	public InputField name;
	public InputField age;
	public Dropdown gradeLevel;
	public Button collectData;
	public static List<string> playerName = new List<string>();

	// Use this for initialization
	void Start () {
		//mainInputField.text = "Enter Text Here...";
	}

	// Update is called once per frame
	void Update () {
		Debug.Log (name.text);
		Debug.Log (gradeLevel.value);
	}

	public void PlayGame() {
		playerName.Add (name.text);
		Debug.Log (playerName[0]);

		SceneManager.LoadScene ("Game");
	}

//	public void BacktoProfile() {
//		playerName.Add (name.text);
//		Application.LoadLevel("Profile");
//	}
		
}
