using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using SoundLib;
using DG.Tweening;

public class GenericScrewBehavior : ScrewBehaviour {

    [SerializeField]
    private GameObject one_;

    // Use this for initialization
    void Start() {
        container_ = GameObject.Find("screwBoxTens").GetComponent<ScrewContainer>();
        collider_ = GetComponent<Collider>();
        origin_ = transform.position;
    }

    public override void ClickEvent() {
        Debug.Log("clicked");
        Vector3 pos = container_.GetNextSlotPosition();
        MoveToContainer();
    }

    public override void MoveToContainer() {
        SoundManager.Instance.PlaySFX(sfx_clip_);

        try {
            if (!container_.IsFull() && !is_in_) {
                Vector3 pos = container_.GetNextSlotPosition();
                container_.ObtainSlot(this);

                // do animation here
                StartCoroutine(TensAnim(pos));

                is_in_ = true;
                collider_.enabled = false;
            }
        }
        catch (NullReferenceException e) {
            container_ = GameObject.Find("screwBoxTens").GetComponent<ScrewContainer>();
            collider_ = GetComponent<Collider>();
        }
    }

    private IEnumerator TensAnim(Vector3 pos) {
        GameObject[] ones = new GameObject[10];
        for (int i = 0; i < 10; ++i) {
            ones[i] = Instantiate(one_, transform.position, Quaternion.identity);

            SoundManager.Instance.PlaySFX(sfx_clip_);
            ones[i].transform.DOMove(pos, 0.5f);

            yield return new WaitForSeconds(0.05f);
        }
        SoundManager.Instance.PlaySFX(sfx_clip_);
        transform.DOMove(pos, 0.5f);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; ++i)
            Destroy(ones[i]);
    }
}
