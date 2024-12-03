using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Manager : MonoBehaviour
{
    string[] Comparaisons = { "<", ">", "=", "!=", ">=", "<=" };
    string[] Operations = { "+", "-", "*", "/", "%" };
    Block_Perscription P;
    TMPro.TextMeshPro T;
    int type;
    void Start()
    {
        P = gameObject.GetComponentInParent<Block_Perscription>();    
        T = gameObject.GetComponent<TMPro.TextMeshPro>();
        T.text = "";
        T.color = Color.black;
    }
    void Update()
    {
        type = P.BI.type;
        if (type == 0)
        {
            goto H;
        }
        if (type == 1)
        {
            T.text = P.BI.B2.ToString() + Comparaisons[P.BI.OP] + P.BI.B1.ToString();
            goto H;
        }
        if (type == 2)
        {
            T.text = P.BI.B2.ToString()+"?"+P.BI.B1.ToString();
            goto H;
        }
        if (type == 3)
        {
            T.text = P.BI.val.ToString();
            goto H;
        }
        if (type == 4)
        {
            T.text = P.BI.B2.ToString() + Operations[P.BI.OP] + P.BI.B1.ToString();
            goto H;
        }
        if (type == 5)
        {
            goto H;
        }
        if (type == 6)
        {
            T.text = P.BI.text;
            goto H;
        }
        if (type == 7)
        {
            T.text = P.BI.time.ToString();
            goto H;
        }
        if (type == 8)
        {
            T.text =(P.BI.activated?"O":"X");
            goto H;
        }
    H:
        int n = T.text.Length;
        if (n == 1) T.fontSize = 8;
        if (n == 2) T.fontSize = 6;
        if (n == 3) T.fontSize = 5;
        if (n == 4) T.fontSize = 3;
        if(n>4)
        T.fontSize = 1.5f;
    }
}
