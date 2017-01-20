using UnityEngine;
using UnityEditor;
using System.Collections;


public class EditorUtilityOpenFilePanel {

        [MenuItem ("Examples/Overwrite Texture")]

        static void Apply () {
        Texture2D texture = (Texture2D)Selection.activeObject;
        if (texture == null) {
            EditorUtility.DisplayDialog(
                "Select Texture",
                "You Must Select a Texture first!",
                "Ok");
        }
        var path = EditorUtility.OpenFilePanel(
                "Overwrite with png",
                "",
                "png");
        if (path.Length != 0) {
            WWW www = new WWW("file:///" + path);
            www.LoadImageIntoTexture(texture);
        }
    }
}