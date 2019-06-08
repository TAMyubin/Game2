using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour {

    Text myText;
    void Start () {
		myText = GetComponent<Text>();

        if (PlayerPrefs.GetInt("isFirstPlayGame") > 0)
            myText.text = "按任意键跳过动画";

	}
	

	void Update () {
		
	}
}
