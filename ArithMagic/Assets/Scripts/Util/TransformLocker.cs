#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TransformLocker : MonoBehaviour {

    Vector3 local_pos_;
    Quaternion local_rot_;
    Vector3 local_scale_;

    void Start() {
        local_pos_ = transform.localPosition;
        local_rot_ = transform.localRotation;
        local_scale_ = transform.localScale;
    }

	// Update is called once per frame
	void Update () {
        transform.localPosition = local_pos_;
        transform.localRotation = local_rot_;
        transform.localScale = local_scale_;
	}

    public void SetTransform() {
        local_pos_ = transform.localPosition;
        local_rot_ = transform.localRotation;
        local_scale_ = transform.localScale;
    }
}

public class LockerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Set"))
            ((TransformLocker)target).SetTransform();
    }
}
#endif
