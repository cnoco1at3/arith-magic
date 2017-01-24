using UnityEngine;
using System;
using System.Collections.Generic;

namespace AvatarLib {
    public class AvatarBackEnd {

        /// <summary>
        /// The filename of profiles database
        /// </summary>
        private const string filename = "Profiles.xml";

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
            string local_path_ = Application.dataPath + "/" + filename;
            Debug.Log(local_path_);
            profiles_ = Util.Xml.Load<List<AvatarProfile>>(local_path_) ?? new List<AvatarProfile>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AvatarBackEnd"/> class.
        /// </summary>
        /// <param name="instant_save">if set to <c>true</c> [instant save].</param>
        public AvatarBackEnd(bool instant_save) {
            instant_save_mode_ = instant_save;
            string local_path_ = Application.dataPath + "/" + filename;
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
        /// Gets the profile by the index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public AvatarProfile GetProfileByIndex(int index) {
            return profiles_[index];
        }

        /// <summary>
        /// Sets the profile by the index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="avatar">The avatar.</param>
        public void SetProfileByIndex(int index, AvatarProfile avatar) {
            profiles_[index] = avatar;
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
                Util.Xml.Save(Application.dataPath + "/" + filename, profiles_);
        }
    }
}