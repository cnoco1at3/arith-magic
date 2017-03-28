using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMapMovement : MonoBehaviour {
    [SerializeField]
    private float speed;
    public Vector2 targetPos;

    private MapMovement[] levels;


    // Use this for initialization
    void Start() {
        targetPos = transform.localPosition;

        /*for(int i =0;i<levels.Lenght;i++)
         * {
         *  if(level[i].levelNumber >= gamecontroller.level)
         *  {
         *      level[i].unlocked = true
            }
         * }
         
         */
    }

    // Update is called once per frame
    void Update() {
        float step = speed * Time.deltaTime;
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPos, step);
    }
}
