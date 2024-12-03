using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Information_Transport : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] static int WX=0, WY=0, WZ=0;
    [SerializeField] Text TX, TY, TZ;
    [SerializeField] Slider X, Y, Z;
    void Start()
    {
        if (X != null)
        {
            WX = (int)X.value; WY = (int)Y.value; WZ = (int)Z.value;

        }
        if (TX != null)
        {
            TX.text = "X: " + WX.ToString();
            TY.text = "Y: " + WY.ToString();
            TZ.text = "Z: " + WZ.ToString();
        }
    }
    public void Update_World_X()
    {
        WX = (int)X.value;
        TX.text = "X: " + WX.ToString();
    }
    public void Update_World_Y()
    {
        WY = (int)Y.value;
        TY.text = "Y: " + WY.ToString();
    }
    public void Update_World_Z()
    {
        WZ = (int)Z.value;
        TZ.text = "Z: " + WZ.ToString();
    }
    public int get_World_X()
    {

        return WX;
    }
    public int get_World_Y()
    {
        return WY;
    }
    public int get_World_Z()
    {
        return WZ;
    }
    public void Load_Game()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
