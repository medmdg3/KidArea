using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Create_Level:MonoBehaviour
{
    [SerializeField] InputField Name;
    [SerializeField] Information_Transport IT;
    [SerializeField] Scenemanager Sc;
    public void Load_Scene(int scene_Id)
    {
        string T = Name.text;
        Block_Definition BD=new Block_Definition();
        BD.Init_Position = new Vector3(IT.get_World_X(), IT.get_World_Y(), IT.get_World_Z());
        Base_Functions.Save_Data(Path.Combine("Data",T+".kar"),JsonUtility.ToJson(BD));
        Base_Functions.Save_Data(Path.Combine("Data","Infos.txt"), T );
        Scenemanager.Load_Scene(scene_Id);
    }
}
