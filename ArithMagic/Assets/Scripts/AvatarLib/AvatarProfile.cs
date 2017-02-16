using System;

namespace AvatarLib {
    [Serializable]
    public class AvatarProfile : IEquatable<AvatarProfile> {

        public AvatarProfile() {
            first_name = "";
            last_name = "";
            user = UserType.kStudent;
        }

        public AvatarProfile(string first, string last, UserType user) {
            first_name = first;
            last_name = last;
            this.user = user;
        }

        public int color = 0;

        public string first_name;
        public string last_name;

        public enum UserType {
            kStudent = 0,
            kTeacher = 1,
            kDevelop = 2
        };
        UserType user;

        // TODO add progress here

        public bool Equals(AvatarProfile other) {
            return first_name.Equals(other.first_name) && last_name.Equals(other.last_name);
        }
    }
}
