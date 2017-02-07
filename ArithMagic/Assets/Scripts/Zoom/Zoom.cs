using UnityEngine;
using System.Collections;
using InteractLib;

public class Zoom : MonoBehaviour, IInteractable
{

    public GameObject gameCamera;
    public bool zoom = false;
    public bool zoomHead = false;
    public bool zoomChest = false;
    public bool zoomLeftHand = false;
    public bool zoomBackground = false;
    public float zoomZPosition = 0;
    public Animator anim;

    //public GameObject toolBox;

    // Use this for initialization
    void Start () {
        //toolBox.SetActive(false);
        //toolBox.transform.localScale = new Vector3(1,1,1);
        
    }
	
	// Update is called once per frame
	void Update () {

        if (zoomHead)
        {
            Debug.Log("zoomhead");
            if (gameCamera.transform.position.z < -3.6f && zoomZPosition<9f){
                gameCamera.transform.position += new Vector3(-1, 2.5f, zoomZPosition) * Time.deltaTime;
                zoomZPosition = zoomZPosition + 0.1f;
            } else {
                anim.SetTrigger("HeadTrigger");
                gameCamera.transform.position += new Vector3(-1, 2.5f, -3.6f) * Time.deltaTime;
                zoomHead = false;
                zoomZPosition = 0;
            }
        } else if (zoomChest) {
            Debug.Log("zoomchest");
            if (gameCamera.transform.position.z < -4.8f && zoomZPosition < 8.2f) {
                gameCamera.transform.position += new Vector3(-1.6f, -0.81f, zoomZPosition) * Time.deltaTime;
                zoomZPosition = zoomZPosition + 0.1f;
            } else{
                anim.SetTrigger("ChestTrigger");
                gameCamera.transform.position += new Vector3(-1.6f, -0.81f, -4.8f) * Time.deltaTime;
                zoomChest = false;
                zoomZPosition = 0;
            }
        } else if (zoomLeftHand)
        {
            if (gameCamera.transform.position.z < -2f && zoomZPosition < 10f){
                gameCamera.transform.position += new Vector3(-3f, 0, zoomZPosition) * Time.deltaTime;
                zoomZPosition = zoomZPosition + 0.1f;
            }else{
                anim.SetTrigger("LeftHandTrigger");
                gameCamera.transform.position += new Vector3(-3f, 0, -2f) * Time.deltaTime;
                zoomLeftHand = false;
                zoomZPosition = 0;
            }
        } else if (zoomBackground) {
            anim.SetTrigger("Reverse");
            gameCamera.transform.position = new Vector3(0, 0, -10);
            zoomBackground = false;
        }
    }

    public void OnTouchEnter()
    {
        Debug.Log(gameObject.tag);
        if (gameObject.tag == "Head")
        {
            zoomHead = true;
        } else if (gameObject.tag == "Chest")
        {
            zoomChest = true;
        }
        else if (gameObject.tag == "LeftHand")
        {
            zoomLeftHand = true;
        }
        else if (gameObject.tag == "Background")
        {
            zoomBackground = true;
        }
    }

    public void OnTouchStay(Vector3 touch_pos)
    {
        //Vector3 world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, transform.position.z));
        //transform.position = new Vector3(world_pos.x, world_pos.y, transform.position.z);
    }

    public void OnTouchExit() { }

    void OnMouseDown()
    {

        //Vector3 fwd = gameObject.transform.TransformDirection(Vector3.forward);
        //if (Physics.Raycast(gameObject.transform.position, fwd, 10))
        //{
        //    Debug.Log("There is something in front of the object!");
        //}


        //for (int i = 0; i < arr.Length; i++)
        //{
        //    Debug.Log(arr[i].name);
        //}
        //Debug.Log(gameObject.name);
        //zoom = true;

        //gameCamera.transform.position = new Vector3(0,2,-3);
        //gameCamera.transform.position = new Vector3(0,0, zoomYPosition);

    }
}
