﻿using System;
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

    public int from { get; private set; }

    public void OnEnterEditPanel(AvatarProfile edit, int from) {
        edit_ = edit;

        this.from = from;

        foreach (Button button in buttons_)
            button.interactable = true;

        UpdateDisplay();
    }

    public void OnExitEditPanel() {

        inputs_[0].text = "";
        inputs_[1].text = "";
        dropdowns_[0].value = 0;
        dropdowns_[1].value = 0;

        foreach (Button button in buttons_)
            button.interactable = false;
    }

    public void SaveOrAddProfileToData() {
        if (edit_ == null) {
            AvatarProfile saved = new AvatarProfile(inputs_[0].text, (uint)Int32.Parse(inputs_[1].text),
                (AvatarProfile.Gender)dropdowns_[0].value, (AvatarProfile.GradeLevel)dropdowns_[1].value);
            GameController.AddProfile(saved);
        } else {
            GameController.EditProfile(edit_, inputs_[0].text, (uint)Int32.Parse(inputs_[1].text),
                (AvatarProfile.Gender)dropdowns_[0].value, (AvatarProfile.GradeLevel)dropdowns_[1].value);
        }
    }


    public void UpdateDisplay() {

        if (edit_ != null) {
            inputs_[0].text = edit_.ToString();
            inputs_[1].text = edit_.age.ToString();
            dropdowns_[0].value = (int)edit_.gender;
            dropdowns_[1].value = (int)edit_.grade;
        } else {
            inputs_[0].text = "";
            inputs_[1].text = "";
            dropdowns_[0].value = 0;
        }
    }
}