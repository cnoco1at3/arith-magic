using UnityEngine;
using System.Collections;
using Util;

public class InteractManager : GenericSingleton<InteractManager> {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        Vector3 touch_pos = new Vector3();

#if UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Canceled && Input.GetTouch(0).phase != TouchPhase.Ended) {
            touch_pos = Input.GetTouch(0).position;
        }
#endif

#if UNITY_EDITOR
        if (Input.GetMouseButton(0)) {
            touch_pos = Input.mousePosition;
        }
#endif

        Ray ray = Camera.main.ScreenPointToRay(touch_pos);
        RaycastHit rayhit;

        if (Physics.Raycast(ray, out rayhit)) {

            Transform target = rayhit.collider.transform;

            Vector3 world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, target.position.z));
            target.position = new Vector3(world_pos.x, world_pos.y, target.position.z);
        }
    }
}
