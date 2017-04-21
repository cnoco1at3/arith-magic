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
        SetButtonActive(true);

        this.prof = prof;

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
