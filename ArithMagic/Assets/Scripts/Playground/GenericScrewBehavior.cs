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

    private Vector3 origin_;
    private Vector2 speed = new Vector2(0.2f, 0.2f);
    private Vector3 current_vel;
    private bool wiggle_ = false;

    // Use this for initialization
    void Start() {
        container_ = ToolBoxBehavior.Instance.GetContainerById(screw_id_);
        collider_ = GetComponent<Collider>();
        origin_ = transform.localPosition;
    }

    public override void ClickEvent() {
        if (add)
            MoveToContainer();
        else
            MissleContainer();
    }

    public void SetWiggle(bool wiggle) {
        if (wiggle != wiggle_) {
            transform.localPosition = origin_;
            wiggle_ = wiggle;
        }
    }

    public void MissleContainer() {
        try {
            if (!container_.is_empty) {
                SoundManager.Instance.PlaySFX(sfx_clip_, false);
                Vector3 pos = container_.GetLastSlotPosition();
                GenericScrewBehavior last = container_.ReleaseSlot();

                if (prev_screw_ != null)
                    StartCoroutine(ClusterAnim(pos, last));
                else
                    StartCoroutine(SingleAnim(pos, last));
            }
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
    }

    public void MoveToContainer() {

        try {
            if (!container_.is_full) {
                SoundManager.Instance.PlaySFX(sfx_clip_, false);
                Vector3 pos = container_.GetNextSlotPosition();
                container_.ObtainSlot(this);

                SetWiggle(false);

                // do animation here
                if (prev_screw_ != null)
                    StartCoroutine(ClusterAnim(pos));
                else
                    StartCoroutine(SingleAnim(pos));

                collider_.enabled = false;
            }
        } catch (NullReferenceException e) {
            Debug.LogException(e);
        }
    }

    public void MoveToPosition(Vector3 position, float duration = 0.5f) {
        transform.DOMove(position, duration);
        origin_ = transform.parent.InverseTransformPoint(position);
    }

    public void MoveToLocalPosition(Vector3 local, float duration = 0.5f) {
        transform.DOLocalMove(local, duration);
        origin_ = local;
    }

    private IEnumerator SingleAnim(Vector3 pos, GenericScrewBehavior tar = null) {
        InteractManager.LockInteraction();

        MoveToPosition(pos);
        yield return new WaitForSeconds(0.5f);

        // Wiggle if possible
        if (container_.is_full)
            container_.SetWiggleBuckets(true);
        else
            container_.SetWiggleBuckets(false);

        if (!add) {
            Destroy(tar.gameObject);
            Destroy(gameObject);
        }

        InteractManager.ReleaseInteraction();
    }

    private IEnumerator ClusterAnim(Vector3 pos, GenericScrewBehavior tar = null) {
        InteractManager.LockInteraction();

        GameObject[] ones = new GameObject[9];

        // Instantiate ones
        for (int i = 0; i < ones.Length; ++i) {
            ones[i] = Instantiate(prev_screw_, transform.position, Quaternion.identity);
            ones[i].GetComponent<Collider>().enabled = false;
            Vector3 randoff = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));

            // Move them to scattered position
            SoundManager.Instance.PlaySFX(sfx_clip_, false);
            ones[i].transform.DOLocalMove(ones[i].transform.localPosition + randoff, 0.5f);
        }

        yield return new WaitForSeconds(0.8f);

        // Move them to target position
        for (int i = 0; i < ones.Length; ++i)
            ones[i].transform.DOMove(pos, 0.8f);

        // Move ten to target position
        SoundManager.Instance.PlaySFX(sfx_clip_, false);
        MoveToPosition(pos, 0.8f);

        yield return new WaitForSeconds(0.8f);
        // Destroy ones
        for (int i = 0; i < ones.Length; ++i)
            Destroy(ones[i]);

        if (!add) {
            Destroy(tar.gameObject);
            Destroy(gameObject);
        }

        InteractManager.ReleaseInteraction();
    }


    private void Update() {
        if (wiggle_) {
            Vector3 target = UnityEngine.Random.insideUnitCircle;
            target = Vector3.Scale(target, speed) + origin_;
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, target, ref current_vel, 0.1f);
        }
    }
}
