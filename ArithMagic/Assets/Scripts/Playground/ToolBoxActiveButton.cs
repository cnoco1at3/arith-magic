using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractLib;

public class ToolBoxActiveButton : Clickable {

    private SpriteRenderer sprite_;
    private Collider collider_;

    void Start() {
        sprite_ = GetComponent<SpriteRenderer>();
        collider_ = GetComponent<Collider>();
    }

    public void ActiveButton(bool enabled) {
        sprite_.enabled = enabled;
        collider_.enabled = enabled;
    }

    public override void ClickEvent() {
        ToolBoxBehavior.Instance.CheckSolveStatus();
    }
}
