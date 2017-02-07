using UnityEngine;
using System.Collections.Generic;
using AvatarLib;
using Util;

public class User : GenericSingleton<User> {

    private static AvatarBackEnd avatar_conn;
    private static AvatarProfile current_prof;

    // Use this for initialization
    void Start() {
        // no instant save mode, need to manually save the xml file after modifying user profiles
        if (avatar_conn == null)
            avatar_conn = new AvatarBackEnd(false);

        // no profiles exist, needs to create at least one
        if (avatar_conn.GetProfilesSize() < 1) {
            // creating profile here
            AvatarProfile new_profile = new AvatarProfile();

            // add the profile to the database
            avatar_conn.AddProfile(new_profile);

            // save the data base from memory to local storage
            avatar_conn.SaveToText();

            current_prof = new_profile;
            // AvatarConstructor.Instance.ConstructByProfile(new_profile);
        }

        // Examples
        //current_prof = avatar_conn.GetProfileByIndex(0);
        //ExModifyProfile(0, "hahaha");
        //AvatarProfile newnew_profile = new AvatarProfile();
        //avatar_conn.AddProfile(newnew_profile);
        //ExModifyProfile(1, "bububu");
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
