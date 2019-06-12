using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCTRL : MonoBehaviour
{
    public GameObject go;
    private Animator goani;
    BulletResources Bullet;
    public GameObject audioctrl;
    private Animator slider;
    // Start is called before the first frame update
    void Reset()
    {
        if (Bullet.Lives.Count > 0)
        {
            Bullet.Lives.Clear();
        }
        if (Bullet.Deaths.Count > 0)
        {
            Bullet.Deaths.Clear();
        }
  
    }
    void Start()
    {
        goani = go.GetComponent<Animator>();
        audioctrl = GameObject.FindGameObjectWithTag("GameController");
        slider = audioctrl.GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopTheGame()
    {
        goani.SetBool("yes", true);
       // Time.timeScale = 0;
        slider.SetBool("yes", true);

    }
    public void StarTheGame()
    {
        //  Time.timeScale = 1;
        goani.SetBool("yes", false);
        slider.SetBool("yes", false);

    }

}
