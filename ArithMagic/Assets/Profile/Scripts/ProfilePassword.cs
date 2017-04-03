using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ProfilePassword : MonoBehaviour {

	private string answer = "";
	public List<Button> buttons = new List<Button>();
	private bool Up;
	private int counter;
	private bool wrongAnswerDetected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (wrongAnswerDetected && counter < 5) {
			if (Up) {
				transform.Translate (0.5f, 0, 0);
				Up = false;
			} else {
				transform.Translate (-0.5f, 0, 0);
				Up = true;
				counter++;
			}
		} else {
			transform.position = new Vector3 (0,0,0);
			wrongAnswerDetected = false;
			counter = 0;
		}
		
		if (answer == "53") {
			SceneManager.LoadScene ("Profile");
		} else {
			if (answer.Length > 1 && answer != "53") {
				answer = "";
				wrongAnswerDetected = true;
			}
		}

		
	}

	public void ButtonClicked() {
		string buttonName = EventSystem.current.currentSelectedGameObject.name;
		Debug.Log (buttonName);
		answer += buttonName;
	}

//	public void OnMouseDown(){
//		Debug.Log ("cancel");
//		if (gameObject.tag == "cancel") {
//			Debug.Log ("cancel");
//		}
//	}



}
