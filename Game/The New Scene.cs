using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TheNewScene : MonoBehaviour
{
    [SerializeField] GameObject Par, Node;
    string A = Path.Combine(Application.dataPath,"Data");
    private void Start()
    {
        open();
    }
    public void open()
    {
        string[] P = Base_Functions.Load_Datas(A);
        Debug.Log(P);
        for(int i = 0; i < P.Length; i++)
        {
            GameObject obj = Instantiate(Node,Par.transform);
            string temp = "";
            for(int j = A.Length+1; j < P[i].Length; j++)
            {
                
                if (P[i][j] == '.') break;
                temp += P[i][j];
            }
            obj.GetComponentInChildren<Text>().text = temp;
            obj.GetComponent<Button>().onClick.AddListener(()=>loadScene(temp));
        }
    }
    public void loadScene(string type)
    {
        Base_Functions.Save_Data("Infos.txt", type);
        Scenemanager.Load_Scene(1);
    }
}
