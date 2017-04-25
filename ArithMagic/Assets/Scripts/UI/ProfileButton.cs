using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ProfileButton : MonoBehaviour {

    // Use this for initialization
    void Start() {
        GetComponent<Button>().onClick.AddListener(ClickEvent);
    }


    public virtual void ClickEvent() {
        Debug.Log("clicked!");
    }
}
