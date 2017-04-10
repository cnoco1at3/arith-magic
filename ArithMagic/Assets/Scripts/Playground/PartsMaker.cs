using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class PartsMaker : PartsAcceptor {

    public Text text;

    private List<PartsBehavior> container_;
    private List<int> prev_ids_;

    void Start() {
        container_ = new List<PartsBehavior>();
        prev_ids_ = new List<int>();
    }

    public override Vector3 GetAcceptPoint(Vector3 position) {
        Vector3 pos = position;
        pos.z = transform.position.z;
        return pos;
    }

    public override Vector3 GetScaleFactor() {
        return Vector3.one;
    }

    public override void OnPartEnter(PartsBehavior part) {
        container_.Add(part);
        text.text = GetUpdateText(false);
    }

    public override void OnPartExit(PartsBehavior part) {
        container_.Remove(part);
        text.text = GetUpdateText(false);
    }

    // NOTE: call this function when the "MAKE" button is click or triggered
    public void OnMake() {
        // Only do this when there are more than one screw inside
        if (container_.Count > 1) {
            prev_ids_.Clear();
            int res = CalcResult();
            if (res < PartsCluster.Instance.parts.Length) {
                String tmp = GetUpdateText(true);
                foreach (PartsBehavior part in container_) {
                    Destroy(part.gameObject);
                    prev_ids_.Add(part.part_id);
                }
                container_.Clear();

                GameObject npart = (GameObject)Instantiate(PartsCluster.Instance.parts[res],
                    transform.position - new Vector3(0, 0, 1.5f),
                    Quaternion.identity);

                try {
                    PartsBehavior pb = npart.GetComponent<PartsBehavior>();
                    pb.curr_acceptor_ = this;
                    pb.is_accepted_ = true;
                    OnPartEnter(pb);
                }
                catch (Exception e) {
                    Debug.LogException(e);
                }
                text.text = tmp;
            }
        }
    }

    // TODO: we need to implement the recycle method here, include two steps
    // 1. destroy the part that is just put into the recycle bin
    // 2. get parts back from the prev ids list
    public void OnUndoMake() {

    }

    public override bool IsFinished() {
        return true;
    }

    // TODO: rewrite this function when other operation is added
    private int CalcResult() {
        int sum = 0;
        foreach (PartsBehavior part in container_)
            sum += part.part_id + 1;
        return sum - 1;
    }

    private string GetUpdateText(bool show_res) {
        if (container_.Count == 0) 
            return "";
        String nt = "";
        foreach (PartsBehavior cp in container_) 
            nt += (cp.part_id + 1).ToString() + "+";
        int length = nt.Length;
        nt = nt.Substring(0, length - 1);
        if (show_res)
            nt += "=" + (CalcResult() + 1);
        return nt;
    }
}
