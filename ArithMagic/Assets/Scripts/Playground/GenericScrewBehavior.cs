using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using SoundLib;

public class GenericScrewBehavior : ScrewBehaviour {

    // Use this for initialization
    void Start() {
        container_ = GameObject.Find("screwBoxTens").GetComponent<ScrewContainer>();
        collider_ = GetComponent<Collider>();
        origin_ = transform.position;
    }

    public override void ClickEvent() {
        MoveToContainer();
    }

    private void MoveToContainer() {
        SoundManager.Instance.PlaySFX(sfx_clip_);

        try {
            Vector3 pos = container_.GetNextSlotPosition();
            if (!container_.IsFull() && !is_in_) {
                container_.ObtainSlot(this);

                // do animation here

                is_in_ = true;
                collider_.enabled = false;
            }
        } catch (NullReferenceException e) {
            container_ = GameObject.Find("screwBoxTens").GetComponent<ScrewContainer>();
            Debug.LogException(e);
        }
    }

    
}
