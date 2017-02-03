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
        Debug.Log(button);
        button.onClick.AddListener(GetFold);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void GetFold() {
        if (is_folded_)
            Debug.Log(folding_part_.GetComponent<RectTransform>().localPosition);
        else
            Debug.Log(folding_part_.GetComponent<RectTransform>().localPosition);
    }
}
