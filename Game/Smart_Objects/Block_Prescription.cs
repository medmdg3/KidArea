using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block_Perscription : MonoBehaviour
{
    [SerializeField] Data_Center DC;
    public class Block_Infos
    {
        public int id;
        public int type;
        public int B1;
        public int B2;
        public int OP;
        public float val;
        public string text;
        public float time;
        public bool activated;
    }
    public Block_Infos BI=new Block_Infos();
    // Initialize values
    void Start()
    {
        DC = FindAnyObjectByType<Data_Center>();  
    }
    public void set_Block(string H)
    {
        BI = JsonUtility.FromJson<Block_Infos>(H);
    }
    public string get_Block()
    {
        return JsonUtility.ToJson(BI);
    }
    public void set_Id(int Id)
    {
        BI.id = Id;
    }

    public void Clicked(bool right)
    {
        int type = BI.type;
        if (type == 0) return;
        if (type == 1)
        {
            return;
        }
        if (type == 2)
        {
            return;
        }
        if (type == 3)
        {
            if (!BI.activated) return;
            if (right)
            {
                BI.val--;
            }
            else
            {
                BI.val++;
               
            }
            //Update the values
            return;
        }
        if (type == 4)
        {
            return;
        }
        if (type == 5)
        {
            return;
        }
        if (type == 6)
        {
            return;
        }
        if (type == 7)
        {
            return;
        }
        if (type == 8)
        {
            return;
        }

    }
    public void Fixe_Value()
    {
        int type = BI.type;
        if (BI.activated == false) return;
        if (type == 0) return;
        if (type == 1)
        {
            if (BI.B1 == -1 || BI.B2 == -1) return;
            if (DC.Blocks[BI.B1] == null || DC.Blocks[BI.B2] == null) return;
            float v1 = DC.Blocks[BI.B1].BI.val, v2 = DC.Blocks[BI.B2].BI.val;
            //BI.val=v1 OP v2
            //Act according to the new value
            //Update values
            return;
        }
        if (type == 2)
        {
            return;
        }
        if (type == 3)
        {
            return;
        }
        if (type == 4)
        {
            if (BI.B1 == -1 || BI.B2 == -1) return;
            if (DC.Blocks[BI.B1] == null || DC.Blocks[BI.B2] == null) return;
            float v1 = DC.Blocks[BI.B1].BI.val, v2 = DC.Blocks[BI.B2].BI.val;
            //BI.val=v1 OP v2
            //Act according to the new value
            //Update values
            return;
        }
        if (type == 5)
        {
            return;
        }
        if (type == 6)
        {
            return;
        }
        if (type == 7)
        {
            return;
        }
        if (type == 8)
        {
            return;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;
        int type = BI.type;
        if (type == 0) return;
        if (type == 1)
        {
            return;
        }
        if (type == 2)
        {
            //If activated and B1 is null win!
            //Transport to B1 if activated
            return;
        }
        if (type == 3)
        {
            return;
        }
        if (type == 4)
        {
            return;
        }
        if (type == 5)
        {
            return;
        }
        if (type == 6)
        {
            return;
        }
        if (type == 7)
        {
            return;
        }
        if (type == 8)
        {
            //Send message: death
            return;
        }

    }
    void Update()
    {
        
    }
}

