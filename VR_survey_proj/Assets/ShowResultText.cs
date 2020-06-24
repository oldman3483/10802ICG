using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    // 記得加這行

public class ShowResultText : MonoBehaviour
{

    int a = 0;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<Text>().text = "" + a;    // 前面加空字串，是為了把 整數a 轉為 字串。
        }
    }
}