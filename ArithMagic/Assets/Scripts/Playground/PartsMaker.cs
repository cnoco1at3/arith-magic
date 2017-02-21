using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartsMaker : PartsAcceptor {

    private List<PartsBehavior> container_;

    void Start() {
        container_ = new List<PartsBehavior>();
    }

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

    public void OnMake() {
        int res = CalcResult();
        foreach (PartsBehavior part in container_)
            Destroy(part.gameObject);
        container_.Clear();
        Instantiate(PartsCluster.Instance.parts[res], transform.position - new Vector3(0, 0, 1.5f), Quaternion.identity);
    }

    private int CalcResult() {
        int sum = 0;
        foreach(PartsBehavior part in container_) 
            sum += part.part_id + 1;
        return sum - 1;
    }
}
