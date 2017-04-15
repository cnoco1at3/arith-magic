using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using DG.Tweening;

public class ProfileGuide : GenericSingleton<ProfileGuide> {

    private const float kStep = 803.0f;

    public void MoveToScreenById(int id) {
        if (id < 0 || id > 2)
            return;

        Vector3 pos = new Vector3((2 - id) * kStep, 0);
        transform.DOLocalMove(pos, 0.5f);
    }
}
