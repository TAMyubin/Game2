using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop : MonoBehaviour
{
    // Start is called before the first frame update
    bool a;
    public void StopTheGame()
    {
        if (a==false)
        {
            a = true;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            a = false;
        }
    }
}
