using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewMapCube : MonoBehaviour
{
    public bool canborn;//判断到了位置没
    public bool isbuilt;//判断是否有炮塔
    private Button btn;
   private Color color;
    private ColorBlock cb;
    void Start()
    {
       btn = GameObject.Find("生成鸭子").GetComponent<Button>();
        
       color  = new Color(1, 1, 1, 0.5f);
        cb = new ColorBlock();
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.pressedColor = Color.white;
        cb.disabledColor = color;
        cb.colorMultiplier = 1;
        btn.colors = cb;
        canborn = false;
        /*动态修改button颜色*/
        //ColorBlock cb = new ColorBlock();
        //cb.normalColor = Color.gray;
        //cb.highlightedColor = Color.green;
        //cb.pressedColor = Color.blue;
        //cb.disabledColor = Color.black;
        //btn.colors = cb
    }
    void Update()
    {

    }
    void OnTriggerStay(Collider col)
    {
        canborn = true;
        color = new Color(1, 1, 1, 1f);
        cb.normalColor = color;
        btn.colors = cb;
    }
    void OnTriggerExit(Collider col)
    {
        canborn = false;
        color = new Color(1, 1, 1, 0.5f);
        cb.normalColor = color;
        btn.colors = cb;
    }
}
