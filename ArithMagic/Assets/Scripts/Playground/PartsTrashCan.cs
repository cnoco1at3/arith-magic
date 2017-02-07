using UnityEngine;
using System.Collections;

public class PartsTrashCan : PartsAcceptor {

    private const string kSnapEaseOut = "SnapEaseOut";

    public override void OnPartEnter(PartsBehavior part) {
        Destroy(part.gameObject);
    }

    public override void OnPartExit(PartsBehavior part) { }
}
