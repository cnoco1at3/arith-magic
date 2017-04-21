using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRobo : MonoBehaviour
{
    public GameObject Body;
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

    [SerializeField]
    private GameObject defaultBody; 


    public void ResetLimbs()
    {
        if (Head)
        {
            Head.transform.SetParent(this.transform);
            Head.transform.position = HeadPos.transform.position;
        }
        if (RightArm)
        {
            RightArm.transform.SetParent(this.transform);
            RightArm.transform.position = RightArmPos.transform.position;
        }
        if (LeftArm)
        {
            LeftArm.transform.SetParent(this.transform);
            LeftArm.transform.position = LeftArmPos.transform.position;
        }
        if (RightLeg)
        {
            RightLeg.transform.SetParent(this.transform);
            RightLeg.transform.position = RightLegPos.transform.position;
        }
        if (LeftLeg)
        {
            LeftLeg.transform.SetParent(this.transform);
            LeftLeg.transform.position = LeftLegPos.transform.position;
        }
    }

    public void ResetBody()
    {
        Body.transform.position = this.transform.position; 
        Body.transform.SetParent(this.transform);
        HeadPos = Body.transform.FindChild("HeadPos").gameObject;
        RightArmPos = Body.transform.FindChild("RightArmPos").gameObject;
        LeftArmPos = Body.transform.FindChild("LeftArmPos").gameObject;
        RightLegPos = Body.transform.FindChild("RightLegPos").gameObject;
        LeftLegPos = Body.transform.FindChild("LeftLegPos").gameObject;

        ResetLimbs();
    }

	// Use this for initialization
	void Start ()
    {
        if (!Body)
            Body = Instantiate(defaultBody,this.transform); 
        ResetBody();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}


}
