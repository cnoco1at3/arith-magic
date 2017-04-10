using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AvatarLib {
    public class AvatarDisplay : MonoBehaviour {

        [SerializeField]
        private GameObject[] players_;

        private const float kStep = 250.0f;

        // Use this for initialization
        void Start() {
            List<AvatarProfile> profiles = GameController.GetProfiles();
            if (profiles == null)
                return;

            int j = 0;
            for (int i = 0; i < profiles.Count; ++i) {
                float mid = (profiles.Count - 1) / 2.0f;
                Vector3 pos = new Vector3(0, ((float)i - mid) * kStep);
                GameObject avatar = Instantiate(players_[j++], transform);
                avatar.transform.localPosition = pos;

                avatar.transform.GetChild(0).GetComponent<Text>().text = profiles[i].first_name + " " + profiles[i].last_name;

                if (j == 3) j = 0;
            }

            ((RectTransform)transform).sizeDelta = new Vector2(600, (profiles.Count + 1) * kStep);
        }
    }
}
