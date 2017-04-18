using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Util;
using AvatarLib;

public class GameController : PersistentSingleton<GameController> {

    public const bool kDebug = true;

    public static bool add = true;

    void Awake() {
        DOTween.Init(true, true);
    }

    private void Start() {
        if (avatar_conn == null)
            avatar_conn = new AvatarBackEnd();
    }

    #region level
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

    public static void EnterNextLevel() {
        SceneManager.LoadScene(kGameScene);
    }

    public static void AdvanceToNextLevel() {
        level_index_ = GetCurrentLevel() + 1;
        try {
            user_prof.progress_index = level_index_;
            avatar_conn.SaveToText();
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
    }
    /*
     * END LEVEL
     */
    #endregion

    #region profile
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

    public static void AddProfile(AvatarProfile profile) {
        avatar_conn.AddProfile(profile);
        avatar_conn.SaveToText();
    }

    public static AvatarProfile GetProfileById(int id) {
        return avatar_conn.GetProfileByIndex(id);
    }

    public static void RemoveProfile(AvatarProfile prof) {
        avatar_conn.RemoveProfile(prof);
        avatar_conn.SaveToText();
    }

    public static void EditProfile(AvatarProfile prof, string name, uint age, AvatarProfile.Gender gender, AvatarProfile.GradeLevel grade) {
        prof.name = name;
        prof.age = age;
        prof.gender = gender;
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
    #endregion

    #region cheat

    public AudioClip cheat_clip;
    public static long sequence = 0;
    public const long cheatcode = 1122343456;

    public void CheckCheat() {
       // if (sequence == cheatcode) {
            Debug.Log("Cheat!");
            SoundLib.SoundManager.Instance.PlaySFX(cheat_clip);
            add = false;
        //}
    }

    #endregion
}