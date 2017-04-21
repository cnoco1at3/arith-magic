using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class AddPart : Clickable
{
    private CustomRobo robot;

    [SerializeField]
    private UnityEvent SpawnPart;


    [SerializeField]
    private GameObject partPrefab;

	// Use this for initialization
	void Start ()
    {
        robot = GameObject.FindGameObjectWithTag("CustomRobot").GetComponent<CustomRobo>();
        if (!GetComponent<BoxCollider>())
        {
            this.gameObject.AddComponent<BoxCollider>(); 
        }
	}

    public override void ClickEvent()
    {
        SpawnPart.Invoke(); 
    }

    public void Replace_Body()
    {
        GameObject part;
        part = Instantiate(partPrefab, robot.transform);
        DestroyImmediate(robot.Body);
        robot.Body = part;
        robot.ResetBody();
    }
    public void Replace_Head()
    {
        GameObject part;
        part = Instantiate(partPrefab, robot.transform);
        DestroyImmediate(robot.Head);
        robot.Head = part;
        robot.ResetLimbs();
    }
    public void Replace_RightArm()
    {
        GameObject part;
        part = Instantiate(partPrefab, robot.transform);
        DestroyImmediate(robot.RightArm);
        robot.RightArm = part;
        robot.ResetLimbs();
    }

    public void Replace_LeftArm()
    {
        GameObject part;
        part = Instantiate(partPrefab, robot.transform);
        DestroyImmediate(robot.LeftArm);
        robot.LeftArm = part;
        robot.ResetLimbs(); 
    }
    public void Replace_RightLeg()
    {
        GameObject part;
        part = Instantiate(partPrefab, robot.transform);
        DestroyImmediate(robot.RightLeg);
        robot.RightLeg = part;
        robot.ResetLimbs();
    }
    public void Replace_LeftLeg()
    {
        GameObject part;
        part = Instantiate(partPrefab, robot.transform);
        DestroyImmediate(robot.LeftLeg);
        robot.LeftLeg = part;
        robot.ResetLimbs();
    }
}
