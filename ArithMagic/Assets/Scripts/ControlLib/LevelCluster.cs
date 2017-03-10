using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCluster : ICsvObject {

    private Hashtable level_table_;

    public LevelCluster() {
        level_table_ = new Hashtable();
    }

    public bool IsHeader(string[] text) {
        return false;
    }

    public void StartHeader(string[] header) {
    }

    public void EndHeader() {

    }

    public void AddElement(string[] element) {

    }

    public int GetLevelCount() {
        return 0;
    }
}
