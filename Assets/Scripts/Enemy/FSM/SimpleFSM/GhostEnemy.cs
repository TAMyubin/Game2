using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : SimpleFSM
{/*幽灵*/
    private Shader shader;
    private SkinnedMeshRenderer[] meshRenderer;//网格数据
    public float a;
    private Color color;
    /*初始函数*/
    protected override void SonStart()
    {
        a = 0.5f;
        color = Color.white;
        shader = Shader.Find("Standard");
        meshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < meshRenderer.Length; i++)
        {

            Color temp = meshRenderer[i].material.GetColor("_Color");
            temp = color;
            meshRenderer[i].material.SetColor("_Color", temp);
        }

    }//子函数
    /*帧函数*/
    protected override void SonUpdate()
    {
        for (int i = 0; i < meshRenderer.Length; i++)
        {

            Color temp = meshRenderer[i].material.GetColor("_Color");
            color.a = a;
            temp = color;
          
            meshRenderer[i].material.SetColor("_Color", temp);
        }
    }
    //子函数

    protected override void SonAttackStart()
    {
        a = 1;
    }
    protected override void SonAttackEnd()
    {
        a = 0.5f;
    }
}
