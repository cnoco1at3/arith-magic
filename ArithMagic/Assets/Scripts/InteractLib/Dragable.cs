using UnityEngine;

namespace InteractLib {
    public abstract class Dragable : MonoBehaviour, IInteractable {

        public virtual void OnTouchEnter() { }

        public virtual void OnTouchStay(Vector3 touch_pos) {
            Vector3 world_pos = Camera.main.ScreenToWorldPoint(new Vector3(touch_pos.x, touch_pos.y, transform.position.z));
            transform.position = new Vector3(world_pos.x, world_pos.y, transform.position.z);
        }

        public virtual void OnTouchExit() { }
    }
}
