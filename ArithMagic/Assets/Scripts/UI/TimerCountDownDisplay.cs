using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class TimerCountDownDisplay : GenericSingleton<TimerCountDownDisplay> {

    private float time_left_;
    private TextMesh text_mesh_;

    private bool is_ended = false;

    private const int kLastTime = 10;

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

            if (time_left_ < kLastTime) {
                text_mesh_.color = new Color(0.85f, 0.41f, 0.35f);
                float scale = 0.025f + 0.003f * Mathf.Sin(2 * Mathf.PI * time_left_);
                text_mesh_.transform.localScale = new Vector3(scale, scale, 1.0f);
            }

        } else if (!is_ended) {
            is_ended = true;

            ToolBoxBehavior.Instance.OnTimeUp();
        }
    }

    public void ResetTimer() {
        time_left_ = ToolBoxBehavior.kTimerTime;
    }
}
