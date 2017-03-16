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
        container_ = GameObject.Find("screwBoxOnes").GetComponent<ScrewContainer>();
        collider_ = GetComponent<Collider>();
        origin_ = transform.position;
    }

    public override void ClickEvent() {
        MoveToContainer();
    }

    public virtual void ReturnFromContainer() {
        is_in_ = false;
        collider_.enabled = true;
        transform.DOMove(origin_, 0.5f);
    }

    private void MoveToContainer() {
        SoundManager.Instance.PlaySFX(sfx_clip_);
        
        try {
            Vector3 pos = container_.GetNextSlotPosition();
            if (!container_.IsFull() && !is_in_) {
                container_.ObtainSlot(this);
                transform.DOMove(pos, 0.5f);
                is_in_ = true;
                collider_.enabled = false;
            }
        } catch (NullReferenceException e) {
            container_ = FindObjectOfType<ScrewContainer>();
            Debug.LogException(e);
        }
    }
}