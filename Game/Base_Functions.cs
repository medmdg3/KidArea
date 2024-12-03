using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Base_Functions
{
    const int Id_Size = 24;
    public static string gen_Id()
    {
        char[] characters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        char[] Id = new char[Id_Size];
        for(int i = 0; i < Id_Size; i++)
        {
            int t = UnityEngine.Random.Range(0,characters.Length-1);
            Id[i] = characters[t];
        }
        return new string(Id);
    }
    public static void Save_Scene(List<Block_Definition> S,string name,string File_Beg = "Data")
    {
        string data = "";
        for (int i = 0; i < S.Count; i++)
        {
            data += JsonUtility.ToJson(S[i], true);
        }
        Save_Data(Path.Combine(File_Beg, name), data);
    }
    public static void Save_Scene_Scale(Vector3 S, string name, string File_Beg = "Data")
    {
        string data = "";
            data += JsonUtility.ToJson(S, true);
        Save_Data(Path.Combine(File_Beg, "Scale" + name), data);
    }
    public static Vector3 Load_Scene_Scale(string name, string File_Beg = "Data")
    {
        string data = Load_Data(Path.Combine(File_Beg, "Scale"+ name ));
        return JsonUtility.FromJson<Vector3>(data);
    }
    public static List<Block_Definition> Load_Scene(string name,string File_Beg = "Data")
    {
        List<Block_Definition> S = new List<Block_Definition>();
        try
        {
            string s = Load_Data(Path.Combine(File_Beg ,name));
            int i = 0, co = 0;
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
        }
        catch
        {

        }
        return S;
    }
    public static void Save_Data(string File_Name, string Data,bool full_Path=false )
    {
        if (!full_Path)
        {
            File_Name = Path.Combine( Application.dataPath, File_Name);
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
        }
    }
    public static string Load_Data(string File_Name, bool full_Path = false)
    {
        if (!full_Path)
        {
            File_Name = Path.Combine(Application.dataPath, File_Name);
        }

        if (!File.Exists(File_Name))
        {
            Debug.LogError("File does not exist: "+File_Name);
            return "\t";
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
        }
        return "\t";
    }
    public static string[] Load_Datas(string File_Name, bool full_Path = false)
    {
        if (!full_Path)
        {
            File_Name = Path.Combine(Application.dataPath, File_Name);
        }

        if (!Directory.Exists(File_Name))
        {
            Debug.LogError("Directory does not exist: " + File_Name); 
            return new string[0];
        }
        try
        {
            return Directory.GetFiles(File_Name,$"*.kar");
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured while openeing the Folder!" + e.ToString());
        }
        return new string[0];
    }
}
