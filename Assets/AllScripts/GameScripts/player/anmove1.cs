using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class anmove1 : MonoBehaviour
{
    public Vector2 Pos;
    public Vector2 beetween;
    private Vector2 mounse;
    public bool SpaceMove=false;
    public Touch[] touches;//屏幕上触控点数组
    int touch_Id = -1;//触控点数组下标
    //private int count;
    public PlayerShoot shoot; 
    private PlayerMove PlayerMove;
    private bool isattack;
    public Mode mode;
    public enum Mode
    {
        lunpan,//轮盘模式
        moren,//默认模式
    }
    // Start is called before the first frame update
    void Start()
    {
        Pos =GameObject.Find("镜头按键").transform.position;
        PlayerMove = GameObject.Find("PlayerBear").GetComponent<PlayerMove>();
    }
    // Update is called once per frame
    void Update()
    {
        touches = Input.touches;
        if (touches.Length>0)
        {
            //得到离摇杆中心最近的触点下标 touch_Id;
            if (touches.Length == 1)//只有一个触点时
            {
                touch_Id = 0;
                Vector2 mouse = touches[touch_Id].position;
                touchInput(mouse, touch_Id);
            }
            else if (touches.Length > 1)//触点大于1个时
            {
                touch_Id = 0;//先假设下标为0
                for (int i = 1; i < touches.Length; i++)//遍历触点数组
                {
                    if (Vector2.SqrMagnitude(touches[i].position - Pos) < Vector2.SqrMagnitude(touches[touch_Id].position - Pos))//第i个点比假设的点近
                    {
                        touch_Id = i;//假设的点改为第i个点
                    }
                    Vector2 mouse = touches[touch_Id].position;
                    touchInput(mouse, touch_Id);
                }
            }
            if (Input.GetTouch(touch_Id).phase == TouchPhase.Ended)
            {
                Attackstate();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            mouseInput();
            if (Input.GetMouseButtonUp(0))
            {
                Attackstate();
            }
        }     
        else //超出范围
        {
            Attackstate(); 
        }    
    }
    void ShootRange()
    {
        switch (mode)
        {
            case Mode.lunpan:
                shoot.pos.x = beetween.x / 100;
                shoot.pos.z = beetween.y / 100;break;
            case Mode.moren:shoot.pos = PlayerMove.pos1/30;Debug.Log(shoot.pos); break;
        }
    }
    void touchInput(Vector2 mouse,int i)
    {
        if (Vector2.Distance(mouse, Pos) < 300)
        {
            float distance = Vector2.Distance(mouse, Pos);
            if (distance > 150)
            {
                Vector2 target = mouse - Pos;
                mouse = Pos + target.normalized * 150;
            }
            GameObject.Find("镜头按键").transform.position = touches[i].position;
            beetween = mouse - Pos;

            SpaceMove = true;
            ShootRange();
            shoot.ismiaozhun = true;
            isattack = true;
        }
    }
    void mouseInput()
    {
        Vector2 mouse = Input.mousePosition;
        if (Vector2.Distance(mouse, Pos) < 300)//鼠标控制
        {
            float distance = Vector2.Distance(mouse, Pos);
            if (distance > 100)
            {
                Vector2 target = mouse - Pos;
                mouse = Pos + target.normalized * 100;
            }
            GameObject.Find("镜头按键").transform.position = Input.mousePosition;
            beetween = mouse - Pos;
            SpaceMove = true;
            ShootRange();
            shoot.ismiaozhun = true;
            isattack = true;
        }
    }
    void Attackstate()
    {
        if (isattack)
        {
            PlayerMove.flyPos1 = shoot.pos * 10 + shoot.transform.position;
            PlayerMove.state = PlayerMove.PlayerState.attack;
            PlayerMove.isattack = true;
            isattack = false;
        }
        GameObject.Find("镜头按键").transform.position = Pos;
        beetween = new Vector2(0, 0);
        ShootRange();
        SpaceMove = false;
        shoot.ismiaozhun = false;
    }
}
