using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class RobotCluster : GenericSingleton<RobotCluster> {

    [SerializeField]
    private GameObject[] robots;

    public GameObject GetRobotById(int id) {
        int wrap_id = Mathf.RoundToInt(Mathf.Repeat((float)id, (float)robots.Length - 1) + 1);
        return robots[wrap_id];
    }
}
