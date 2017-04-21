using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMapBackground : MonoBehaviour
{
    [SerializeField]
    private Sprite SubBackground;
    [SerializeField]
    private Sprite SubTubes;
    [SerializeField]
    private Sprite SubPipes;

    [SerializeField]
    private Image Layer_1;
    [SerializeField]
    private Image Layer_2;
    [SerializeField]
    private Image Layer_3;
    [SerializeField]
    private Image Layer_3CC;

    private Camera main;

    [SerializeField]
    private Color subColor; 

    // Use this for initialization
    void Start ()
    {
        if (GameController.add == false)
        {
            main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            main.backgroundColor = subColor;
            Layer_1.sprite = SubBackground;
            Layer_2.sprite = SubTubes;
            Layer_3.sprite = SubPipes;
            Layer_3CC.sprite = SubPipes;

        }
	}
}
