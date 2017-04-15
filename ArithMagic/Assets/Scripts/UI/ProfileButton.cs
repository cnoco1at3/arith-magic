using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ProfileButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(ClickEvent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void ClickEvent() {
        Debug.Log("clicked!");
    }
}
