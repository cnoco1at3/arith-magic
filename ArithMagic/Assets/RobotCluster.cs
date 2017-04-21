using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class RobotCluster : GenericSingleton<RobotCluster> {

    [SerializeField]
    private GameObject[] robots;

    public GameObject GetRobotById(int id) {
        int clamp_id = Mathf.Clamp(id, 0, robots.Length - 1);
        return robots[clamp_id];
    }

    public int GetRobotsSize() {
        return robots.Length;
    }
}
