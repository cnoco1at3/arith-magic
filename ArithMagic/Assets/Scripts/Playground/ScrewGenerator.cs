using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewGenerator : MonoBehaviour {

    private Transform[] anchors_;

    public void GenerateScrews(int id) {
        GameObject screw = ToolBoxBehavior.Instance.GetScrewById(id);

        anchors_ = new Transform[transform.childCount];
        for (int i = 0; i < anchors_.Length; ++i)
            anchors_[i] = transform.GetChild(i);

        if (anchors_.Length > 0) {
            for (int i = 0; i < anchors_.Length; ++i)
                Instantiate(screw, anchors_[i].position, Quaternion.identity, ToolBoxBehavior.Instance.transform);
        }
    }
}
