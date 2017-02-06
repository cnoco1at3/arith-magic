using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

    public GameObject gameCamera;
    public bool zoom = false;
    public float zoomZPosition = 0;
    public GameObject toolBox;
    public GameObject[] arr;

	// Use this for initialization
	void Start () {
        toolBox.SetActive(false);
        toolBox.transform.localScale = new Vector3(1,1,1);
        
    }
	
	// Update is called once per frame
	void Update () {
        



        if (zoom)
        {
            if (gameCamera.transform.position.z < -7f)
            {
                gameCamera.transform.position += new Vector3(0, 0, zoomZPosition) * Time.deltaTime;
                zoomZPosition = zoomZPosition + 0.1f;
                Debug.Log(gameCamera.transform.position + ".................." + zoomZPosition);
            }
            else
            {
                toolBox.SetActive(true);
                if (toolBox.transform.localScale.x < 2f) {
                    toolBox.transform.localScale += new Vector3(0.05F, 0, 0);
                    Debug.Log("do something");
                } else
                {
                    zoom = false;
                }
                
                
            }
        }

    }

    void OnMouseDown()
    {
        Vector3 fwd = gameObject.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(gameObject.transform.position, fwd, 10))
        {
            //Debug.DrawLine();
            Debug.Log("There is something in front of the object!");
        }
            

        for (int i = 0; i < arr.Length; i++)
        {
            Debug.Log(arr[i].name);
        }
        Debug.Log(gameObject.name);
        zoom = true;
        
        //gameCamera.transform.position = new Vector3(0,2,-3);
        //gameCamera.transform.position = new Vector3(0,0, zoomYPosition);
        
    }
}
