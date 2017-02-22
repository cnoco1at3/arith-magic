using UnityEngine;

namespace InteractLib {
    public abstract class Dragable : MonoBehaviour, IInteractable {

        public virtual void OnTouchEnter(Vector3 touch_pos) { }

        public virtual void OnTouchStay(Vector3 touch_pos) {
            Vector3 world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, transform.position.z - Camera.main.transform.position.z));
            transform.position = world_pos;
        }

        public virtual void OnTouchExit(Vector3 touch_pos) { }
    }
}
