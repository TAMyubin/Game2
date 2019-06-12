using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{


    AudioSource myAu;
    public Slider BGM_Slider;
    void Start()
    {
        myAu = GetComponent<AudioSource>();
        BGM_Slider.value = Volume_Save.volume_BGM;
    }

    void Update()
    {


        myAu.volume = Volume_Save.volume_BGM;
        Volume_Save.volume_BGM = BGM_Slider.value;

    }

}
