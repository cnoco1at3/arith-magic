using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using Util;

public class FoldMenu : GenericSingleton<FoldMenu> {

    public void OnUIFold() {
        transform.DOLocalMoveY(-0.5f, 0.5f);
    }

    public void OnUIUnfold() {
        transform.DOLocalMoveY(0, 0.5f);
    }
}
