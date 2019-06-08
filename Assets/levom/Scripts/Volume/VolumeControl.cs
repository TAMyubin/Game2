using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour {


    public Slider BGM_Slider;
    public Slider SE_Slider;
	void Start () {

	}
	
	void Update () {

        Volume_Save.volume_BGM = BGM_Slider.value;
        Volume_Save.volume_SE = SE_Slider.value;

	}

}
