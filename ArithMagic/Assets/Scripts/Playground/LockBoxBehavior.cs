using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoundLib;

public class LockBoxBehavior : ProfileButton, IComparable {

    private static Vector3 target_pos_ = new Vector3(0.0f, 180.0f);

    [SerializeField]
    private GameObject load_;
    private static GameObject _load_;

    private int id_ = -1;

    private Button button_;

    private bool unlocked = false;

    [SerializeField]
    private Sprite source;
    [SerializeField]
    private Sprite pressed;

    private void Start() {
        button_ = GetComponent<Button>();
        button_.onClick.AddListener(ClickEvent);
        SpriteState spritestate = new SpriteState();
        spritestate = GetComponent<Button>().spriteState;
        if (!unlocked)
        {
            GetComponent<Image>().sprite = LockBoxSingleton.Instance.lockBox;
            spritestate.pressedSprite = LockBoxSingleton.Instance.lockBox;
        }

        else if (unlocked)
        {
            GetComponent<Image>().sprite = source;
            spritestate.pressedSprite = pressed;
        }
        try
        {
            button_.interactable = true;
            button_.spriteState = spritestate;
        }
        catch (NullReferenceException)
        {
            button_ = GetComponent<Button>();
            button_.interactable = true;
            button_.spriteState = spritestate;
        }

        if (load_ != null) {
            _load_ = load_;
            _load_.SetActive(false);
        }
    }

    public int CompareTo(object obj) {
        if (obj.GetType() != typeof(LockBoxBehavior))
            return -1;
        LockBoxBehavior other = (LockBoxBehavior)obj;
        return other.transform.position.y.CompareTo(transform.position.y);
    }

    public override void ClickEvent() {

        if (unlocked == true)
        {
            SoundManager.Instance.PlaySFX(LockBoxSingleton.Instance.touch_box, false);
            MapRobotBehavior.Instance.MoveToPosition(this);
            _load_.SetActive(true);
        }
        else if (unlocked == false)
        {
            SoundManager.Instance.PlaySFX(LockBoxSingleton.Instance.touch_locked_box[UnityEngine.Random.Range(0, LockBoxSingleton.Instance.touch_locked_box.Length)], false);
        }
    }

    public void SetUnlocked()
    {
        unlocked = true;

        SpriteState spritestate = new SpriteState();
        try
        {
            spritestate = button_.spriteState;
        }
        catch (NullReferenceException)
        {
            button_ = GetComponent<Button>();
            spritestate = button_.spriteState;
        }
        GetComponent<Image>().sprite = source;
        spritestate.pressedSprite = pressed;
        button_.spriteState = spritestate;
     }

    public void SetLockBoxId(int id) { id_ = id; }

    public int GetLockBoxId() { return id_; }

    public Vector3 GetTargetLocalPosition() {
        return transform.localPosition + target_pos_;
    }
}
