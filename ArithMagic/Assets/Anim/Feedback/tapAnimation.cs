using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapAnimation : MonoBehaviour {

    private bool keyPressed = false;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space")){
            Debug.Log("space");
            keyPressed = true;
            transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
        } else if (keyPressed)
        {
            keyPressed = false;
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        }
        

    }
}
