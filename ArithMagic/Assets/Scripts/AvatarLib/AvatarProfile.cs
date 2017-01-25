using System;

namespace AvatarLib {
    [Serializable]
    public class AvatarProfile : IEquatable<AvatarProfile> {
        public string first_name = "";
        public string last_name = "";

        public int attr1 = 0;
        public int attr2 = 0;

        public enum UserType {
            kStudent = 0,
            kTeacher = 1,
            kDevelop = 2
        };
        UserType user = UserType.kStudent;

        public bool Equals(AvatarProfile other) {
            return first_name.Equals(other.first_name);
        }
    }
}
