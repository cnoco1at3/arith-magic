using System;
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
        int tmp = level_index_;
        try {
            level_index_ = AvatarUser.current_prof.progress_index;
        } catch (NullReferenceException) {
            level_index_ = tmp;
        }
        return level_index_;
    }

    public static void BackLevel() {
        level_index_ = FetchLevelFromUser() - 1;
        try {
            AvatarUser.current_prof.progress_index = level_index_;
        } catch (NullReferenceException) { }
        SceneManager.LoadScene("Map");
    }

    public void AdvanceLevel() {
        level_index_ = FetchLevelFromUser() + 1;
        try {
            AvatarUser.current_prof.progress_index = level_index_;
        } catch (NullReferenceException) { }
        SceneManager.LoadScene("GameVertical");
    }
}