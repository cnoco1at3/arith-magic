using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class LevelController : GenericSingleton<LevelController> {

    private static int level_index_ = 0;

    // NOTE: Consider there are two ways we could build a level
    // 1. the level controller have list of levels and we only maintain the level index parameter here.
    // 2. the level controller accept a data package from outside and then build the level here (this might help reducing memory usage).

    public static int GetLevelIndex() { return level_index_; }

    public static bool IsEndOfPhase() { return false; }

    private void CleanLevel() { }

    private void BuildLevel() { }

    private void BuildLevel(LevelData data) { }

    private void TransLevel() {
        CleanLevel();
        BuildLevel();
        UpdateLevelIndex();
    }

    private void TransLevel(LevelData data) {
        CleanLevel();
        BuildLevel(data);
        UpdateLevelIndex(); // How?
    }

    private void UpdateLevelIndex() {
        level_index_ = 0;
    }
}

public class LevelData {
    int p1, p2;
    int ans;

    public LevelData(int p1, int p2, int ans) {
        this.p1 = p1;
        this.p2 = p2;
        this.ans = ans;
    }
}