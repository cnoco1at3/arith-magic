using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Util;
using AvatarLib;

public class GameController : PersistentSingleton<GameController> {

    public const bool kDebug = true;

    void Awake() {
        DOTween.Init(true, true);
    }

    private void Start() {
        if (avatar_conn == null)
            avatar_conn = new AvatarBackEnd();
    }

    /*
     * BEGIN LEVEL
     */
    private static int level_index_ = -1;
    private const string kGameScene = "GameVertical";
    private const string kMapScene = "Map";

    public static int GetCurrentLevel() {
        try {
            level_index_ = user_prof.progress_index;
        } catch (NullReferenceException) { }
        return level_index_;
    }

    public static void ReturnToPreviousLevel() {
        level_index_ = GetCurrentLevel() - 1;
        try {
            user_prof.progress_index = level_index_;
            avatar_conn.SaveToText();
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
        SceneManager.LoadScene(kMapScene);
    }

    public static void AdvanceToNextLevel() {
        level_index_ = GetCurrentLevel() + 1;
        try {
            user_prof.progress_index = level_index_;
            avatar_conn.SaveToText();
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
        SceneManager.LoadScene(kGameScene);
    }
    /*
     * END LEVEL
     */

    /* 
     * BEGIN PROFILE 
     */
    private static AvatarBackEnd avatar_conn;
    public static AvatarProfile user_prof { get; private set; }

    public static List<AvatarProfile> GetProfiles() {
        List<AvatarProfile> profiles;
        try {
            profiles = avatar_conn.GetProfilesList();
        } catch (NullReferenceException) {
            profiles = null;
        }
        return profiles;
    }

    public static void AddProfile(string first, string last) {
        AvatarProfile profile = new AvatarProfile(first, last);
        avatar_conn.AddProfile(profile);
        avatar_conn.SaveToText();
    }

    public static void EditProfileById(int id, string first, string last, AvatarProfile.GradeLevel grade) {
        AvatarProfile prof = avatar_conn.GetProfileByIndex(id);

        prof.first_name = first;
        prof.last_name = last;
        prof.grade = grade;

        avatar_conn.SaveToText();
    }

    public static void SignInById(int id) {
        try {
            user_prof = avatar_conn.GetProfileByIndex(id);
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
    }

    public static void SignOut() {
        user_prof = null;
    }
    /*
     * END PROFILE
     */
}