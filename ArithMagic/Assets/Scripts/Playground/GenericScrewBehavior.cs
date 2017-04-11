using System;
using System.Collections;
using UnityEngine;
using InteractLib;
using SoundLib;
using DG.Tweening;

public class GenericScrewBehavior : Clickable {

    [SerializeField]
    protected int screw_id_;

    [SerializeField]
    protected GameObject prev_screw_;

    [SerializeField]
    protected AudioClip sfx_clip_;

    protected ScrewContainer container_;
    protected Collider collider_;
    protected AudioSource soundEffect;

    // Use this for initialization
    void Start() {
        container_ = ToolBoxBehavior.Instance.GetContainerById(screw_id_);
        collider_ = GetComponent<Collider>();
    }

    public override void ClickEvent() {
        MoveToContainer();
    }

    public void MoveToContainer() {
        SoundManager.Instance.PlaySFX(sfx_clip_, false);

        try {
            if (!container_.is_full) {
                Vector3 pos = container_.GetNextSlotPosition();
                container_.ObtainSlot(this);

                // do animation here
                if (prev_screw_ != null)
                    StartCoroutine(ClusterAnim(pos));
                else
                    transform.DOMove(pos, 0.5f);

                collider_.enabled = false;
            }
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
    }

    private IEnumerator ClusterAnim(Vector3 pos) {
        GameObject[] ones = new GameObject[9];

        for (int i = 0; i < ones.Length; ++i) {
            ones[i] = Instantiate(prev_screw_, transform.position, Quaternion.identity);
            ones[i].GetComponent<Collider>().enabled = false;
            Vector3 randoff = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));

            SoundManager.Instance.PlaySFX(sfx_clip_, false);
            ones[i].transform.DOLocalMove(ones[i].transform.localPosition + randoff, 0.5f);
        }

        yield return new WaitForSeconds(0.8f);

        for (int i = 0; i < ones.Length; ++i)
            ones[i].transform.DOMove(pos, 0.8f);

        SoundManager.Instance.PlaySFX(sfx_clip_, false);
        transform.DOMove(pos, 0.8f);
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < ones.Length; ++i)
            Destroy(ones[i]);
    }
}
