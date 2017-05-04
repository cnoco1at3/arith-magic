using System;
using System.Collections;
using UnityEngine;
using InteractLib;
using SoundLib;
using DG.Tweening;

public class GenericScrewBehavior : Clickable {

    public bool add = true;

    [SerializeField]
    protected int screw_id_;

    [SerializeField]
    protected GameObject prev_screw_;

    [SerializeField]
    protected AudioClip sfx_clip_;

    protected ScrewContainer container_;
    protected Collider collider_;
    protected AudioSource soundEffect;

    private GameObject[] ones_;

    private Coroutine cluster_anim_;

    // Use this for initialization
    void Start() {
        container_ = ToolBoxBehavior.Instance.GetContainerById(screw_id_);
        collider_ = GetComponent<Collider>();
    }

    public override void ClickEvent() {
        if (add)
            MoveToContainer();
        else
            MissleContainer();
    }

    public void MissleContainer() {
        try {
            if (!container_.IsEmpty) {
                collider_.enabled = false;
                SoundManager.Instance.PlaySFX(sfx_clip_, false);
                Vector3 pos = container_.GetLastSlotPosition();
                GenericScrewBehavior last = container_.ReleaseSlot();

                if (prev_screw_ != null)
                    cluster_anim_ = StartCoroutine(ClusterAnim(pos, last));
                else
                    StartCoroutine(SingleAnim(pos, last));
            }
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
    }

    public void MoveToContainer() {

        try {
            if (!container_.IsFull) {
                collider_.enabled = false;
                SoundManager.Instance.PlaySFX(sfx_clip_, false);
                Vector3 pos = container_.GetNextSlotPosition();
                container_.ObtainSlot(this);

                // do animation here
                if (prev_screw_ != null)
                    cluster_anim_ = StartCoroutine(ClusterAnim(pos));
                else
                    StartCoroutine(SingleAnim(pos));

                transform.parent = container_.transform;
            }
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
    }

    public void MoveToPosition(Vector3 position, float duration = 0.5f) {
        transform.DOMove(position, duration);
    }

    public void MoveToLocalPosition(Vector3 local, float duration = 0.5f) {
        transform.DOLocalMove(local, duration);
    }

    public void LatentDestroy() {
        if (ones_ != null && ones_.Length > 0) {
            for (int i = 0; i < ones_.Length; ++i)
                if (ones_[i] != null)
                    Destroy(ones_[i]);
        }

        Destroy(gameObject);
    }

    public void LatentStop() {
        transform.DOKill();
        if (cluster_anim_ != null)
            StopCoroutine(cluster_anim_);
        if (ones_ != null && ones_.Length > 0) {
            for (int i = 0; i < ones_.Length; ++i)
                if (ones_[i] != null)
                    Destroy(ones_[i]);
        }
    }

    private IEnumerator SingleAnim(Vector3 pos, GenericScrewBehavior tar = null) {

        MoveToPosition(pos);
        yield return new WaitForSeconds(0.5f);

        // Wiggle if possible

        if (tar != null && !add) {

            tar.LatentDestroy();
            LatentDestroy();
        }

    }

    public IEnumerator ClusterAnim(Vector3 pos, GenericScrewBehavior tar = null) {

        ones_ = new GameObject[9];

        // Instantiate ones
        for (int i = 0; i < ones_.Length; ++i) {
            ones_[i] = Instantiate(prev_screw_, transform.position, Quaternion.identity);
            ones_[i].GetComponent<Collider>().enabled = false;
            Vector3 randoff = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));

            // Move them to scattered position
            SoundManager.Instance.PlaySFX(sfx_clip_, false);
            ones_[i].transform.DOLocalMove(ones_[i].transform.localPosition + randoff, 0.5f);
        }

        yield return new WaitForSeconds(0.8f);

        // Move them to target position
        for (int i = 0; i < ones_.Length; ++i)
            ones_[i].transform.DOMove(pos, 0.8f);

        // Move ten to target position
        SoundManager.Instance.PlaySFX(sfx_clip_, false);
        MoveToPosition(pos, 0.8f);

        yield return new WaitForSeconds(0.8f);
        // Destroy ones
        for (int i = 0; i < ones_.Length; ++i)
            Destroy(ones_[i]);
        ones_ = null;

        if (tar != null && !add) {

            tar.LatentDestroy();
            LatentDestroy();
        }

    }

}
