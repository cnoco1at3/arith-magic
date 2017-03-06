using UnityEngine;
using System.Collections;

public class PartsAcceptor : MonoBehaviour {

    [SerializeField]
    private ZoomController zoom;

    [SerializeField]
    private int acc_part_id = 0;

    // NOTE: here we need to set the point where we want the screw snap to
    [SerializeField]
    private Transform accept_point_;

    [SerializeField]
    private int layer_order_ = 0;

    private bool is_occupied = false;

    void Start() {
        if (accept_point_ == null)
            accept_point_ = transform;
        GameController.Instance.acceptors_.Add(this);
    }

    // NOTE: if we didn't define a certain position we want the screw move to, it will stay where it is
    public virtual Vector3 GetAcceptPoint(Vector3 position) {
        return accept_point_.position;
    }

    public virtual Vector3 GetScaleFactor() {
        return new Vector3(1.6f, 1.6f, 1.6f);
    }

    public virtual int GetLayerOrder() {
        return layer_order_;
    }

    public virtual bool IsValid(PartsBehavior pb) {
        return pb.part_id == acc_part_id && !is_occupied;
    }

    public virtual bool IsFinished() {
        return is_occupied;
    }

    public virtual void OnPartEnter(PartsBehavior part) {
        is_occupied = true;
        if (zoom != null)
            zoom.trigger_obj_.Add(part.gameObject);
        GameController.Instance.OnEndGame();
    }

    public virtual void OnPartExit(PartsBehavior part) {
        is_occupied = false;
        if (zoom != null)
            zoom.trigger_obj_.Remove(part.gameObject);
    }
}
