using UnityEngine;
using System.Collections;

public class PartsAcceptor : MonoBehaviour {

    public Transform accept_point;

    private bool is_occupied = false;

    void Start() {
        if (accept_point == null)
            accept_point = transform;
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
