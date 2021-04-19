using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_House : MonoBehaviour
{
    public GameObject[] light_home;
    public bool active;

    void Start()
    {
        active = false;
    }
    public void LightOff()
    {
        for(int i=0; i<light_home.Length;i++)
        light_home[i].SetActive(active);
    }
}
