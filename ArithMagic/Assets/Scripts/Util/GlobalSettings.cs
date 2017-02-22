using UnityEngine;
using System.Collections;
using Util;
using DG.Tweening;

public class GlobalSettings : GenericSingleton<GlobalSettings> {

	// Use this for initialization
	void Awake () {
        DOTween.Init(true, true);
	}
}
