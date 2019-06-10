using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCTRL : MonoBehaviour
{
    public GameObject go;
    BulletResources Bullet;
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
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopTheGame()
    {
        go.SetActive(true);
        Time.timeScale = 0;
       

    }
    public void StarTheGame()
    {
        Time.timeScale = 1;
        go.SetActive(false);
    }

}
