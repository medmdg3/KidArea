using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Put_Blocks : MonoBehaviour
{
    [SerializeField] float max_distance;
    [SerializeField] LayerMask To_Scan;
    [SerializeField] GameObject Blocks;
    [SerializeField] Material[] MTS;
    [SerializeField] int selected = 0;
    [SerializeField] GameObject Edit_Block,Pause_Menu,Normal_Menu;
    [SerializeField] Vector3 Player_in;
    [SerializeField] Image[] Pictures;
    [SerializeField] Color Showen, Hidden;
    public CameraMovement CM;
    [SerializeField] bool Is_Paused = false;
    [SerializeField] Data_Center DC;
    public bool Playing = false;
    Dictionary<Vector3, GameObject> D = new Dictionary<Vector3, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        selected = 0;
        Is_Paused = false;
        Pause_Menu.SetActive(false);
        Normal_Menu.SetActive(true);
    }
    Vector3 suitable(Vector3 A,Vector3 pos)
    {
        if (A.x > pos.x)
        {
            pos -= new Vector3(0.1f, 0, 0);
        }
        else
        {
            pos += new Vector3(0.1f, 0, 0);
        }
        if (A.y > pos.y)
        {
            pos -= new Vector3(0,0.1f, 0);
        }
        else
        {
            pos += new Vector3(0,0.1f, 0);
        }
        if (A.z > pos.z)
        {
            pos -= new Vector3(0,0,0.1f);
        }
        else
        {
            pos += new Vector3(0,0,0.1f);
        }
        return pos;
    }
    public GameObject Find_Ob(int t)
    {
        if (t < 0 || t >= MTS.Length)
        {
            Debug.LogError("Index out of range!");
            t = 0;
        }
        GameObject obj = Blocks;
        obj.GetComponent<Renderer>().material = MTS[t];
        obj.GetComponent<Block_Perscription>().BI.type = t;
        return obj;
    }
    public GameObject Create_Obj(Vector3 pos, int t)
    {
        if (t < 0 || t >= MTS.Length)
        {
            Debug.LogError("Index out of range!");
            t = 0;
        }
        GameObject obj = Find_Ob(t);
        if (D.ContainsKey(pos))
        {
            Debug.LogError(pos.ToString() + "Position filled with block!");
            if (D[pos] == null)
            {
                Debug.LogError("Which is null!");
            }
            return null;
        }
        obj.transform.position = pos;
        D[pos] = Instantiate(obj);
        return D[pos];
    }
    public GameObject Create_Obj(Vector3 pos, GameObject obj)
    {
        if (D.ContainsKey(pos))
        {
            return null;
        }
        obj.transform.position = pos;
        D[pos] = Instantiate(obj);
        return D[pos];
    }
    void Create_Obj(Vector3 center,Vector3 pos,GameObject obj)
    {
        pos = suitable(center,pos);
        pos = new Vector3((int)(pos.x-0.01f)+0.5f, (int)(pos.y)+0.5f, (int)(pos.z-0.01f)+0.5f);
        if (D.ContainsKey(pos))
        {
            Debug.LogError(pos.ToString() + "Position filled with block!");
            if (D[pos] == null)
            {
                Debug.LogError("Which is null!");
            }
            return;
        }
        obj.transform.position = pos;
        D[pos] = DC.Add_Block(obj);
    }
    public void Unpause()
    {
        Is_Paused = false;
        Pause_Menu.SetActive(false);
        Normal_Menu.SetActive(true);
        CM.UnPause();
        Time.timeScale = 1;
    }
    public void Pause(int id=-1)
    {
        Debug.Log("ID is :" + id.ToString());
        Is_Paused = true;
        CM.Pause();
        Pause_Menu.SetActive(true);
        Pause_Menu.GetComponent<ManagePanel>().Open(id);
        Normal_Menu.SetActive(false);
        
        Time.timeScale = 0;
        
    }
    void Menu()
    {
        Pause();
        Pause_Menu.SetActive(true);
        Normal_Menu.SetActive(false);
    }
    public void Delete(GameObject Obj)
    {
        if (!D.ContainsKey(Obj.transform.position)) return;
        DC.delete(Obj.GetComponent<Block_Perscription>().BI.id);
        D.Remove(Obj.transform.position);
        Destroy(Obj);
    }
    void Right_Click(GameObject Obj)
    {
        Pause(Obj.GetComponent<Block_Perscription>().BI.id);
    }
    void Update()
    {
        
        if (!Is_Paused && !Playing)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                //Menu();
            }
            int lase = selected;
            if (Input.GetKey(KeyCode.Alpha0))
                selected = 0;
            if (Input.GetKey(KeyCode.Alpha1))
                selected = 1;
            if (Input.GetKey(KeyCode.Alpha2))
                selected = 2;
            if (Input.GetKey(KeyCode.Alpha3))
                selected = 3;
            if (Input.GetKey(KeyCode.Alpha4))
                selected = 4;
            if (Input.GetKey(KeyCode.Alpha5))
                selected = 5;
            if (Input.GetKey(KeyCode.Alpha6))
                selected = 6;
            if (Input.GetKey(KeyCode.Alpha7))
                selected = 7;
            if (Input.GetKey(KeyCode.Alpha8))
                selected = 8;
            if (Input.GetKey(KeyCode.Backspace))
                selected = -1;
            if (Input.GetMouseButtonDown(0))
            {
                if (selected == -1)
                {
                    Debug.LogWarning("No object selected");
                }
                else
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit H;
                    if (Physics.Raycast(ray, out H, max_distance, To_Scan))
                    {
                        Transform T = H.transform;
                        Create_Obj(T.position, H.point, Find_Ob(selected));
                    }
                    else
                    {
                        Debug.Log(16777216);
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit H;
                if (Physics.Raycast(ray, out H, max_distance, To_Scan))
                {
                    Transform T = H.transform;
                    Right_Click(T.gameObject);
                }
                else
                {
                    Debug.LogWarning(16777216);
                }
            }
            if (selected != lase)
            {
                if (lase != -1 && lase < Pictures.Length)
                    Pictures[lase].color = Hidden;
                if (selected != -1 && selected < Pictures.Length) Pictures[selected].color = Showen;
            }
        }
        else
        {
            Player();
        }
        Debug.Log(Is_Paused);
    }
     void Player()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selected == -1)
            {
                Debug.LogWarning("No object selected");
            }
            else
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit H;
                if (Physics.Raycast(ray, out H, max_distance, To_Scan))
                {
                    Transform T = H.transform;
                    Debug.Log(T.name);
                }
                else
                {
                    Debug.Log(16777216);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit H;
            if (Physics.Raycast(ray, out H, max_distance, To_Scan))
            {
                Transform T = H.transform;
                Debug.Log(T.name+"!");
            }
            else
            {
                Debug.LogWarning(16777216);
            }
        }
    }
    // Update is called once per frame
}
