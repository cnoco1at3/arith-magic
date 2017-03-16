using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using DG.Tweening;

public class mapMovement : Clickable  
{
    [SerializeField]
    private RobotMapMovement Robot;
    [SerializeField]
    private Vector2 newPos;

    private BoxCollider bCol; 

	// Use this for initialization
	void Start ()
    {
        Robot = GameObject.FindGameObjectWithTag("Player").GetComponent<RobotMapMovement>();
        bCol = GetComponent<BoxCollider>();
        newPos = transform.position;  
	}

    public override void ClickEvent()
    {
        Debug.Log("click");
        MoveRobot(); 
    }

    private void MoveRobot()
    {
        Robot.targetPos = newPos;
        bCol.enabled = true; 
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}

