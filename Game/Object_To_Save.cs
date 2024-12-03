using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Object_Infos<T>:MonoBehaviour
{
    Base_Functions A = new Base_Functions();
    [SerializeField] string Id;
    [SerializeField] T Data;
    void Start()
    {
        
        
    }
}
