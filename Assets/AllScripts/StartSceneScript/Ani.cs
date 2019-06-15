using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ani : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator ani;
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    public void danru()
    {
        ani.CrossFade("hengsao", 1f);
    }
    public void dan()
    {
        ani.CrossFade("sao", 1f);
    }
    public void danchu()
    {
        ani.CrossFade("StarCamera", 0.5f);
    }

}
