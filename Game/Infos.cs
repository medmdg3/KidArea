using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infos : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public static GameObject Material;
    [SerializeField] public GameObject Side_Borders,Up_Border,Down_Border;
    public void Gen_Borders(Vector3 B)
    {
        Vector3 A;
        if (Side_Borders == null) Side_Borders = Up_Border;
        if (Side_Borders == null) Side_Borders = Down_Border;
        if (Side_Borders == null) Side_Borders = Material;
        if (Up_Border == null) Up_Border = Side_Borders;
        if (Down_Border == null) Down_Border = Up_Border;
        A = Side_Borders.GetComponent<Renderer>().bounds.size;
        create_borders(new Vector3(B.x / 2, ( B.y) / 2, -A.z / 2), new Vector3(B.x + A.x, (0 + B.y) + A.y, A.z), Side_Borders);
        create_borders(new Vector3(B.x / 2, ( B.y) / 2, B.z + A.z / 2), new Vector3(B.x + A.x, (0 + B.y) + A.y, A.z), Side_Borders);
        create_borders(new Vector3(-A.x / 2, ( B.y) / 2, B.z / 2), new Vector3(A.x, (0 + B.y) + A.y, B.z + A.z), Side_Borders);
        create_borders(new Vector3(B.x + A.x / 2, ( B.y) / 2, B.z / 2), new Vector3(A.x, (0 + B.y) + A.y, B.z + A.z), Side_Borders);
        A = Down_Border.GetComponent<Renderer>().bounds.size;
        create_borders(new Vector3(B.x / 2, -A.y / 2 , B.z / 2), new Vector3(B.x + A.x, A.y, B.z + A.z), Down_Border);
        A = Up_Border.GetComponent<Renderer>().bounds.size;
        create_borders(new Vector3(B.x / 2, 0 + A.y / 2+B.y, B.z / 2), new Vector3(B.x + A.x, A.y, B.z + A.z), Up_Border);
    }
    public static void create_borders(Vector3 pos,Vector3 scal,GameObject obj)
    {
        
        Vector3 Ipos = obj.transform.position,Isc=obj.transform.localScale;
        obj.transform.position = pos;
        obj.transform.localScale = new Vector3(scal.x,scal.y,scal.z);
        Instantiate(obj);
        obj.transform.position = Ipos;
        obj.transform.localScale = Isc;
    }
    
}
