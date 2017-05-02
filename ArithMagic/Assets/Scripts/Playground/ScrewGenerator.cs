using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewGenerator : MonoBehaviour {

    private Transform[] anchors_;


    public void GenerateScrews(int id, bool add = true, int num = -1) {
        GameObject screw = ToolBoxBehavior.Instance.GetScrewById(id);

        anchors_ = new Transform[transform.childCount];
        for (int i = 0; i < anchors_.Length; ++i)
            anchors_[i] = transform.GetChild(i);

        if (anchors_.Length > 0) {
            num = num == -1 ? anchors_.Length : num;
            for (int i = 0; i < num; ++i) {
                GameObject s = Instantiate(screw, anchors_[i].position, Quaternion.identity, transform);
                s.transform.localScale = new Vector3(0.415f / transform.localScale.x, 0.415f / transform.localScale.y, 1.0f);
                GenericScrewBehavior sb = s.GetComponent<GenericScrewBehavior>();
                sb.add = add;
            }
        }
    }


    public void ClearScrews() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }


    public int GetRemainNum() {
        return transform.childCount - anchors_.Length;
    }
}
