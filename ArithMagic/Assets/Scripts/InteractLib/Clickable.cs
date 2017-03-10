using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractLib {
    public abstract class Clickable : MonoBehaviour, IInteractable {

        private const float kTimeThreshold = 1.0f;

        private float enter_;

        public virtual void OnTouchEnter(Vector3 touch_pos) { enter_ = Time.time; }

        public virtual void OnTouchStay(Vector3 touch_pos) { }
        public virtual void OnTouchExit(Vector3 touch_pos) {
            if (Time.time - enter_ < kTimeThreshold)
                ClickEvent();
        }

        public virtual void ClickEvent() { }
    }
}
