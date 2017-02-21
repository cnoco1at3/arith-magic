using UnityEngine;
using System.Collections;

public class PartsAcceptor : MonoBehaviour {

    [SerializeField]
    private Transform accept_point;

    private bool is_occupied = false;

    public virtual Vector3 GetAcceptPoint(Vector3 position) {
        if (accept_point != null)
            return accept_point.position;
        return position;
    }

    public bool IsOccupied() {
        return is_occupied;
    }

    public virtual void OnPartEnter(PartsBehavior part) {
        is_occupied = true;
    }

    public virtual void OnPartExit(PartsBehavior part) {
        is_occupied = false;
    }
}
