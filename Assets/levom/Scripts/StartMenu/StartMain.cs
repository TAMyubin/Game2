using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMain : MonoBehaviour
{
    enum StartmainState
    {
        main,//刚进入游戏的界面
        start,//进入开始游戏的界面
        setting,//进入设置的界面
        instruction,//帮助说明界面
    }

    /// <summary>
    /// 当前界面状态
    /// </summary>
    StartmainState _StartmainState = StartmainState.main;
    /// <summary>
    /// 上一帧的界面状态
    /// </summary>
    StartmainState _Pass_StartmainState = StartmainState.setting;

    GameObject GO_Main_Interface;
    GameObject GO_Start_Interface;
    GameObject GO_Setting_Interface;
    GameObject GO_HelpInstruction_Interface;
    void Start()
    {
        GO_Main_Interface = GameObject.Find("Canvas/Button/Main_Interface");
        GO_Start_Interface = GameObject.Find("Canvas/Button/Start_Interface");
        GO_Setting_Interface = GameObject.Find("Canvas/Button/Setting_Interface");
        GO_HelpInstruction_Interface = GameObject.Find("Canvas/Button/HelpInstruction_Interface");
    }

    void Update()
    {
        //界面切换显示
        ShowInterface(_StartmainState);
        // 键盘输入响应
        Key_Input();

    }

    /// <summary>
    /// 改变界面状态
    /// </summary>
    /// <param name="sms"></param>
    private void ChangeStartMainState(StartmainState sms)
    {
        //保存上一次的界面状态
        _Pass_StartmainState = _StartmainState;
        //设置新的状态
        _StartmainState = sms;

    }

    /// <summary>
    /// 键盘输入响应
    /// </summary>
    void Key_Input()
    {
        //输入ESC
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //如果在开始界面或设置界面或帮助界面
            if (_StartmainState == StartmainState.instruction || _StartmainState == StartmainState.setting || _StartmainState == StartmainState.start)
                //设置到主界面
                ChangeStartMainState(StartmainState.main);
        }
    }

    /// <summary>
    /// 关闭所有的界面
    /// </summary>
    private void CloseAllInterface()
    {
        GO_Start_Interface.SetActive(false);
        GO_Setting_Interface.SetActive(false);
        GO_HelpInstruction_Interface.SetActive(false);
        GO_Main_Interface.SetActive(false);
    }

    /// <summary>
    /// 界面切换显示
    /// </summary>
    /// <param name="str"></param>
    private void ShowInterface(StartmainState str)
    {
        if (_Pass_StartmainState != _StartmainState)
        {
            CloseAllInterface();

            if (_StartmainState == StartmainState.main)
            {
                //主界面按钮可见
                GO_Main_Interface.SetActive(true);
            }
            else if (_StartmainState == StartmainState.start)
            {
                //开始界面按钮可见
                GO_Start_Interface.SetActive(true);
            }
            else if (_StartmainState == StartmainState.setting)
            {
                //设置界面按钮可见
                GO_Setting_Interface.SetActive(true);
            }
            else if (_StartmainState == StartmainState.instruction)
            {
                //设置界面按钮可见
                GO_HelpInstruction_Interface.SetActive(true);
            }
        }
    }

    //开始游戏界面
    public void Start_Interface()
    {
        //进入开始游戏界面
        ChangeStartMainState(StartmainState.start);
    }
    //开始新游戏
    public void New_Game()
    {
        SceneManager.LoadSceneAsync("Loading_ToNewGame");
    }
    //继续游戏
    public void Continue_Game()
    {
        //SceneManager.LoadSceneAsync("Loading_ToNewGame");
    }

    public void Setting_Interface()
    {
        //进入设置游戏界面
        ChangeStartMainState(StartmainState.setting);
    }
    public void Instruction_Interface()
    {
        //进入帮助与说明界面
        ChangeStartMainState(StartmainState.instruction);
    }

    public void Qiut_Game()
    {
        Application.Quit();
    }
}
