using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    protected Transform PlayerTransform;//玩家的位置
    protected Transform BabyTransform;//玩家的位置

    public Vector3 destPos;//当前位置
    /*子类继承使用的函数*/
    protected virtual void Initialize() { }
    protected virtual void SonOnEnable() { }
    protected virtual void FSMUpdate() { }
 
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    void OnEnable()
    {
        SonOnEnable();
    }
    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }
  
    }
