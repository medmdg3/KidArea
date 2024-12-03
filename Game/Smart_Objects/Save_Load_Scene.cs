using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class Save_Load_Scene 
{
    public static void Save_Scene(List<Block_Definition> S,string File_Beg="")
    {
        string data = "";
        for(int i = 0; i < S.Count; i++)
        {
            data += JsonUtility.ToJson(S[i], true) ;
        }
        Save_Data(File_Beg + "Scene", data);
    }
    public static List<Block_Definition> Load_Scene(string File_Beg="")
    {
        List<Block_Definition> S = new List<Block_Definition>();
        string s = Load_Data(File_Beg + "Scene");
        int i = 0,co=0;
        string t = "";
        while (i < s.Length)
        {
            if (s[i] == '{') co++;
            if (s[i] == '}') co--;
            if (co < 0)
            {
                Debug.LogError("Something is wrong!");
                return S;
            }
            if (co == 1 && t == "")
            {
                t += s[i];
            }
            else if (t != "")
            {
                t += s[i];
            }
            if (co == 0)
            {
                S.Add(JsonUtility.FromJson<Block_Definition>(t));
                t = "";
            }
            i++;
        }
        return S;
    }
    public static int Save_Data(string File_Name, string Data, bool full_Path = false)
    {
        if (!full_Path)
        {
            File_Name = Path.Combine(Application.dataPath, File_Name);
        }
        try
        {
            using (StreamWriter writer = new StreamWriter(File_Name))
            {
                writer.Write(Data);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured while openeing the file!" + e.ToString());
            return -1;
        }
        return 0;
    }
    public static string Load_Data(string File_Name, bool full_Path = false)
    {
        if (!full_Path)
        {
            File_Name = Path.Combine(Application.dataPath, File_Name);
        }

        if (!File.Exists(File_Name))
        {
            Debug.LogError("File does not exist: " + File_Name);
            return "Error";
        }
        try
        {
            using (StreamReader reader = new StreamReader(File_Name))
            {
                return reader.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured while openeing the file!" + e.ToString());
            return "Error";
        }
        return "";
    }

}
