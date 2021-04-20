using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_House : MonoBehaviour
{
    public GameObject[] light_home;
    private AudioSource au;
    public AudioClip click;
    public bool active;

    void Start()
    {
        active = false;
    }
    public void LightOff()
    {
        au = this.GetComponent<AudioSource>();
        au.volume = MenuManager.volume/100f;
        au.PlayOneShot(click);
        for(int i=0; i<light_home.Length;i++)
        light_home[i].SetActive(active);

    }
}
