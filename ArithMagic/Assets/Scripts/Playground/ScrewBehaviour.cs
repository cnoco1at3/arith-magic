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

    protected bool is_in_ = false;

    protected Collider collider_;
    protected AudioSource soundEffect;

    void Start() {
        container_ = ToolBoxBehavior.Instance.GetContainerById(0);
        collider_ = GetComponent<Collider>();
    }

    public override void ClickEvent() {
        if (!is_in_) {
            Vector3 pos = container_.GetNextSlotPosition();
            MoveToContainer();
        }
    }

    public void SetInStatus(bool is_in) {
        is_in_ = is_in;
    }

    public virtual void MoveToContainer() {
        SoundManager.Instance.PlaySFX(sfx_clip_);

        try {
            if (!container_.IsFull()) {
                Vector3 pos = container_.GetNextSlotPosition();
                container_.ObtainSlot(this);
                transform.DOMove(pos, 0.5f);
                collider_.enabled = false;
                is_in_ = true;
            }
        } catch (NullReferenceException e) {
            container_ = FindObjectOfType<ScrewContainer>();
            collider_ = GetComponent<Collider>();
        }
    }
}