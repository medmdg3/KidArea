using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class testing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string A;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Show_All_Directories(A);
    }
    public static void Show_All_File(string path,bool is_full = false)
    {
        string[] Files;
        if (is_full == false) path = Path.Combine(Application.dataPath , path);
        if (Directory.Exists(path))
        {
            Files = Directory.GetFiles(path);
            for (int i = 0; i < Files.Length; i++) Debug.LogWarning(Files[i]);
        }
        else
        {
            Debug.LogError("Directory does not exist! " + path);
        }
    }
    public static void Show_All_Directories(string path, bool is_full = false)
    {
        string[] Files;
        if (is_full == false) path = Path.Combine(Application.dataPath, path);
        if (Directory.Exists(path))
        {
            Files = Directory.GetDirectories(path);
            for (int i = 0; i < Files.Length; i++) Debug.LogWarning(Files[i]);
        }
        else
        {
            Debug.LogError("Directory does not exist! " + path);
        }
    }
}
