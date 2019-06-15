using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayHP : MonoBehaviour {
    public int HP = 100;
    private Animator ani;
    private SkinnedMeshRenderer skin;
    public float ChangeColorSpeed = 2;
    private PlayerMove playermove;
    private PlayerShoot playershoot;
    public Slider HPslider;
    public bool isdeath;
    private BabyHp baby;
    private bool noplay;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        baby = GameObject.FindGameObjectWithTag("Baby").GetComponent<BabyHp>();
        ani = GetComponent<Animator>();
        skin = GetComponentInChildren<SkinnedMeshRenderer>();
        playermove = GetComponent<PlayerMove>();
        playershoot = GetComponentInChildren<PlayerShoot>();
    }
	// Update is called once per frame
	void Update () {
        HPslider.value = HP;
        if (skin.material.color != Color.white)
        {
            skin.material.color = Color.Lerp(skin.material.color, Color.white, Time.deltaTime*ChangeColorSpeed);
        }
        if (HP <= 0|| baby.end)//血量为0时，播放死亡动画
        {
            Dead();
        }
    }
    public void TackDamage(int damage)
    {
        HP -= damage;
        skin.material.color = Color.red;//受伤时变红色
        if (HP <= 0)//血量为0时，播放死亡动画
        {
            Dead();
            isdeath = true;
        }  
    }
    void Dead()//主角死亡后不能移动
    {
        if (isdeath||baby.end)
        {
            playermove.enabled = false;
            playershoot.enabled = false;
            isdeath = false;
            baby.end = true;
            if (noplay == false)//只播放一次
            {
                ani.SetTrigger("Death");
                noplay = true;
            }
        }
    }
    public void END()
    {
        Time.timeScale = 0;
    }
}
