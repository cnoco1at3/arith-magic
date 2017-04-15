using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;
using AvatarLib;

public class ProfileDisplay : GenericSingleton<ProfileDisplay> {

    [SerializeField]
    private GameObject user_;

    [SerializeField]
    private GameObject add_button_;

    [SerializeField]
    private Button next_button_;

    [SerializeField]
    private GameObject selected_;

    private const float kHStep = 225.0f;
    private const float kVStep = 255.0f;
    private const float kTAnchor = 365.0f;
    private const int kMaxProfiles = 9;

    private List<AvatarProfile> profiles_;
    private GameObject[] avatars_;

    // Use this for initialization
    void Start() {
        avatars_ = new GameObject[kMaxProfiles];

        ProfileUserButton.selected_ = selected_;
        ProfileUserButton.next_button_ = next_button_;

        UpdateDisplay();
    }

    public void UpdateDisplay() {
        for (int i = 0; i < kMaxProfiles; ++i) {
            if (avatars_[i] != null)
                Destroy(avatars_[i]);
        }

        next_button_.interactable = false;
        selected_.transform.localPosition = new Vector3(-500.0f, 320.0f);

        profiles_ = GameController.GetProfiles();
        if (profiles_ == null)
            return;

        int add_offset = 1;
        if (profiles_.Count >= 9) {
            add_button_.SetActive(false);
            add_offset = 0;
        } else {
            add_button_.SetActive(true);
        }


        for (int i = 0; i < profiles_.Count && i < kMaxProfiles; ++i) {
            Vector3 pos = new Vector3((((i + add_offset) % 3) - 1) * kHStep, kTAnchor - ((i + add_offset) / 3) * kVStep);
            GameObject avatar = Instantiate(user_, transform);
            avatar.transform.localPosition = pos;

            avatar.transform.GetChild(0).GetComponent<Text>().text = profiles_[i].ToString();
            avatar.GetComponent<ProfileUserButton>().id_ = i;

            avatars_[i] = avatar;
        }

        if(profiles_.Count > 0) {
            selected_.transform.localPosition = avatars_[0].transform.localPosition - new Vector3(0, 45.0f);
            ProfileNextButton.id = 0;
            next_button_.interactable = true;
        } else {
            ProfileNextButton.id = -1;
            next_button_.interactable = false;
        }
    }
}
