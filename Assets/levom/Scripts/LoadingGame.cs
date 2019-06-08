using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    /// <summary>
    /// 异步对象
    /// </summary>
    AsyncOperation async;

    //需要加载的游戏关卡
    [Header("关卡编号")]
    public int gameState = 1;

    public Slider m_Slider;

    void Start()
    {
        StartCoroutine(LoadScene());
        async.allowSceneActivation = false;
    }

    IEnumerator LoadScene()
    {

        //异步读取场景。

        switch (gameState)
        {
            case 0:
                async = SceneManager.LoadSceneAsync("StartMenu");
                break;
            case 1:
                async = SceneManager.LoadSceneAsync("FPS1");
                break;
            case 2:
                async = SceneManager.LoadSceneAsync("end");
                break;

        }

            //读取完毕后返回， 系统会自动进入C场景
            yield return async;
    }


    float t = 0;
    void Update()
    {
        m_Slider.value = async.progress;

        if (async.progress >= 0.9f)
        {
            //1秒后转场景
            t ++;
            if (t >= 50)
            {
                async.allowSceneActivation = true;
            }
        }
    }
}
