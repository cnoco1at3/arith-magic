using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class FoldMenu : MonoBehaviour {

    [SerializeField]
    GameObject folding_part_;

    bool is_folded_ = true;

	// Use this for initialization
	void Start () {
        Button button = gameObject.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void GetFold() {
        if (is_folded_) 
            folding_part_.transform.DOMoveY(0, 1);
        else
            folding_part_.transform.DOMoveY(-90, 1);

        Debug.Log(is_folded_);

        is_folded_ = !is_folded_;
    }
}
