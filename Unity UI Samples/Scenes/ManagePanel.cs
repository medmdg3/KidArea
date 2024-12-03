using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePanel : MonoBehaviour
{
    [SerializeField] Data_Center DC;
    [SerializeField] Dropdown M1, M2, M3;
    [SerializeField] InputField T;
    [SerializeField] Text title;
    [SerializeField] Button B1, B2, B3;
    [SerializeField] Put_Blocks Pb;
    [SerializeField] Toggle To;
    public int Curr_Id=-1;
    void Start()
    {
        DC = FindAnyObjectByType<Data_Center>();
    }
    public static int Str_to_int(string s)
    {
        int ans = 0;
        int i = 0;
        if (s[0] == '-') i++;
        for (; i < s.Length; i++)
        {
            ans *= 10;
            ans += s[i] - '0';
            if (ans > 999999){
                ans = 999999;
                break;
            }
        }
        if (s[0] == '-') ans *= -1;
        return ans;
    }
    public void Delete()
    {
        if (DC.Blocks.ContainsKey(Curr_Id))
        {
            Pb.Delete(DC.Blocks[Curr_Id].gameObject);
        }
        Pb.Unpause();
    }
    public void Open(int ID)
    {
        if (ID != -1)
        {
            title.text = "ID = " + ID.ToString();
        }
        Curr_Id = ID;
        if (!DC.Blocks.ContainsKey(ID)) return;
        Block_Perscription BL = DC.Blocks[ID];
        Block_Perscription.Block_Infos Inf = BL.BI;
        int type = Inf.type;
        M1.gameObject.SetActive(false);
        M2.gameObject.SetActive(false);
        M3.gameObject.SetActive(false);
        T.gameObject.SetActive(false);
        To.gameObject.SetActive(false);
        if (type == 0)
        {
            return;
        }
        if (type == 1)
        {
            M1.gameObject.SetActive(true);
            M2.gameObject.SetActive(true);
            M3.gameObject.SetActive(true);
            M1.options.Clear();
            M2.options.Clear();
            M3.options.Clear();
            M1.options.Add(new Dropdown.OptionData("-1", null));
            M3.options.Add(new Dropdown.OptionData("-1", null));
            M1.value = 0;
            M3.value = 0;
            foreach (int i in DC.Blocks.Keys)
            {
                if (i == ID) continue;
                int t = DC.Blocks[i].BI.type;
                if (t==1||t==3||t==4||t==7)
                {
                    M1.options.Add(new Dropdown.OptionData(i.ToString(), null));
                    M3.options.Add(new Dropdown.OptionData(i.ToString(), null));
                    if (Inf.B1 == i)
                    {
                        M1.value = M1.options.Count - 1;
                    }
                    if (Inf.B2 == i)
                    {
                        M3.value = M3.options.Count - 1;
                    }
                }
            }
            string[] operations = { "<", ">", "=", "!=", ">=", "<=" };
            for(int i=0;i<operations.Length;i++) M2.options.Add(new Dropdown.OptionData(operations[i], null));
            M2.value = 0;
            if (Inf.OP!=-1) M2.value = Inf.OP;
            return;
        }
        if (type == 2)
        {
            M1.gameObject.SetActive(true);
            M2.gameObject.SetActive(true);
            M1.options.Clear();
            M2.options.Clear();
            M1.options.Add(new Dropdown.OptionData("-1", null));
            M2.options.Add(new Dropdown.OptionData("-1", null));
            foreach (int i in DC.Blocks.Keys)
            {
                if (i == ID) continue;
                int t = DC.Blocks[i].BI.type;
                if (t == 2)
                {
                    M1.options.Add(new Dropdown.OptionData(i.ToString(), null));
                }
                if (t == 1)
                {
                    M2.options.Add(new Dropdown.OptionData(i.ToString(), null));
                }
            }
            return;
        }
        if (type == 3)
        {
            T.gameObject.SetActive(true);
            To.gameObject.SetActive(true);
            T.text = Inf.val.ToString();
            To.isOn = Inf.activated;
            T.contentType = InputField.ContentType.IntegerNumber;
            return;
        }
        if (type == 4)
        {
            M1.gameObject.SetActive(true);
            M2.gameObject.SetActive(true);
            M3.gameObject.SetActive(true);
            M1.options.Clear();
            M2.options.Clear();
            M3.options.Clear();
            M1.options.Add(new Dropdown.OptionData("-1", null));
            M3.options.Add(new Dropdown.OptionData("-1", null));
            M1.value = 0;
            M3.value = 0;
            foreach (int i in DC.Blocks.Keys)
            {
                if (i == ID) continue;
                int t = DC.Blocks[i].BI.type;
                if (t == 1 || t == 3 || t == 4 || t == 7)
                {
                    M1.options.Add(new Dropdown.OptionData(i.ToString(), null));
                    M3.options.Add(new Dropdown.OptionData(i.ToString(), null));
                    if (Inf.B1 == i)
                    {
                        M1.value = M1.options.Count - 1;
                    }
                    if (Inf.B2 == i)
                    {
                        M3.value = M3.options.Count - 1;
                    }
                }
            }
            string[] operations = { "+", "-", "*", "/", "%" };
            for (int i = 0; i < operations.Length; i++) M2.options.Add(new Dropdown.OptionData(operations[i], null));
            M2.value = 0;
            if (Inf.OP != -1) M2.value = Inf.OP;
            return;
        }
        if (type == 5)
        {
            return;
        }
        if (type == 6)
        {
            T.gameObject.SetActive(true);
            T.contentType = InputField.ContentType.Standard;
            return;
        }
        if (type == 7)
        {
            T.gameObject.SetActive(true);
            T.text = ((int)Inf.time).ToString();
            T.contentType = InputField.ContentType.IntegerNumber;
            return;
        }
        if (type == 8)
        {
            return;
        }
    }
    public void Close(bool save=true)
    {
        if (!save)
        {
            Pb.Unpause();
            return;
        }
        int ID = Curr_Id;
        Curr_Id = ID;
        if (!DC.Blocks.ContainsKey(ID)) Pb.Unpause();
        Block_Perscription BL = DC.Blocks[ID];
        Block_Perscription.Block_Infos Inf = BL.BI;
        int type = Inf.type;
        if (type == 0)
        {
            Pb.Unpause();
            
            
        }
        if (type == 1)
        {
            Inf.B1 = Str_to_int(M1.options[M1.value].text);
            Inf.B2 = Str_to_int(M3.options[M3.value].text);
            Inf.OP = M2.value;
            //Fixe_Value
            Pb.Unpause();
            
            
        }
        if (type == 2)
        {
            Inf.B1 = Str_to_int(M1.options[M1.value].text);
            Inf.B2 = Str_to_int(M2.options[M2.value].text);
            //Fixe values
            Pb.Unpause();
           
            
        }
        if (type == 3)
        {
            if (T.text.Length > 10)
            {
                Inf.val = 0;
            }else
            Inf.val = Str_to_int(T.text);
            Inf.activated = To.isOn;
            Pb.Unpause();
        }
        if (type == 4)
        {
            Inf.B1 = Str_to_int(M1.options[M1.value].text);
            Inf.B2 = Str_to_int(M3.options[M3.value].text);
            Inf.OP = M2.value;
            //Fixe_Value
            Pb.Unpause();
        }
        if (type == 5)
        {
            Pb.Unpause();
            
            
        }
        if (type == 6)
        {
            
            Inf.text = T.text;
            Pb.Unpause();

        }
        if (type == 7)
        {
            if (T.text.Length > 10) Inf.time = 100;
            else Inf.time = Str_to_int(T.text);

            
            
        }
        if (type == 8)
        {
            Pb.Unpause();
        }
    }
}

