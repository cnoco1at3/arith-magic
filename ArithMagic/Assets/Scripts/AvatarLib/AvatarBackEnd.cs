using UnityEngine;
using System;
using System.Collections.Generic;

namespace AvatarLib {
    public class AvatarBackEnd {

        /// <summary>
        /// The filename of profiles database
        /// </summary>
        private const string kFileName = "User_Profiles.xml";

        /// <summary>
        /// The profiles
        /// </summary>
        private List<AvatarProfile> profiles_;

        /// <summary>
        /// Instant save mode
        /// </summary>
        private bool instant_save_mode_ = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="AvatarBackEnd"/> class.
        /// </summary>
        public AvatarBackEnd() {
            string local_path_ = Application.dataPath + "/" + kFileName;
            Debug.Log(local_path_);
            profiles_ = Util.Xml.Load<List<AvatarProfile>>(local_path_) ?? new List<AvatarProfile>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AvatarBackEnd"/> class.
        /// </summary>
        /// <param name="instant_save">if set to <c>true</c> [instant save].</param>
        public AvatarBackEnd(bool instant_save) {
            instant_save_mode_ = instant_save;
            string local_path_ = Application.dataPath + "/" + kFileName;
            profiles_ = Util.Xml.Load<List<AvatarProfile>>(local_path_) ?? new List<AvatarProfile>();
        }

        /// <summary>
        /// Gets the profile list (Read only).
        /// </summary>
        /// <returns></returns>
        public List<AvatarProfile> GetProfilesList() {
            return new List<AvatarProfile>(profiles_);
        }

        /// <summary>
        /// Gets the size of the profiles.
        /// </summary>
        /// <returns></returns>
        public int GetProfilesSize() {
            return profiles_.Count;
        }

        /// <summary>
        /// Finds the profile.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <returns></returns>
        public AvatarProfile FindProfile(AvatarProfile avatar) {
            foreach (AvatarProfile profile in profiles_)
                if (avatar.Equals(profile))
                    return profile;
            return null;
        }

        /// <summary>
        /// Finds the index of the profile.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <returns></returns>
        public int FindProfileIndex(AvatarProfile avatar) {
            for(int i = 0; i < profiles_.Count; ++i) 
                if (avatar.Equals(profiles_[i]))
                    return i;
            return -1;
        }

        /// <summary>
        /// Gets the profile by the index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public AvatarProfile GetProfileByIndex(int index) {
            if(index < 0) 
                index = profiles_.Count + index;
            try {
                return profiles_[index];
            }
            catch (IndexOutOfRangeException) {
                return null;
            }
        }

        /// <summary>
        /// Sets the profile by the index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="avatar">The avatar.</param>
        public void SetProfileByIndex(int index, AvatarProfile avatar) {
            if(index < 0) 
                index = profiles_.Count + index;
            try {
                profiles_[index] = avatar;
            }
            catch (IndexOutOfRangeException) { }
            if (instant_save_mode_)
                SaveToText();
        }

        /// <summary>
        /// Adds the profile.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        public void AddProfile(AvatarProfile avatar) {
            if (avatar == null)
                return;

            profiles_.Add(avatar);

            if (instant_save_mode_)
                SaveToText();
        }

        /// <summary>
        /// Removes the profile.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        public void RemoveProfile(AvatarProfile avatar) {
            if (avatar == null)
                return;

            profiles_.Remove(avatar);

            if (instant_save_mode_)
                SaveToText();
        }

        /// <summary>
        /// Saves to text.
        /// </summary>
        public void SaveToText() {
            if (profiles_ != null)
                Util.Xml.Save(Application.dataPath + "/" + kFileName, profiles_);
        }
    }
}