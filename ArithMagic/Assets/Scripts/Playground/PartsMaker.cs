using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartsMaker : PartsAcceptor {

    private List<PartsBehavior> container_;
    private List<int> prev_ids_;

    void Start() {
        container_ = new List<PartsBehavior>();
    }

    // TODO: now it's triggering by space key
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            OnMake();
    }

    public override void OnPartEnter(PartsBehavior part) {
        container_.Add(part);
    }

    public override void OnPartExit(PartsBehavior part) {
        container_.Remove(part);
    }

    // NOTE: call this function when the "MAKE" button is click or triggered
    public void OnMake() {
        // Only do this when there are more than one screw inside
        if (container_.Count > 1) {
            prev_ids_.Clear();
            int res = CalcResult();
            foreach (PartsBehavior part in container_) {
                Destroy(part.gameObject);
                prev_ids_.Add(part.part_id);
            }
            container_.Clear();
            Instantiate(PartsCluster.Instance.parts[res],
                transform.position - new Vector3(0, 0, 1.5f),
                Quaternion.identity);
        }
    }

    // TODO: we need to implement the recycle method here, include two steps
    // 1. destroy the part that is just put into the recycle bin
    // 2. get parts back from the prev ids list
    public void OnUndoMake() {

    }

    // TODO: rewrite this function when other operation is added
    private int CalcResult() {
        int sum = 0;
        foreach (PartsBehavior part in container_)
            sum += part.part_id + 1;
        return sum - 1;
    }
}
