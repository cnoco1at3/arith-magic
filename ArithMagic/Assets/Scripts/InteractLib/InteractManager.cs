using System.Collections;
using UnityEngine;
using InteractLib;
using Util;

/// <summary>
/// This help detecting if the user touch the screen and it handle different input for different platforms
/// </summary>
public class InteractManager : PersistentSingleton<InteractManager> {

    public bool IsTouched { get; private set; }

    private bool is_occupied_ = false;
    private static bool locker_ = false;
    private IInteractable current_;
    private Vector3 touch_pos;

    /// <summary>
    /// Get the input from either mouse or touch screen and interprete them into Enter, Stay and Exit events.
    /// </summary>
    void Update() {
        if (locker_)
            return;

        // NOTE: This snippet get the touch position based on the platform
        IsTouched = false;
#if UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Canceled && Input.GetTouch(0).phase != TouchPhase.Ended) {
            touch_pos = Input.GetTouch(0).position;
            IsTouched = true;
        }
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButton(0)) {
            touch_pos = Input.mousePosition;
            IsTouched = true;
        }
#endif
        else {
            if (current_ != null)
                current_.OnTouchExit(touch_pos);
            ReleaseOccupation(current_);
            touch_pos = new Vector3();
        }

        // NOTE: Determine whether this touch is just enter or is a stay state touch
        if (IsTouched) {
            if (!is_occupied_) {
                Ray ray = Camera.main.ScreenPointToRay(touch_pos);
                RaycastHit rayhit;

                if (Physics.Raycast(ray, out rayhit)) {
                    IInteractable src = rayhit.collider.GetComponent<IInteractable>();
                    if (src != null) {
                        RequireOccupation(src);
                        src.OnTouchEnter(touch_pos);
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

    public static bool LockInteraction() {
        locker_ = true;
        return locker_;
    }

    public static bool ReleaseInteraction() {
        locker_ = false;
        return !locker_;
    }

    public void LockInteractionForSeconds(float time) {
        StartCoroutine(LockCoroutine(time));
    }

    private IEnumerator LockCoroutine(float time) {
        LockInteraction();
        yield return new WaitForSeconds(time);
        ReleaseInteraction();
    }
}
