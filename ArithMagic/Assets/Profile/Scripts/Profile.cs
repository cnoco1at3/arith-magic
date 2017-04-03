using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour {

	//public InputField mainInputField;

	//public List<Text> names = new List<Text>();
	public GameObject ExistingPlayerPrefab1;
	public GameObject ExistingPlayerPrefab2;
	public GameObject ExistingPlayerPrefab3;
	public Text Player1Text;
	public Text Player2Text;
	public Text Player3Text;
	public GameObject AddPlayerPrefab;

	// Use this for initialization
	void Start () {
		
		if (ProfileInfo.playerName.Count == 0) {
			
			Instantiate (AddPlayerPrefab, new Vector3 (0, 5, 0), Quaternion.identity);
		} else if (ProfileInfo.playerName.Count == 1) {
			Player1Text.text = ProfileInfo.playerName[0];
			Instantiate (ExistingPlayerPrefab1, new Vector3 (0, 5, 0), Quaternion.identity);
			Instantiate (AddPlayerPrefab, new Vector3 (0, 1, 0), Quaternion.identity);
			//AddPlayerPrefab.transform.position = new Vector3 (0, 0, 0);
			//names [0].text = ProfileInfo.playerName [0];
		} else if (ProfileInfo.playerName.Count == 2) {
			Player1Text.text = ProfileInfo.playerName[0];
			Player1Text.text = ProfileInfo.playerName[1];
			Instantiate (ExistingPlayerPrefab1, new Vector3 (0, 5, 0), Quaternion.identity);
			Instantiate (ExistingPlayerPrefab2, new Vector3 (0, 1, 0), Quaternion.identity);
			Instantiate (AddPlayerPrefab, new Vector3 (0, -3, 0), Quaternion.identity);
		} else {
			Player1Text.text = ProfileInfo.playerName[0];
			Player1Text.text = ProfileInfo.playerName[1];
			Player1Text.text = ProfileInfo.playerName[2];
			Instantiate (ExistingPlayerPrefab1, new Vector3 (0, 5, 0), Quaternion.identity);
			Instantiate (ExistingPlayerPrefab2, new Vector3 (0, 1, 0), Quaternion.identity);
			Instantiate (ExistingPlayerPrefab3, new Vector3 (0, -3, 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	void OnMouseDown()
	{
		Debug.Log ("testing");

	}
}
