using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Sprite[] lan;
    public GameObject[] sett;
    public GameObject[] button;
    private Image butt_language;
    private AudioSource audio;
    public AudioClip click;
    private RectTransform panl;
    public int speed;
    private bool panel,ch_panel; // открыты ли настройки

    public void Start()
    {
        panel = false;
        ch_panel = false;
        butt_language = button[4].GetComponent<Image>();
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
    // Начало игры
    public void StartGame()
    {
        audio = button[0].GetComponent<AudioSource>();
        audio.volume = MenuManager.volume/100f;
        audio.PlayOneShot(click);
        SceneManager.LoadScene(1);
    }
    // Настройки
    public void Settings()
    {
        audio = button[1].GetComponent<AudioSource>();
        audio.volume = MenuManager.volume/100f;
        audio.PlayOneShot(click);
        panl = sett[0].GetComponent<RectTransform>();
        panel = true;
    }
    private void FixedUpdate()
    {
        if (panel) // передвижение панели настроеек
        {
            if (panl.offsetMin.y != 0)
            {
                panl.offsetMin += new Vector2(panl.offsetMin.x, speed);
                panl.offsetMax += new Vector2(panl.offsetMax.x, speed);
            }
        }
        if(ch_panel)
        {
            if (panl.offsetMin.y != 0)
            {
                panl.offsetMin -= new Vector2(panl.offsetMin.x, speed);
                panl.offsetMax -= new Vector2(panl.offsetMax.x, speed);
            }
        }
    }
    public void Back()
    {
        if(panel)
        {
        MenuManager.Save_Sett();
        audio = button[3].GetComponent<AudioSource>();
        audio.volume = MenuManager.volume/100f;
        audio.PlayOneShot(click);
        panl = sett[0].GetComponent<RectTransform>();
        panel = false;
        panl.offsetMin = new Vector2(panl.offsetMin.x, -1200);
        panl.offsetMax = new Vector2(panl.offsetMax.x, -1200);
        }
        else if(ch_panel)
        {
            MenuManager.Save_Sett();
            audio = button[3].GetComponent<AudioSource>();
            audio.volume = MenuManager.volume/100f;
            audio.PlayOneShot(click);
            panl = sett[1].GetComponent<RectTransform>();
            ch_panel = false;
            panl.offsetMin = new Vector2(panl.offsetMin.x, 1200);
            panl.offsetMax = new Vector2(panl.offsetMax.x, 1200);
        }
    }
    // Выход
    public void Exit()
    {
        MenuManager.Save_Sett();
        audio = button[2].GetComponent<AudioSource>();
        audio.volume = MenuManager.volume/100f;
        audio.PlayOneShot(click);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void Continue()
    {
        audio = button[0].GetComponent<AudioSource>();
        audio.volume = MenuManager.volume / 100f;
        audio.PlayOneShot(click);
        FirstPersonMovement.pause = false;
        FirstPersonLook.TimerStop();
        //Cursor.lockState = CursorLockMode.Locked;
    }
    

    public void Charters()
    {
        audio = button[1].GetComponent<AudioSource>();
        audio.volume = MenuManager.volume/100f;
        audio.PlayOneShot(click);
        panl = sett[1].GetComponent<RectTransform>();
        ch_panel = true;
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
    public void Charters_Select(int index)
    {
        MenuManager.chart_pick = index;
    }

    public void BacktoMain()
    {
         audio = button[2].GetComponent<AudioSource>();
        audio.volume = MenuManager.volume/100f;
        audio.PlayOneShot(click);
        SceneManager.LoadScene(0);
    }
}
