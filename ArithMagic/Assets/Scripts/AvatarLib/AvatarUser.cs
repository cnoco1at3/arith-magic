using UnityEngine;
using System.Collections.Generic;
using Util;

namespace AvatarLib {
    public class AvatarUser : GenericSingleton<AvatarUser> {

        private AvatarBackEnd avatar_conn;
        public static AvatarProfile current_prof { get; private set; }

        // Use this for initialization
        void Start() {
            // not instant save mode, need to manually save the xml file after modifying user profiles
            if (avatar_conn == null)
                avatar_conn = new AvatarBackEnd();
        }

        public List<AvatarProfile> GetProfiles() {
            if (avatar_conn != null)
                return avatar_conn.GetProfilesList();
            return null;
        }

        public AvatarProfile AddProfile(string first, string last) {

            AvatarProfile.UserType user = AvatarProfile.UserType.kStudent;
            AvatarProfile prof = new AvatarProfile(first, last, user);

            avatar_conn.AddProfile(prof);
            avatar_conn.SaveToText();

            return prof;
        }

        public AvatarProfile EditProfileById(int id, string first, string last, AvatarProfile.GradeLevel grade) {

            AvatarProfile prof = avatar_conn.GetProfileByIndex(id);

            prof.first_name = first;
            prof.last_name = last;
            prof.grade = grade;

            avatar_conn.SaveToText();

            return prof;
        }

        public void LogInById(int id) {
            if (avatar_conn != null)
                current_prof = avatar_conn.GetProfileByIndex(id);
        }

        public void SignOut() {
            current_prof = null;
        }
    }
}
