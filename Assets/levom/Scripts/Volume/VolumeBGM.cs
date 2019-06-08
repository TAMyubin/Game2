using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeBGM : MonoBehaviour
{

    AudioSource myAu;
	void Start () {

        myAu = GetComponent<AudioSource>();
	}
	

	void Update () {

        myAu.volume = Volume_Save.volume_BGM;
	}
}
