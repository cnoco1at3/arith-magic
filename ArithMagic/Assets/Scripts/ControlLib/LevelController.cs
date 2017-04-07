using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AvatarLib;
using Util;

public class LevelController : PersistentSingleton<LevelController> {

    [SerializeField]
    private static GameObject[] robots;

    private static int level_index_ = -1;

    public static GameObject GetRobotById(int id) { return robots[id]; }

    public static int FetchLevelFromUser() {
        level_index_ = AvatarUser.current_prof.progress_index;
        return level_index_;
    }

    public void AdvanceLevel() {
        level_index_ = FetchLevelFromUser() + 1;
        AvatarUser.current_prof.progress_index = level_index_;
        SceneManager.LoadScene("GameVertical");
    }
}