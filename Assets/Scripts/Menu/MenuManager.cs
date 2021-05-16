using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Slider []scroll;
    public GameObject music_obj;
    private AudioSource au;
    public AudioClip []mus;
    public static int volume,volume_mus,chart_pick;
    public static string lang = "RU";
    public Text []volumTxt;

    //public Material[] sky;

    public static Save sv = new Save(); // экземпляр

    private void Awake() //вызывается до старта 
    {
        if(PlayerPrefs.HasKey("SV"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            volume = sv.vol;
            volume_mus = sv.vol_mus;
            lang = sv.language;
            chart_pick = sv.chart;
        }
        scroll[0].value = volume;
        scroll[1].value = volume_mus;
    }
    void Start()
    {
        //RenderSettings.skybox = sky[0];
        au = music_obj.GetComponent<AudioSource>();
        au.volume = volume_mus/100f;
        au.PlayOneShot(mus[Random.Range(0,3)]);
        volume = (int)scroll[0].value;
        volume_mus = (int)scroll[1].value;
        volumTxt[0].text = volume + "%";
        volumTxt[1].text = volume_mus + "%";
    }

    void Update()
    {
        volume = (int)scroll[0].value;
        volume_mus = (int)scroll[1].value;
        volumTxt[0].text = volume + "%";
        volumTxt[1].text = volume_mus + "%";
        au.volume = volume_mus / 100f;
    }

    private void OnApplicationQuit()
    {
        Save_Sett();
    }

    public  static void Start_Sett()
    {
        if(PlayerPrefs.HasKey("SV"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            volume = sv.vol;
            volume_mus = sv.vol_mus;
            lang = sv.language;
            chart_pick = sv.chart;
        }
    }
    public  static void Save_Sett()
    {
        sv.vol = volume;
        sv.vol_mus = volume_mus;
        sv.language = lang;
        sv.chart = chart_pick;
        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }
}

public class Save
{
    public int vol, vol_mus, chart;
    public string language;
}
