using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BabyHp : MonoBehaviour
{
    public GameObject[] imagego;
    public Image[] image;
    public bool iskouxue;
    public GameObject go;
    public GameObject success;
    private int a = 2;
    public bool end;

    public float GameTime = 100;
    private Slider Gametimer;
    // Start is called before the first frame update
    void Start()
    {
        Gametimer = GameObject.Find("GameTime").GetComponent<Slider>();
        imagego = GameObject.FindGameObjectsWithTag("BabyHp");
        for(int i = 0; i < image.Length; i++)
        {
            image[i] = imagego[i].GetComponent<Image>();
        }
    }
    // Update is called once per frame
    void Update()
    {
     
            GameSuccess();//时间到
            if (iskouxue)
            {
                image[a].enabled = false;
                a -= 1;
                if (a <= 0)
                {
                    a = 0;
                }
                iskouxue = false;
            }
            if (image[0].enabled == false)
            {
                end = true;
        }
        go.SetActive(end);
    }
    /// <summary>
    /// 时间到游戏成功
    /// </summary>
    void GameSuccess()
    {
        Gametimer.value = GameTime;
        GameTime -= Time.deltaTime;//时间倒数
        if (GameTime <= 0)
        {
            Time.timeScale = 0;
            success.SetActive(true);
        }
    }
}
