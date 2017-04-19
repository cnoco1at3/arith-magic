using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class LevelCluster : GenericSingleton<LevelCluster> {

    private LockBoxBehavior[] lock_boxes_;

    void Start() {
        lock_boxes_ = FindObjectsOfType<LockBoxBehavior>();
        try {
            Array.Sort(lock_boxes_);
            for (int i = 0; i < lock_boxes_.Length; ++i)
                if (lock_boxes_[i] != null) {
                    lock_boxes_[i].SetLockBoxId(i);
                    if (i <= GameController.GetCurrentLevel() + 1)
                        lock_boxes_[i].SetUnlocked();
                }
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
    }

    public LockBoxBehavior GetLockBoxById(int id) {
        if (id >= 0 && id < lock_boxes_.Length)
            return lock_boxes_[id];
        if (lock_boxes_.Length > 0 && id > lock_boxes_.Length)
            return lock_boxes_[lock_boxes_.Length - 1];
        return null;
    }

    public int GetLockBoxSize() {
        return lock_boxes_.Length;
    }
}