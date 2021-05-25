using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    [Header("Выбраный персонаж")]
    public GameObject[] chart;
    [Header("Спрайт языков")]
    public Sprite[] lan;
    [Header("Панель настроик и персонажей")]
    public GameObject[] sett; 
    public GameObject[] button;
    private Image butt_language;
    private AudioSource au;
    public AudioClip click;
    private RectTransform panl;
    public int speed;
    [Header("Открыта ль одна из панелей")]
    private bool panel,ch_panel;
    [Header("Ссылки для перехода в соц.сети")]
    public string[] URL;
    [Header("Кнопки соц.сетей")]
    public GameObject[] social;
    
    private bool internetOn;
    [System.Obsolete]
    void Awake()
    {
        StartCoroutine(GetText());
    }
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

        if(MenuManager.chart_pick == 0) 
        {
            chart[0].SetActive(true);
            chart[1].SetActive(false);
        } 
        if(MenuManager.chart_pick == 1) 
        {
            chart[1].SetActive(true);
            chart[0].SetActive(false);
        } 
        
        Debug.Log(internetOn);
    }
    // Начало игры
    public void StartGame()
    {
        au = button[0].GetComponent<AudioSource>();
        au.volume = MenuManager.volume/100f;
        au.PlayOneShot(click);
        SceneManager.LoadScene(1);
    }
    // Настройки
    public void Settings()
    {
        au = button[1].GetComponent<AudioSource>();
        au.volume = MenuManager.volume/100f;
        au.PlayOneShot(click);
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
        au = button[3].GetComponent<AudioSource>();
        au.volume = MenuManager.volume/100f;
        au.PlayOneShot(click);
        panl = sett[0].GetComponent<RectTransform>();
        panel = false;
        panl.offsetMin = new Vector2(panl.offsetMin.x, -1200);
        panl.offsetMax = new Vector2(panl.offsetMax.x, -1200);
        }
        else if(ch_panel)
        {
            MenuManager.Save_Sett();
            au = button[5].GetComponent<AudioSource>();
            au.volume = MenuManager.volume/100f;
            au.PlayOneShot(click);
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
        au = button[2].GetComponent<AudioSource>();
        au.volume = MenuManager.volume/100f;
        au.PlayOneShot(click);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void Charters()
    {
        au = button[1].GetComponent<AudioSource>();
        au.volume = MenuManager.volume/100f;
        au.PlayOneShot(click);
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
        if(index == 0) 
        {
            chart[0].SetActive(true);
            chart[1].SetActive(false);
        } 
        if(index == 1) 
        {
            chart[1].SetActive(true);
            chart[0].SetActive(false);
        } 
        MenuManager.chart_pick = index;
    }

    public void BacktoMain()
    {
        au = button[2].GetComponent<AudioSource>();
        au.volume = MenuManager.volume/100f;
        au.PlayOneShot(click);
        SceneManager.LoadScene(0);
    }

    [System.Obsolete]
    public void GoInsta()
    {
        if(internetOn)
        {
            Application.OpenURL(URL[0]);
        }
    }

    [System.Obsolete]
    public void GoTT()
    {
        if(internetOn)
        {
            Application.OpenURL(URL[1]);
        }
    }
    [System.Obsolete]
     IEnumerator GetText() 
     {
        UnityWebRequest www = UnityWebRequest.Get("https://www.google.com");
        yield return www.Send();
 
        if(www.isError) 
        {
            internetOn = false;
        }
        else {
            // Show results as text
            internetOn = true;
        }
        for(int i = 0;i<social.Length;i++) social[i].SetActive(internetOn);
    }
}
