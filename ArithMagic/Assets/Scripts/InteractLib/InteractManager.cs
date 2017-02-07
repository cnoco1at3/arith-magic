using UnityEngine;
using InteractLib;
using Util;

public class InteractManager : GenericSingleton<InteractManager> {

    private bool is_occupied_ = false;
    private IInteractable current_;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        Vector3 touch_pos = new Vector3();
        bool is_touched = false;

#if UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Canceled && Input.GetTouch(0).phase != TouchPhase.Ended) {
            touch_pos = Input.GetTouch(0).position;
            is_touched = true;
        }
#endif

#if UNITY_EDITOR
        if (Input.GetMouseButton(0)) {
            touch_pos = Input.mousePosition;
            is_touched = true;
        }
#endif

        else {
            if (current_ != null)
                current_.OnTouchExit();
            ReleaseOccupation(current_);
        }

        if (is_touched) {
            if (!is_occupied_) {
                Ray ray = Camera.main.ScreenPointToRay(touch_pos);
                RaycastHit rayhit;

                if (Physics.Raycast(ray, out rayhit)) {
                    IInteractable src = rayhit.collider.GetComponent<IInteractable>();
                    if (src != null) {
                        RequireOccupation(src);
                        src.OnTouchEnter();
                    }
                }
            }

            if (is_occupied_)
                current_.OnTouchStay(touch_pos);
        }
    }

    /// <summary>
    /// Require the interaction occupation.
    /// </summary>
    /// <param name="src">The source.</param>
    /// <returns></returns>
    public bool RequireOccupation(IInteractable src) {
        if (is_occupied_)
            return false;

        is_occupied_ = true;
        current_ = src;
        return true;
    }

    /// <summary>
    /// Releases the occupation.
    /// </summary>
    /// <param name="src">The source.</param>
    /// <returns></returns>
    public bool ReleaseOccupation(IInteractable src) {
        if (current_ != src)
            return false;

        is_occupied_ = false;
        current_ = null;
        return true;
    }
}
