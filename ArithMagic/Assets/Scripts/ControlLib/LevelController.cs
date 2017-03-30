using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

public class LevelController : PersistentSingleton<LevelController> {

    [SerializeField]
    private static GameObject[] robots;

    [SerializeField]
    private static int level_index_ = -1;

    public static GameObject GetRobotById(int id) { return robots[id]; }

    public static int GetLevelIndex() { return level_index_; }

    public void AdvanceLevel() {
        level_index_++;
        SceneManager.LoadScene("GameVertical");
    }
}