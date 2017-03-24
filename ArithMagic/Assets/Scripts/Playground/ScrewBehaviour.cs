using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;
using SoundLib;
using DG.Tweening;

public class ScrewBehaviour : Clickable {

    [SerializeField]
    protected AudioClip sfx_clip_;

    protected ScrewContainer container_;

    protected Vector3 origin_;
    protected bool is_in_ = false;
    protected Collider collider_;
    protected AudioSource soundEffect;

    void Start() {
        container_ = ToolBoxBehavior.Instance.GetContainerById(0);
        collider_ = GetComponent<Collider>();
        origin_ = transform.position;
    }

    public override void ClickEvent() {
        Vector3 pos = container_.GetNextSlotPosition();
        MoveToContainer();
    }

    public virtual void ReturnFromContainer() {
        is_in_ = false;
        collider_.enabled = true;
        transform.DOMove(origin_, 0.5f);
    }

    public virtual void MoveToContainer() {
        SoundManager.Instance.PlaySFX(sfx_clip_);

        try {
            if (!container_.IsFull() && !is_in_) {
                Vector3 pos = container_.GetNextSlotPosition();
                container_.ObtainSlot(this);
                transform.DOMove(pos, 0.5f);
                is_in_ = true;
                collider_.enabled = false;
            }
        } catch (NullReferenceException e) {
            container_ = FindObjectOfType<ScrewContainer>();
            collider_ = GetComponent<Collider>();
        }
    }
}