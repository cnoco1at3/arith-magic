using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using Util;
using DG.Tweening;

public class MapScroller : Dragable {

    public mapMovement firstBox;

    private Vector3 prev_world_pos;
    private Vector3 prev_world_vel;
    private static float hooke;
    private static float decay;

    // -7.5, 7.5
    // -19

    void Start() {
        try {
            hooke = (float)ConstantTweakTool.Instance.const_dict["HookeFactor"];
            decay = (float)ConstantTweakTool.Instance.const_dict["ScrollDecay"];

            // hacky way to implement the effect
            transform.DOMoveY(-34, 2.0f);
            firstBox.ClickEvent();
        }
        catch (KeyNotFoundException e) {
            Debug.LogException(e);
        }
    }

    void FixedUpdate() {
        prev_world_vel *= decay;
        float f = prev_world_vel.y;
        if (transform.position.y > 12)
            f = hooke * (12 - transform.position.y);
        else if (transform.position.y < -48)
            f = -hooke * (48 + transform.position.y);
        float x = Mathf.Lerp(-7.5f, 7.5f, (Mathf.Clamp(transform.position.y, -28.0f, -10.0f) + 10.0f) / -18.0f);

        transform.position = new Vector3(x, transform.position.y + f, transform.position.z);
    }

    public override void OnTouchEnter(Vector3 touch_pos) {
        prev_world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, transform.position.z - Camera.main.transform.position.z));
    }

    public override void OnTouchStay(Vector3 touch_pos) {
        Vector3 world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, transform.position.z - Camera.main.transform.position.z));
        Vector3 delta_pos = world_pos - prev_world_pos;
        prev_world_vel = world_pos - prev_world_pos;
        prev_world_pos = world_pos;

        float x = Mathf.Lerp(-7.5f, 7.5f, (Mathf.Clamp(transform.position.y, -28.0f, -10.0f) + 10.0f) / -18.0f);
        transform.position = new Vector3(x, transform.position.y + delta_pos.y, transform.position.z);
    }
}
