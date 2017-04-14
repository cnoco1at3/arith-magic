using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using Util;
using DG.Tweening;
using SoundLib;

public class MapScroller : Dragable {

    public LockBoxBehavior first_box;

    private Vector3 prev_world_pos;
    private Vector3 prev_world_vel;
    private static float kHooke;
    private static float kDecay;
    private SpriteRenderer sprite_;

    public AudioClip mapBackground;

    void Start() {
        SoundManager.Instance.PlayBGM(mapBackground);
        try {
            kHooke = (float)ConstantTweakTool.Instance["HookeFactor"];
            kDecay = (float)ConstantTweakTool.Instance["ScrollDecay"];

            sprite_ = GetComponent<SpriteRenderer>();

            // hacky way to implement the effect
            LockBoxBehavior center = LevelCluster.Instance.GetLockBoxById(MapRobotBehavior.GetDockedId());
            if (center == null)
                center = LevelCluster.Instance.GetLockBoxById(0);
            transform.DOMoveY(transform.position.y - center.transform.position.y, 2.0f);
        } catch (KeyNotFoundException e) {
            Debug.LogException(e);
        }
    }

    void FixedUpdate() {
        prev_world_vel *= kDecay;
        float f = prev_world_vel.y;

        float bound = sprite_.bounds.extents.y - 10.0f;

        if (transform.position.y > bound)
            f = kHooke * (bound - transform.position.y);
        else if (transform.position.y < -bound)
            f = -kHooke * (bound + transform.position.y);

        //float x = SlideX();

        transform.position = new Vector3(transform.position.x, transform.position.y + f, transform.position.z);
    }

    public override void OnTouchEnter(Vector3 touch_pos) {
        prev_world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, transform.position.z - Camera.main.transform.position.z));
    }

    public override void OnTouchStay(Vector3 touch_pos) {
        Vector3 world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, transform.position.z - Camera.main.transform.position.z));
        Vector3 delta_pos = world_pos - prev_world_pos;
        prev_world_vel = world_pos - prev_world_pos;
        prev_world_pos = world_pos;

        //float x = SlideX();
        transform.position = new Vector3(transform.position.x, transform.position.y + delta_pos.y, transform.position.z);
    }

    private float SlideX() {
        return Mathf.Lerp(7.5f, -7.5f, (Mathf.Clamp(transform.position.y + 16.0f, -8.0f, 8.0f) + 8.0f) / 16.0f);
    }
}
