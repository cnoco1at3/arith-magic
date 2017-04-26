using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeScanningBackground : MonoBehaviour {

    [SerializeField]
    private Sprite background;
    [SerializeField]
    private Sprite topGear;
    [SerializeField]
    private Sprite bottomGear;

    [SerializeField]
    private GameObject backgroundImage;
    [SerializeField]
    private GameObject topGearImage;
    [SerializeField]
    private GameObject bottomGearImage;

    // Use this for initialization
    void Start () {
        if (GameController.add == false)
        {
            backgroundImage.GetComponent<SpriteRenderer>().sprite = background;
            topGearImage.GetComponent<SpriteRenderer>().sprite = topGear;
            bottomGearImage.GetComponent<SpriteRenderer>().sprite = bottomGear;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
