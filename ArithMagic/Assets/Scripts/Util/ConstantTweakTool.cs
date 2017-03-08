using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using Util;

/// <summary>
/// Tool to help tweaking constants
/// </summary>
[ExecuteInEditMode]
public class ConstantTweakTool : GenericSingleton<ConstantTweakTool> {
    public delegate void ConstantEvent(ConstantTweakTool src);
    public static ConstantEvent ConstantEventHandler;

    /// <summary>
    /// The constants array
    /// </summary>
    [SerializeField]
    private ConstantXmlObject[] constants_;

    public Dictionary<string, int> const_dict;

    /// <summary>
    /// Saves to the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    public void Save(string path) {
        Xml.SaveXml(path, constants_);
    }

    /// <summary>
    /// Loads from the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    public void Load(string path) {
        constants_ = Xml.LoadXml<ConstantXmlObject[]>(path);

        BuildTable();

        if (ConstantEventHandler != null)
            ConstantEventHandler(this);
    }

    [Serializable]
    public struct ConstantXmlObject {
        public string key;
        public int value;
    }

    void Awake() {
        BuildTable();
    }

    private void BuildTable() {
        if (const_dict == null)
            const_dict = new Dictionary<string, int>();
        else
            const_dict.Clear();

        foreach (ConstantXmlObject xml_obj in constants_) {
            try {
                const_dict.Add(xml_obj.key, xml_obj.value);
            }
            catch (ArgumentException) {
                const_dict.Remove(xml_obj.key);
                const_dict.Add(xml_obj.key, xml_obj.value);
            }
        }
    }
}

[CustomEditor(typeof(ConstantTweakTool))]
public class ConstantTweakEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save")) {
            string path = EditorUtility.SaveFilePanel("", "", "untitiled", "xml");
            ((ConstantTweakTool)target).Save(path);
        }
        if (GUILayout.Button("Load")) {
            string path = EditorUtility.OpenFilePanel("", "", "xml");
            ((ConstantTweakTool)target).Load(path);
        }
        GUILayout.EndHorizontal();
    }
}