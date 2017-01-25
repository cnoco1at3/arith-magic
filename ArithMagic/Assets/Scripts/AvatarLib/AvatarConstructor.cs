using UnityEngine;
using System;
using Util;

namespace AvatarLib {
    public class AvatarConstructor : GenericSingleton<AvatarConstructor> {

        public GameObject default_character;

        public GameObject ConstructByProfile(AvatarProfile profile) {
            if (default_character == null)
                throw new NullReferenceException();

            GameObject character = Instantiate(default_character);

            // changes happen here
            ChangeByProfile(character, profile);

            return character;
        }

        public void ChangeByProfile(GameObject character, AvatarProfile profile) {

        }
    }
}
