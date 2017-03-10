using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Util;
using AvatarLib;

public class WatchDog : GenericSingleton<WatchDog> {

    private List<LogData> logs_;

    // probably need this
    private static AvatarProfile user;

    void Start() {
        logs_ = new List<LogData>();
    }

    [Serializable]
    public class LogData {
        public string description;
        public int score;
        public double quality;
        public enum LetterGrade { A = 0, B = 1, C = 2, D = 3, F = 4 }
        LetterGrade grade;
    }

    public void AddLog(LogData log) {
        logs_.Add(log);
    }

    public void ClearLog() {
        logs_.Clear();
    }

    // TODO: 
    // determine a proper path for log file and make it more effecient
    private void SaveToText() {
        Xml.SaveXml("", logs_);
        ClearLog();
    }
}