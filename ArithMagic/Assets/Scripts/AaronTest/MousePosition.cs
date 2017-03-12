using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class MousePosition : GenericSingleton<MousePosition> {
    private Vector2 xrayPos;

    // Update is called once per frame
    void Update() {
        xrayPos = Input.mousePosition;
        xrayPos = Camera.main.ScreenToWorldPoint(xrayPos);
        transform.position = new Vector3(xrayPos.x, xrayPos.y, -1);
    }
}
