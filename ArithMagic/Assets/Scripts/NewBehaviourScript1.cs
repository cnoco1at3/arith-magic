using UnityEngine;
using UnityEditor;
using System.Collections;

public class NewBehaviourScript1 : MonoBehaviour {
    public int test = 0;
}

[CustomEditor(typeof(NewBehaviourScript1))]
public class NewEditor : Editor {
    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        NewBehaviourScript1 tmp = (NewBehaviourScript1)target;
        if (GUILayout.Button("try")) {
            tmp.test++;
        }
    }
}
