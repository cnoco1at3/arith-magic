using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;
using AvatarLib;

public class ProfileEdit : GenericSingleton<ProfileEdit> {

    [SerializeField]
    InputField[] inputs_;

    [SerializeField]
    Dropdown[] dropdowns_;

    [SerializeField]
    Button[] buttons_;

    private AvatarProfile edit_;

    public int FromIndex { get; private set; }

    private void Start() {
        foreach (InputField input in inputs_)
            input.onValueChanged.AddListener(delegate { CheckIsValidProfile(); });
    }

    public void OnEnterEditPanel(AvatarProfile edit, int from) {
        edit_ = edit;
        this.FromIndex = from;
        buttons_[0].interactable = true;
        UpdateDisplay();
    }

    public void SetButtonActive(bool active) {
        foreach (Button button in buttons_)
            button.interactable = active;
    }

    public void OnExitEditPanel() {

        inputs_[0].text = "";
        inputs_[1].text = "";
        dropdowns_[0].value = 0;
        dropdowns_[1].value = 0;

        SetButtonActive(false);
    }

    public void SaveOrAddProfileToData() {
        if (edit_ == null) {
            AvatarProfile saved = new AvatarProfile(inputs_[0].text, (uint)Int32.Parse(inputs_[1].text), (AvatarProfile.GradeLevel)dropdowns_[1].value);
            GameController.AddProfile(saved);
        } else {
            GameController.EditProfile(edit_, inputs_[0].text, (uint)Int32.Parse(inputs_[1].text), (AvatarProfile.GradeLevel)dropdowns_[1].value);
        }
    }


    public void UpdateDisplay() {

        if (edit_ != null) {
            inputs_[0].text = edit_.ToString();
            inputs_[1].text = edit_.age.ToString();
            dropdowns_[1].value = (int)edit_.grade;
        } else {
            inputs_[0].text = "";
            inputs_[1].text = "";
            dropdowns_[0].value = 0;
            dropdowns_[1].value = 0;
        }
    }

    public bool ProfileChanged() {
        if (edit_ != null) {
            return edit_.name != inputs_[0].text || edit_.age != (uint)Int32.Parse(inputs_[1].text) || (int)edit_.grade != dropdowns_[1].value;
        } else {
            return inputs_[0].text != "" || inputs_[1].text != "";
        }
    }

    private void CheckIsValidProfile() {
        buttons_[1].interactable = inputs_[0].text != "" && inputs_[1].text != "";
    }
}
