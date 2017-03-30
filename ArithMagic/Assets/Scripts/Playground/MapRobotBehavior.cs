using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using DG.Tweening;

public class MapRobotBehavior : GenericSingleton<MapRobotBehavior> {

    private static int docked_id_ = -1;

    void Start() {
        LockBoxBehavior box = LevelCluster.Instance.GetLockBoxById(docked_id_);
        if (box != null)
            transform.localPosition = box.GetTargetLocalPosition();
    }

    public static int GetDockedId() { return docked_id_; }

    public void MoveToPosition(LockBoxBehavior target) {
        transform.DOLocalMove(target.GetTargetLocalPosition(), 2.0f);
        if (target == LevelCluster.Instance.GetLockBoxById(docked_id_)) {
            LevelController.Instance.AdvanceLevel();
        } else {
            docked_id_ = target.GetLockBoxId();
            StartCoroutine(AdvanceLevel());
        }
    }

    private IEnumerator AdvanceLevel() {
        yield return new WaitForSeconds(2.0f);
        LevelController.Instance.AdvanceLevel();
    }
}
