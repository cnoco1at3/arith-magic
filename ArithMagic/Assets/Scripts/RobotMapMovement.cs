using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMapMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public Vector2 targetPos;
	// Use this for initialization
	void Start ()
    {
        targetPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, step);	
	}
}
