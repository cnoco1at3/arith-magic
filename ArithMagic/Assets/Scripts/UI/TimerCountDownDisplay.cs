using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class TimerCountDownDisplay : GenericSingleton<TimerCountDownDisplay> {

    private float time_left_;
    private TextMesh text_mesh_;

    void Awake() {
        Renderer rend = GetComponent<Renderer>();
        rend.sortingOrder = 200;
    }

    private void Start() {
        time_left_ = ToolBoxBehavior.kTimerTime;
        text_mesh_ = GetComponent<TextMesh>();
    }

    private void FixedUpdate() {
        if (time_left_ > 0) {
            time_left_ -= Time.fixedDeltaTime;
            text_mesh_.text = ((int)time_left_).ToString();
        } else {
            ToolBoxBehavior.Instance.OnTimeUp();
        }
    }

    public void ResetTimer() {
        time_left_ = ToolBoxBehavior.kTimerTime;
    }
}
