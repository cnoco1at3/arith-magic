using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapParallel : MonoBehaviour {

    [SerializeField]
    Transform background;

    [SerializeField]
    Transform foreground;

    [SerializeField]
    Transform robot;

    private Vector3 origin_;
    private Vector3 bg_origin_;
    private Vector3 fg_origin_;

    private const float decay = 0.5f;

    // Use this for initialization
    void Start() {
        origin_ = transform.position;
        bg_origin_ = background.position;
        fg_origin_ = foreground.position;

        Vector3 offset = MapRobotBehavior.Instance.transform.localPosition;
        offset.x = offset.z = 0;
        transform.position -= offset;
    }

    // Update is called once per frame
    void Update() {
        Vector3 offset = transform.position - origin_;
        background.position = bg_origin_ + decay * offset;
        foreground.position = fg_origin_ + (1 / decay) * offset;
    }
}
