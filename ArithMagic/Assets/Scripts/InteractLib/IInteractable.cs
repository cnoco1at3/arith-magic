using UnityEngine;

namespace InteractLib {
    public interface IInteractable {
        void OnTouchEnter();
        void OnTouchStay(Vector3 touch_pos);
        void OnTouchExit();
    }
}
