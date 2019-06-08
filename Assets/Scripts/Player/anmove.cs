using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anmove : MonoBehaviour
{
    public Vector2 Pos;
    public Vector2 beetween;
    public bool SpaceMove=false;
    private int count;
    public PlayerMove playerMove;
    // Start is called before the first frame update
    void Start()
    {
        Pos =GameObject.Find("方向按键").transform.position;
        playerMove = GameObject.Find("PlayerBear").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        count = Input.touchCount;
        if (count >= 1)
        {
            for (int i = 0; i < count; i++)
            {
                Touch touch = Input.GetTouch(i);
                Vector2 mouse = touch.position;
                if (Vector2.Distance(mouse, Pos)<300)
                {
                    isattack();
                    float distance = Vector2.Distance(mouse, Pos);
                    if (distance > 200)
                    {
                        Vector2 target = mouse - Pos;
                    mouse = Pos + target.normalized * 200;
                    }
                    GameObject.Find("方向按键").transform.position = mouse;
                    beetween = mouse - Pos;
                    SpaceMove = true;
                }
               }
            }
      else  if (Input.GetMouseButton(0))
        {
                Vector2 mouse = Input.mousePosition;
            if (Vector2.Distance(mouse, Pos) < 300)
            {
                isattack();
                float distance = Vector2.Distance(mouse, Pos);
                if (distance > 200)
                {
                    Vector2 target = mouse - Pos;
                    mouse = Pos + target.normalized * 200;
                }
                GameObject.Find("方向按键").transform.position = mouse;
                beetween = mouse - Pos;
                SpaceMove = true;
            }
        }
        else
        {
            playerMove.state = PlayerMove.PlayerState.idle;
            GameObject.Find("方向按键").transform.position = Pos;
            beetween = new Vector2(0, 0);
            SpaceMove = false;
        }
    }
 void isattack()
    {
        playerMove.state = PlayerMove.PlayerState.walk;
    }
 
}
