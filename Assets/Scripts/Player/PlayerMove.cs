using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float moveSpeed;
    private Animator playAnimator;
    private anmove anmove;
    public Vector3 nowPos;
    public bool timedown = true;
    private PlayerShoot playershoot;
    public Vector3 flyPos1;
    public PlayerState state;
    public Vector3 pos1;
    public bool isattack;

    public enum PlayerState{
        idle,
        walk,
        attack,
    }
    // Use this for initialization
    void Start () {
        playAnimator = GetComponent<Animator>();
        anmove = GameObject.Find("方向控制").GetComponent<anmove>();
        playershoot = GetComponentInChildren<PlayerShoot>();
    }
	
	// Update is called once per frame
    void Update()
    { 
        switch (state)
        {
            case PlayerState.idle: playAnimator.SetBool("IsMoving", false); break;
            case PlayerState.walk:  Move(); break;
            case PlayerState.attack:if (timedown) { attackani(flyPos1); } break;
        }
    }
    void Move()//行走方法
    {
        if (isattack)
        {
            playAnimator.SetBool("IsMoving", false);
            playAnimator.SetBool("Attack", true);
            transform.LookAt(flyPos1);
        }
        else if(isattack == false){
            Vector3 pos = new Vector3(anmove.beetween.x, 0, Mathf.Abs(anmove.beetween.y));
            pos1 = new Vector3(anmove.beetween.x / 5, 0, anmove.beetween.y / 5);
            transform.LookAt(pos1);
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

            playAnimator.SetBool("IsMoving", true);
            nowPos = transform.position;
        }
    }
    public void attackani(Vector3 flyPos)
    {
      
        timedown = false;
        flyPos1 = flyPos;
        playAnimator.SetBool("Attack", true);
        transform.LookAt(flyPos);
   
    }
   public void playattack()
    {
        playershoot.fly(flyPos1);
    }
    public void endAttack()
    {
        playAnimator.SetBool("Attack", false);
        isattack = false;
        timedown = true;
    }
}
