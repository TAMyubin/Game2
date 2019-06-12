using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUICtrl : MonoBehaviour
{
    public Animator[] ani;
    public bool[] a;
    public GameObject image;
    private Animator beijingani;
    private Animator slider;
    // Start is called before the first frame update
    void Start()
    {
        beijingani = image.GetComponent<Animator>();
        slider = GameObject.FindGameObjectWithTag("GameController").GetComponent<Animator>();
        ani = GetComponentsInChildren<Animator>();
        a = new bool[3];
    }
    void Update()
    {
        for (int i = 0; i < a.Length; i++)
        {
            ani[i].SetBool("test", a[i]);
        }
     
    }
    public void dianji()
    {
        if (a[0])
        {
            a[0] = false;
        }
        else
        {
            a[0] = true;
            a[1] = false;
            a[2] = false;
   
        }
      beijingani.SetBool("try", false);
        slider.SetBool("yes", a[0]);
    }
    public void weifalse()
    {
        if (a[1])
        {
            a[1] = false;
        }
        else
        {
            a[1] = true;
            a[0] = false;
            a[2] = false;
        }
        slider.SetBool("yes", false);
        beijingani.SetBool("try", false);
    }
    //开始新游戏
    public void New_Game()
    {

        SceneManager.LoadSceneAsync("Loading_ToNewGame");
 
    }
   public void beijing()
    {
        beijingani.SetBool("try", true);
    
    }
}
