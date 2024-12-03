using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Select_Type : MonoBehaviour
{
    public enum Category
    {
        Integer,
        Decimal,
        String
    };
    [SerializeField] Category Type;
    [SerializeField] bool ranged;
    [SerializeField] double Min, Max;
    [SerializeField] string test;
    [SerializeField] TMPro.TextMeshProUGUI A;
    [SerializeField] TMPro.TMP_InputField B;
    // Start is called before the first frame update
    void Start()
    {
        B.onValueChanged.AddListener(fixe);
    }
    string make_valid_int(string s)
    {
        char[] temp = new char[100];
        int ind = 0;
        for (int i = 0; i < Mathf.Min(100,s.Length); i++)
        {
            if (s[i] > 57 || s[i] < 48)
            {
                continue;
            }
            temp[ind] = s[i];
            ind++;
        }
        Debug.LogError(s);
        return s;
    }
    int str_to_int(string s)
    {
        int i = 0;
        if (s.Length == 0) return 0;
        if (s[0] == '-') i++;
        int ans = 0 ;
        for(; i < s.Length; i++)
        {
            
            if (ans > (int.MaxValue-s[i]+48) / 10) return int.MinValue;
            ans *= 10;
            ans += s[i] - 48;
        }
        if (s[0] == '-') ans *= -1;
        return ans;
    }
    string check_Int(string s)
    {
        if (s == "") s = "0";
        s = make_valid_int(s);
        if (s.Length == 0) s = "0";
        int n = str_to_int(s);
        Debug.Log("TT "+n.ToString());
        if (ranged && Min<=Max)
        {
            if (n == int.MinValue)
            {
                if (s[0] == '-') n = (int)Min;
                else n =(int) Max;
            }
            if (n > Max) n =(int) Max;
            if (n < Min) n = (int)Min;
            s = n.ToString();
        }
        Debug.Log("Final" + s+" "+n.ToString());
        return s;
    }
    string check_Field(string s)
    {
        if (Type == Category.Integer)
        {
            return check_Int(s);
           
        }
        return "";
    }
    // Update is called once per frame
    void fixe(string test)
    {
        B.text = check_Field(test);

    }
    private void LateUpdate()
    {
        
    }
}
