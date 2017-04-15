using System;
using System.ComponentModel;

namespace AvatarLib {
    [Serializable]
    public class AvatarProfile : IEquatable<AvatarProfile> {

        public AvatarProfile() {
            name = "";
            age = 0;
            gender = 0;
            grade = 0;
        }

        public AvatarProfile(string name = "", uint age = 0,
            Gender gender = 0, GradeLevel grade = 0) {

            this.name = name;
            this.age = age;

            this.gender = gender;
            this.grade = grade;
        }

        public string name;

        public enum UserType {
            kStudent = 0,
            kTeacher = 1,
            kDevelop = 2
        };
        UserType user = UserType.kStudent;

        public enum GradeLevel {
            [Description("Kindergarten")]
            kK = 0,
            [Description("First Grade")]
            k1st = 1,
            [Description("Second Grade")]
            k2nd = 2
        };
        public GradeLevel grade;

        public enum Gender {
            [Description("Boy")]
            kBoy = 0,
            [Description("Girl")]
            kGirl = 1,
            [Description("Other")]
            kOther = 2
        };
        public Gender gender = 0;

        public uint age = 0;

        public int progress_index = -1;

        public bool Equals(AvatarProfile other) {
            return user.Equals(other.user) && name.Equals(other.name);
        }

        public override string ToString() {
            return name;
        }
    }
}
