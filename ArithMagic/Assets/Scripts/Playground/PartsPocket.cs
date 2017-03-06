using UnityEngine;
using System;
using Util;
using InteractLib;

public class PartsPocket : MonoBehaviour, IInteractable {
    [SerializeField]
    private int part_id_ = 0;

    // NOTE: 2 STEP GENERATION
    // 1. INSTANTIATE A NEW PART
    // 2. HANDLE THE OCCUPATION OF INTERACT MANAGER
    public virtual void OnTouchEnter(Vector3 touch_pos) {
        // Release occupation of this object
        InteractManager.Instance.ReleaseOccupation(this);

        // Instantiate parts
        GameObject part = null;
        try {
            Vector3 pos = transform.position;
            pos.z -= 0.1f;
            part = (GameObject)Instantiate(PartsCluster.Instance.parts[part_id_], pos, Quaternion.identity);
        }
        catch (ArgumentNullException e) {
            Debug.LogException(e);
        }
        IInteractable src = part.GetComponent<IInteractable>();
        if (src == null) {
            ScriptDebug.Log(this, 40, "Instantiated a non-interactable object");
            Destroy(part);
        }

        // Handle occupation problem
        InteractManager.Instance.RequireOccupation(src);
        src.OnTouchEnter(touch_pos);
    }

    // Do nothing
    public virtual void OnTouchStay(Vector3 touch_pos) { }

    // Do nothing
    public virtual void OnTouchExit(Vector3 touch_pos) { }
}
