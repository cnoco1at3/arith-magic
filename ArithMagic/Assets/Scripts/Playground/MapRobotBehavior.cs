﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using SoundLib;
using DG.Tweening;

public class MapRobotBehavior : GenericSingleton<MapRobotBehavior> {

    [SerializeField]
    private AudioClip move_sfx_;

    private static int docked_id_ = -1;

    public AudioClip[] mapBackground; 

    void Start() {
        SoundManager.Instance.PlayBGM(mapBackground[UnityEngine.Random.Range(0, mapBackground.Length)]);
        LockBoxBehavior box = LevelCluster.Instance.GetLockBoxById(docked_id_);
        if (docked_id_ == -1)
            docked_id_ = 0;
        if (box != null)
            transform.localPosition = box.GetTargetLocalPosition();
        int tmp = GameController.GetCurrentLevel() + 1;
        if (tmp == LevelCluster.Instance.GetLockBoxSize())
            tmp -= 1;
        docked_id_ = tmp;
        transform.localPosition = LevelCluster.Instance.GetLockBoxById(tmp).GetTargetLocalPosition();
        SoundManager.Instance.PlaySFX(move_sfx_);
    }

    public static int GetDockedId() { return docked_id_ > 0 ? docked_id_ : 0; }

    public void MoveToPosition(LockBoxBehavior target) {
        if (target == LevelCluster.Instance.GetLockBoxById(docked_id_)) {
            GameController.EnterNextLevel();
        } else {
            SoundManager.Instance.PlaySFX(move_sfx_);
            transform.DOLocalMove(target.GetTargetLocalPosition(), 2.0f);
            docked_id_ = target.GetLockBoxId();
            StartCoroutine(AdvanceLevel());
        }
    }

    private IEnumerator AdvanceLevel() {
        yield return new WaitForSeconds(2.0f);
        GameController.EnterNextLevel();
    }
}
