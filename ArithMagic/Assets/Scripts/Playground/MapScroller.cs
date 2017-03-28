using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using Util;
using DG.Tweening;

public class MapScroller : Dragable {

    public MapMovement firstBox;

    private Vector3 prev_world_pos;
    private Vector3 prev_world_vel;
    private static float kHooke;
    private static float kDecay;
    private SpriteRenderer sprite_;

    // -7.5, 7.5
    // -19

    void Start() {
        try {
            kHooke = (float)ConstantTweakTool.Instance.const_dict["HookeFactor"];
            kDecay = (float)ConstantTweakTool.Instance.const_dict["ScrollDecay"];

            sprite_ = GetComponent<SpriteRenderer>();

            // hacky way to implement the effect
            /*
            transform.DOMoveY(-34, 2.0f);
            firstBox.ClickEvent();
            */
        } catch (KeyNotFoundException e) {
            Debug.LogException(e);
        }
    }

    void FixedUpdate() {
        prev_world_vel *= kDecay;
        float f = prev_world_vel.y;

        // 12 -48
        float bound = sprite_.bounds.extents.y - 15.0f;

        if (transform.position.y > bound)
            f = kHooke * (bound - transform.position.y);
        else if (transform.position.y < -bound)
            f = -kHooke * (bound + transform.position.y);

        float x = Mathf.Lerp(7.5f, -7.5f, (Mathf.Clamp(transform.position.y, -10.0f, 10.0f) + 10.0f) / 20.0f);

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

        float x = Mathf.Lerp(7.5f, -7.5f, (Mathf.Clamp(transform.position.y, -10.0f, 10.0f) + 10.0f) / 20.0f);
        transform.position = new Vector3(x, transform.position.y + delta_pos.y, transform.position.z);
    }
}
