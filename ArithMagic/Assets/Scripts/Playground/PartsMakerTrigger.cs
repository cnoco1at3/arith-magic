using UnityEngine;
using System.Collections;
using InteractLib;

public class PartsMakerTrigger : MonoBehaviour, IInteractable {

    [SerializeField]
    private PartsMaker maker_;

    public virtual void OnTouchEnter(Vector3 touch_pos) {
        maker_.OnMake();
    }

    public virtual void OnTouchStay(Vector3 touch_pos) { }
    public virtual void OnTouchExit(Vector3 touch_pos) { }
}
