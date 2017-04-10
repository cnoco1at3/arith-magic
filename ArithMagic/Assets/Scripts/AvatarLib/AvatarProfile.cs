using System;

namespace AvatarLib {
    [Serializable]
    public class AvatarProfile : IEquatable<AvatarProfile> {

        public AvatarProfile() {
            first_name = "";
            last_name = "";
            user = UserType.kStudent;
            grade = GradeLevel.kK;
        }

        public AvatarProfile(string first, string last,
            UserType user = 0, GradeLevel grade = 0) {

            first_name = first;
            last_name = last;
            this.user = user;
            this.grade = grade;
        }

        public string first_name;
        public string last_name;

        public enum UserType {
            kStudent = 0,
            kTeacher = 1,
            kDevelop = 2
        };
        UserType user;

        public enum GradeLevel {
            kK = 0,
            k1st = 1,
            k2nd = 2
        };
        public GradeLevel grade;

        public int progress_index = -1;

        public bool Equals(AvatarProfile other) {
            return user.Equals(other.user) && first_name.Equals(other.first_name) && last_name.Equals(other.last_name);
        }
    }
}
