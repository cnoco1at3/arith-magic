using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    private Vector2 xrayPos;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        xrayPos = Input.mousePosition;
        xrayPos = Camera.main.ScreenToWorldPoint(xrayPos);
        transform.position = new Vector3(xrayPos.x, xrayPos.y, -1); 
	}
}
