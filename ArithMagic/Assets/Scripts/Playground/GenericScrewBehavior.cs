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
        container_ = ToolBoxBehavior.Instance.GetContainerById(1);
        collider_ = GetComponent<Collider>();
    }

    public override void ClickEvent() {
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
        } catch (NullReferenceException e) {
            container_ = GameObject.Find("screwBoxTens").GetComponent<ScrewContainer>();
            collider_ = GetComponent<Collider>();
        }
    }

    private IEnumerator TensAnim(Vector3 pos) {
        GameObject[] ones = new GameObject[9];

        for (int i = 0; i < ones.Length; ++i) {
            ones[i] = Instantiate(one_, transform.position, Quaternion.identity);
            ones[i].GetComponent<Collider>().enabled = false;
            Vector3 randoff = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));

            SoundManager.Instance.PlaySFX(sfx_clip_);
            ones[i].transform.DOLocalMove(ones[i].transform.localPosition + randoff, 0.5f);
        }

        yield return new WaitForSeconds(0.8f);

        for (int i = 0; i < ones.Length; ++i)
            ones[i].transform.DOMove(pos, 0.8f);

        SoundManager.Instance.PlaySFX(sfx_clip_);
        transform.DOMove(pos, 0.8f);
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < ones.Length; ++i)
            Destroy(ones[i]);
    }
}
