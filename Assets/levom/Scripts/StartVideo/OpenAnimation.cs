using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OpenAnimation : MonoBehaviour {

    /// <summary>
    /// 判断
    /// </summary>
    static bool isFirstPlayGame = true;
    bool isVideoPlaying = false;
    VideoPlayer myMovie;

	void Start () {

        myMovie = GameObject.Find("Main Camera").GetComponent<VideoPlayer>();

        if (PlayerPrefs.GetInt("isFirstPlayGame") > 0)
            isFirstPlayGame = false;
       // PlayerPrefs.DeleteKey("isFirstPlayGame");
	}
	
	
	void Update () {

        if (myMovie.isPlaying)
            isVideoPlaying = true;

        if (Input.anyKeyDown && !isFirstPlayGame && isVideoPlaying)
            SceneManager.LoadSceneAsync("StartMenu");

        if (!myMovie.isPlaying && isVideoPlaying)        
            SceneManager.LoadSceneAsync("StartMenu");
	}


    void OnDestroy()
    {
        //存储数据到注册表
        PlayerPrefs.SetInt("isFirstPlayGame", 1);
    }
}
