using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("暂停界面")]
    public GameObject PauseMenu_Control;

    /// <summary>
    /// 是否在暂停界面
    /// </summary>
    private bool isPause = false;
    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(isPause)
            {
                isPause = false;
                PauseMenu_Control.SetActive(false); 
            }
            else 
            {
                isPause = true;
                PauseMenu_Control.SetActive(true); 
            }
        }
    }

    public void BackGame()
    {
        PauseMenu_Control.SetActive(false);
    }

    public void BackMenu()
    {
        SceneManager.LoadSceneAsync("StartMenu");
    }
}
