using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

/// <summary>
/// Tool to help tweaking constants
/// </summary>
[ExecuteInEditMode]
public class ConstantTweakTool : MonoBehaviour {

    public int test = 0;

    /// <summary>
    /// The constant dictionary
    /// </summary>
    private Dictionary<string, string> constant_dict_;

    void Start () {
        constant_dict_ = new Dictionary<string, string>();
        AddConstant("test", test.ToString());
    }

    public void Save(string path) {
        this.Start();
        ConstantObject constant_obj = new ConstantObject();
        constant_obj.constant_xml = new ConstantObject.ConstantItem[constant_dict_.Count];
        int i = 0;
        foreach (KeyValuePair<string, string> kv in constant_dict_)
            constant_obj.constant_xml[i++] = new ConstantObject.ConstantItem(kv.Key, kv.Value);

        Xml.Save(path, constant_obj);
    }

    public void Load(string path) {
        ConstantObject constant_obj = Xml.Load<ConstantObject>(path);
    }

    private void AddConstant (string key, string value) {
        try {
            constant_dict_.Add(key, value);
        }
        catch (ArgumentException) {
            constant_dict_.Remove(key);
            constant_dict_.Add(key, value);
        }
    }

    private void RemoveConstant (string key) {
        constant_dict_.Remove(key);
    }
}

[CustomEditor(typeof(ConstantTweakTool))]
public class ConstantTweakEditor : Editor {
    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        ConstantTweakTool tar = (ConstantTweakTool)target;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save")) {
            string path = EditorUtility.SaveFilePanel("", "", "untitiled", "xml");
            tar.Save(path);
        }
        if (GUILayout.Button("Load")) {

        }
        GUILayout.EndHorizontal();
    }
}