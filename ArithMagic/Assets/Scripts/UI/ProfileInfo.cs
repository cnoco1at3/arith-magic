using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AvatarLib;
using Util;

public class ProfileInfo : GenericSingleton<ProfileInfo> {

    public AvatarProfile Profile { get; private set; }

    [SerializeField]
    private Text[] text_fields_;

    [SerializeField]
    private Button[] info_buttons_;

    public void OnEnterInfoPanel(AvatarProfile prof) {
        SetButtonActive(true);

        this.Profile = prof;

        UpdateDisplay();
    }

    public void SetButtonActive(bool active) {

        if (info_buttons_ != null) {
            foreach (Button button in info_buttons_)
                button.interactable = active;
        }
    }

    public void OnExitInfoPanel() {
        SetButtonActive(false);

        Profile = null;
    }

    public void UpdateDisplay() {
        if (Profile != null) {
            text_fields_[0].text = Profile.ToString();
            text_fields_[1].text = Profile.ToString();
            text_fields_[2].text = Profile.gender.GetDescription();
            text_fields_[3].text = Profile.age.ToString();
            text_fields_[4].text = Profile.grade.GetDescription();
        }

    }
}
