using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;

public class MapScroller : Dragable {

    private Vector3 prev_world_pos;

    public override void OnTouchEnter(Vector3 touch_pos) {
        prev_world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, transform.position.z - Camera.main.transform.position.z));
    }

    public override void OnTouchStay(Vector3 touch_pos) {
        Vector3 world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, transform.position.z - Camera.main.transform.position.z));
        Vector3 delta_pos = world_pos - prev_world_pos;
        prev_world_pos = world_pos;

        // TODO: Add soft bound here
        transform.position = new Vector3(transform.position.x + delta_pos.x, transform.position.y, transform.position.z);
    }
}
