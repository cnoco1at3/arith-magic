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

    [SerializeField]
    private bool unlocked = false;

    public int levelNumber; 

	// Use this for initialization
	void Start ()
    {
        Robot = GameObject.FindGameObjectWithTag("Player").GetComponent<RobotMapMovement>();
        bCol = GetComponent<BoxCollider>();
        newPos = new Vector2(transform.position.x - 3, transform.position.y);
	}

    public override void ClickEvent()
    {
        if (unlocked == true)
        {
            Debug.Log("click");
            MoveRobot();
        }
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

