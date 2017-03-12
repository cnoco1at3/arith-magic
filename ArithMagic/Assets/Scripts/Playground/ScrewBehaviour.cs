using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using DG.Tweening;

public class ScrewBehaviour : Clickable {

    private ScrewContainer container_;

    private Vector3 origin_;
    private bool is_in_ = false;

    void Start() {
        container_ = FindObjectOfType<ScrewContainer>();
        if (container_ == null)
            Debug.Log("no screw container in this scene");
        origin_ = transform.position;
        Debug.Log(origin_);
    }

    public override void ClickEvent() {
        MoveToContainer();
    }

    public void ReturnFromContainer() {
        is_in_ = false;
        transform.DOMove(origin_, 0.5f);
    }

    private void MoveToContainer() {
        try {
            Vector3 pos = container_.GetNextSlotPosition();
            if (!container_.IsFull() && !is_in_) {
                container_.ObtainSlot(this);
                transform.DOMove(pos, 0.5f);
                is_in_ = true;
            }
        } catch (NullReferenceException e) {
            container_ = FindObjectOfType<ScrewContainer>();
            Debug.LogException(e);
        }
    }
}