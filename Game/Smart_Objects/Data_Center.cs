using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Data_Center : MonoBehaviour
{
    public Dictionary<int, Block_Perscription> Blocks = new Dictionary<int, Block_Perscription>();
    [SerializeField] Put_Blocks P;
    [SerializeField] CameraMovement Cam;
    public Vector3 Scene_Size;
    int Id = 0;
    string Scene_Name;
    [SerializeField] Infos Brdgen;
    void Start()
    {
       Scene_Name = Base_Functions.Load_Data(Path.Combine("Data", "Infos.txt"));

        Load(Scene_Name);
    }
    public void Generate(List<Block_Definition> Scene)
    {
        if (Scene.Count != 0)
        {
            Scene_Size = Scene[0].Init_Position;
        }
        Brdgen.Gen_Borders(Scene_Size);
        if(Cam!=null)
        Cam.world_Borders = Scene_Size;
        for(int i = 1; i < Scene.Count; i++)
        {
            Block_Definition A = Scene[i];
            if (Blocks.ContainsKey(A.id))
            {
                Debug.LogWarning("Existing ID");
                continue;
            }
            Block_Perscription.Block_Infos temp = JsonUtility.FromJson<Block_Perscription.Block_Infos>(A.properties);
            GameObject T= P.Create_Obj(A.Init_Position, temp.type);
            Block_Perscription H= T.GetComponent<Block_Perscription>();
            Blocks[A.id] = H;
            H.set_Block(A.properties);
            Id = Mathf.Max(Id, A.id + 1);
        }
    }
    public GameObject Add_Block(GameObject A)
    {
        GameObject T = Instantiate(A);
        Blocks[Id] = T.GetComponent<Block_Perscription>();
        T.GetComponent<Block_Perscription>().set_Block(A.GetComponent<Block_Perscription>().get_Block());
        T.GetComponent<Block_Perscription>().set_Id(Id);
        Id++;
        return T;
    }
    public void delete(int Id)
    {
        if (Blocks.ContainsKey(Id))
        {
            Blocks.Remove(Id);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.CapsLock))
        {
            Save(Scene_Name);
        }
    }
    void Save(string name)
    {
        
        List<Block_Definition> A = new List<Block_Definition>();
        A.Add(new Block_Definition());
        A[0].Init_Position = Scene_Size;
        int i = 1;
        foreach (int C in Blocks.Keys)
        {
            if (Blocks[C] == null) continue;
            A.Add(new Block_Definition());
            Block_Perscription E = Blocks[C];
            A[i].id = C;
            A[i].Init_Position = (E.transform.position);
            A[i].properties = E.get_Block();
            i++;
        }
        Base_Functions.Save_Scene(A, name+".kar");
    }
    void Load(string name)
    {
        Blocks = new Dictionary<int, Block_Perscription>();
        Generate(Base_Functions.Load_Scene(name + ".kar"));
    }
}
