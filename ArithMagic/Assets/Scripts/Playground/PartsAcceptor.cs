using UnityEngine;
using System.Collections;

public class PartsAcceptor : MonoBehaviour {

    [SerializeField]
    private int acc_part_id = 0;

    [SerializeField]
    private GameObject red_light_;
    [SerializeField]
    private GameObject green_light_;

    // NOTE: here we need to set the point where we want the screw snap to
    [SerializeField]
    private Transform accept_point_;

    private PartsBehavior pb;

    private bool is_occupied = false;

    void Start() {
        if (accept_point_ == null)
            accept_point_ = transform;
    }

    // NOTE: if we didn't define a certain position we want the screw move to, it will stay where it is
    public virtual Vector3 GetAcceptPoint(Vector3 position) {
        return accept_point_.position;
    }

    public virtual Vector3 GetScaleFactor() {
        return new Vector3(0.5f, 0.5f, 0.5f);
    }

    public virtual bool IsFinished() {
        return is_occupied;
    }

    public virtual void OnPartEnter(PartsBehavior part) {
        is_occupied = true;
        red_light_.SetActive(false);
        green_light_.SetActive(false);

        if (part.part_id == acc_part_id)
            StartCoroutine(ProblemSolved());
        else
            red_light_.SetActive(true);

        if (pb != null)
            pb.MoveBackToBox();
        pb = part;
    }

    public virtual void OnPartExit(PartsBehavior part) {
        is_occupied = false;
        red_light_.SetActive(false);
        green_light_.SetActive(false);
    }

    private IEnumerator ProblemSolved() {
        //add effects, positive reinforcment
        green_light_.SetActive(true);
        yield return new WaitForSeconds(2);
        XRayCameraBehavior.Instance.CheckParts(true);
        Destroy(transform.root.gameObject);
    }
}
