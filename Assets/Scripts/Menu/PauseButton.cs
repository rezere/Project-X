using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject sett;
    public Sprite[] lan;
    public GameObject[] button;
    public GameObject but_l;
    private Image butt_language;
    private AudioSource au;
    public AudioClip click;
    void Start()
    {
        butt_language = but_l.GetComponent<Image>();
        MenuManager.Start_Sett();
        if(MenuManager.lang == "RU") 
        {
            butt_language.sprite = lan[0];
        }
        if(MenuManager.lang == "UA") 
        {
            butt_language.sprite = lan[1];
        }
        if(MenuManager.lang == "EN") 
        {
            butt_language.sprite = lan[2];
        }
    }


    public void Setting()
    {
            au = button[1].GetComponent<AudioSource>();
            au.volume = MenuManager.volume/100f;
            au.PlayOneShot(click);
            sett.SetActive(true);
    }

    public void Back()
    {
            au = button[1].GetComponent<AudioSource>();
            au.volume = MenuManager.volume/100f;
            au.PlayOneShot(click);
            sett.SetActive(false);
            MenuManager.Save_Sett();
    }
     public void Exit()
    {
        MenuManager.Save_Sett();
        au = button[2].GetComponent<AudioSource>();
        au.volume = MenuManager.volume/100f;
        au.PlayOneShot(click);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Continue()
    {
        au = button[0].GetComponent<AudioSource>();
        au.volume = MenuManager.volume / 100f;
        au.PlayOneShot(click);
        FirstPersonMovement.pause = false;
        FirstPersonLook.TimerStop();
        //Cursor.lockState = CursorLockMode.Locked;
    }
       public void Language()
    {
        Debug.Log(MenuManager.lang);
        if(MenuManager.lang == "RU") 
        {
            MenuManager.lang = "UA";
             butt_language.sprite = lan[1];
        }
        else if(MenuManager.lang == "UA") 
        {
            MenuManager.lang = "EN";
             butt_language.sprite = lan[2];
        }
         else if(MenuManager.lang == "EN") 
        {
            MenuManager.lang = "RU";
            butt_language.sprite = lan[0];
        }
    }
}
