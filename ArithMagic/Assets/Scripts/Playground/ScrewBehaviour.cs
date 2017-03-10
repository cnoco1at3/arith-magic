using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using DG.Tweening;

public class ScrewBehaviour : Clickable {

    private ScrewContainer container_;

    private Vector3 origin_;

    void Start() {
        container_ = FindObjectOfType<ScrewContainer>();
        if (container_ == null)
            Debug.Log("no screw container in this scene");
        origin_ = transform.position;
    }

    public override void ClickEvent() {
        MoveToContainer();
    }

    public void ReturnFromContainer() {
        transform.DOMove(origin_, 0.5f, true);
    }

    private void MoveToContainer() {
        try {
            Vector3 pos = container_.GetNextSlotPosition();
            if (!container_.IsFull()) {
                container_.ObtainSlot(this);
                transform.DOMove(pos, 0.5f, true);
            }
        }
        catch (NullReferenceException e) {
            Debug.LogException(e);
        }
    }
}