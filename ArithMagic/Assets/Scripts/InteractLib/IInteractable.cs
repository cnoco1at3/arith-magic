using UnityEngine;

namespace InteractLib {
    public interface IInteractable {
        void OnTouchEnter(Vector3 touch_pos);
        void OnTouchStay(Vector3 touch_pos);
        void OnTouchExit(Vector3 touch_pos);
    }
}
