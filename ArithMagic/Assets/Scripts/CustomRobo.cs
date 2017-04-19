using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRobo : MonoBehaviour
{
    public GameObject Head;
    public GameObject RightArm;
    public GameObject LeftArm;
    public GameObject RightLeg;
    public GameObject LeftLeg;

    public GameObject HeadPos;
    public GameObject RightArmPos;
    public GameObject LeftArmPos;
    public GameObject RightLegPos;
    public GameObject LeftLegPos;


    public void ResetLimbs()
    {
        Head.transform.position = HeadPos.transform.position;
        RightArm.transform.position = RightArmPos.transform.position;
        LeftArm.transform.position = LeftArmPos.transform.position;
        RightLeg.transform.position = RightLegPos.transform.position;
        LeftLeg.transform.position = LeftLegPos.transform.position;
    }

	// Use this for initialization
	void Start ()
    {
        ResetLimbs();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


}
