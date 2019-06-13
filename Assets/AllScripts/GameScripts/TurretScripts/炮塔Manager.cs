using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 炮塔Manager : MonoBehaviour
{
   // private TurretAttack Turret;
    [Header("初始金钱")]
    public int defen = 100;
    [Header("炮塔")]
    public GameObject[] paota;

    [Header("得到的金币（传参）")]
    public int num;
    [Header("造塔耗费")]
    public int[] Turretcost;

   
    public Text money;
    public Text zongfen;
    public Text Notenough;
    private int whichNotEnough;
    public bool jia;
    private int zongdefen;
   public bool costmoney;
    float cost = 0;
    // Start is called before the first frame update
    void Start()
    {
       money.text = "金钱" + defen;
        Notenough.text = "        ";
          whichNotEnough = 10;
    }
    // Update is called once per frame
    void Update()
    {
        jiafen();
        if (costmoney)
        {
      
            cost += Time.deltaTime;
            if (cost >= 2)
            {
                Notenough.text = "        ";
                whichNotEnough = 10;
                cost = 0;
               costmoney = false;
            }
        }
       
    }
    /// <summary>
    /// 建造鸭子
    /// </summary>
    public void duck()
    {
                if (defen >=Turretcost[0])
                {
                    defen -= Turretcost[0];
             Instantiate(paota[0], this.transform, false);
          //  Turret = TurretResources.GetInstance().CreatTurret(0);
        }
                else
                {
            whichNotEnough = 0;
            costmoney = true;
                }
    }
    /// <summary>
    /// 建造狗
    /// </summary>
    public void dog()
    {
        if (defen >= Turretcost[1])
        {
            defen -= Turretcost[1];
           // Turret = TurretResources.GetInstance().CreatTurret(1);
   
           Instantiate(paota[1], this.transform, false);
        }
        else
        {
            whichNotEnough = 1;
            costmoney = true;
        }
    }
    /// <summary>
    /// 建造土豆
    /// </summary>
    public void potato()
    {
        if (defen >= Turretcost[2])
        {
            defen -= Turretcost[2];

           // Turret = TurretResources.GetInstance().CreatTurret(2);
            Instantiate(paota[2], this.transform, false);
        }
        else
        {
            whichNotEnough = 2;
            costmoney = true;
        }
    }
    /// <summary>
    /// 加分函数
    /// </summary>
    void jiafen()
    {
        if (jia)
        {
            defen += num;
            zongdefen += num;
            jia = false;
        }
        string s = "金钱" + defen;
        string x = "总得分" + zongdefen;
        money.text = s;
        zongfen.text = x;
        switch (whichNotEnough)
        {
            case 0: Notenough.text = "你的金币小于" + Turretcost[0]; break;
            case 1: Notenough.text = "你的金币小于" + Turretcost[1]; break;
            case 2: Notenough.text = "你的金币小于" + Turretcost[2]; break;
            default:  break;
        } 
    }
}
