using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    //public Transform destination;

    private UnityEngine.AI.NavMeshAgent agent;

    // Use this for initialization
    void Start () {

        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

       //agent.SetDestination(destination.position);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit; 
            if (Physics.Raycast(screenRay, out hit))
            {
                Debug.Log(hit.transform.tag);
                agent.SetDestination(hit.point);
            }
            //Debug.Log(GetComponent<Rigidbody>().velocity);
        }

    }
}
