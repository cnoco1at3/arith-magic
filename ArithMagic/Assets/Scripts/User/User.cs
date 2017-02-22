using UnityEngine;
using System.Collections.Generic;
using AvatarLib;
using Util;

public class User : GenericSingleton<User> {

    private static AvatarBackEnd avatar_conn;
    private static AvatarProfile current_prof;

    private static Color[] color_table = { Color.yellow, Color.red };

    // Use this for initialization
    void Start() {
        // not instant save mode, need to manually save the xml file after modifying user profiles
        if (avatar_conn == null)
            avatar_conn = new AvatarBackEnd();

        // no profiles exist, needs to create at least one
        List<AvatarProfile> profiles = avatar_conn.GetProfilesList();

        while (profiles.Count < 2)
            AddProfile();

        foreach (AvatarProfile profile in profiles) {
            // TODO display the profiles here
        }

        current_prof = avatar_conn.GetProfileByIndex(0);
        EditCurrent();
        avatar_conn.SaveToText();
    }

    void AddProfile() {

        // TODO input the user profile here
        string first = "", last = "";
        AvatarProfile.UserType user = AvatarProfile.UserType.kStudent;

        AvatarProfile newpro = new AvatarProfile(first, last, user);

        avatar_conn.AddProfile(newpro);

        avatar_conn.SaveToText();
    }

    void EditCurrent() {
        // TODO input the user profile here
        string first = "Edited", last = "Edited";

        current_prof.first_name = first;
        current_prof.last_name = last;
    }

    void SignOut() {
        current_prof = null;
    }

    // an example on how to modify a profile
    //void ExModifyProfile(int index, string name) {
    //    AvatarProfile profile = avatar_conn.GetProfileByIndex(index);

    //    // change the profile here
    //    profile.first_name = name;

    //    avatar_conn.SetProfileByIndex(index, profile);
    //    avatar_conn.SaveToText();
    //}
}
