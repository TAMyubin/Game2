using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCTRL : MonoBehaviour
{
    public GameObject go;
    private Animator goani;
    public GameObject audioctrl;
    private Animator slider;
    bool a;

    void Start()
    {
        goani = go.GetComponent<Animator>();
        audioctrl = GameObject.FindGameObjectWithTag("GameController");
        slider = audioctrl.GetComponentInChildren<Animator>();
        
    }

    public void StopTheGame()
    {
        if (a)
        {
            a = false;
            Time.timeScale = 1;
        }
        else
        {
            a = true;
           
        }
        goani.SetBool("yes", a);
     
        slider.SetBool("yes", a);

    }
    public void StarTheGame()
    {
        Time.timeScale = 0;
    }

}
