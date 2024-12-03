using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadAGame : MonoBehaviour
{
    [SerializeField] InputField T;
    [SerializeField] Scenemanager Sc;
    public void open_level()
    {
        string t = T.text;
        string h = Base_Functions.Load_Data(Path.Combine("Data", t+".kar"));
        if (h == "\t")
        {
            Debug.LogError("File Does not exist! "+t);
            return;
        }
        Base_Functions.Save_Data(Path.Combine("Data", "Infos"), t);
        Scenemanager.Load_Scene(1);
    }
}
