using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUICtrl : MonoBehaviour
{
    public Animator ani;
    public bool a;
    public GameObject image;
    private Animator beijingani;
    private Animator slider;
    // Start is called before the first frame update
    void Start()
    {
        beijingani = image.GetComponent<Animator>();
        slider = GameObject.FindGameObjectWithTag("audio").GetComponent<Animator>();
        ani = GetComponentInChildren<Animator>();
        slider.SetBool("yes", false);
    }
    public void dianji()
    {
        if (a)
        {
            a = false;
        }
        else
        {
            a = true;
        }
      beijingani.SetBool("try", false);
        slider.SetBool("yes", a);
        ani.SetBool("test", a);
    }
    //开始新游戏
    public void New_Game()
    {
        SceneManager.LoadSceneAsync("Loading_ToNewGame");
        slider.SetBool("yes", false);
        Time.timeScale = 1;

    }
   public void beijing()
    {
        beijingani.SetBool("try", true);
        slider.SetBool("yes", false);
    }
}
