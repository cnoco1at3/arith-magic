using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AvatarLib;
using Util;

public class ProfileInfo : GenericSingleton<ProfileInfo> {

    public AvatarProfile prof { get; private set; }

    [SerializeField]
    private Text[] text_fields_;

    [SerializeField]
    private Button[] info_buttons_;

    public void OnEnterInfoPanel(AvatarProfile prof) {
        if (info_buttons_ != null) {
            foreach (Button button in info_buttons_)
                button.interactable = true;
        }

        this.prof = prof;

        UpdateDisplay();
    }

    public void OnExitInfoPanel() {
        if (text_fields_ != null) {
            foreach (Text text in text_fields_)
                text.text = "";
        }

        if (info_buttons_ != null) {
            foreach (Button button in info_buttons_)
                button.interactable = false;
        }

        prof = null;
    }

    public void UpdateDisplay() {
        if (prof != null) {
            text_fields_[0].text = prof.ToString();
            text_fields_[1].text = prof.ToString();
            text_fields_[2].text = prof.gender.GetDescription();
            text_fields_[3].text = prof.age.ToString();
            text_fields_[4].text = prof.grade.GetDescription();
        }

    }
}
