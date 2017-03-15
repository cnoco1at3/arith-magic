using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using Util;

public class MapScroller : Dragable {

    private Vector3 prev_world_pos;
    private Vector3 prev_world_vel;
    private static float hooke;
    private static float decay;

    void Start() {
        try {
            hooke = (float)ConstantTweakTool.Instance.const_dict["HookeFactor"];
            decay = (float)ConstantTweakTool.Instance.const_dict["ScrollDecay"];
        }
        catch (KeyNotFoundException e) {
            Debug.LogException(e);
        }
    }

    void FixedUpdate() {
        prev_world_vel *= decay;
        float f = prev_world_vel.x;
        if (transform.position.x > 0)
            f = -hooke * (transform.position.x);
        else if (transform.position.x < -30)
            f = -hooke * (30 + transform.position.x);

        transform.position = new Vector3(transform.position.x + f, transform.position.y, transform.position.z);
    }

    public override void OnTouchEnter(Vector3 touch_pos) {
        prev_world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, transform.position.z - Camera.main.transform.position.z));
    }

    public override void OnTouchStay(Vector3 touch_pos) {
        Vector3 world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, transform.position.z - Camera.main.transform.position.z));
        Vector3 delta_pos = world_pos - prev_world_pos;
        prev_world_vel = world_pos - prev_world_pos;
        prev_world_pos = world_pos;

        // TODO: Add soft bound here
        transform.position = new Vector3(transform.position.x + delta_pos.x, transform.position.y, transform.position.z);
    }
}
