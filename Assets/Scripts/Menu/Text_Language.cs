using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Language : MonoBehaviour
{
    public string[] mas_text;
    private Text txt;

    void Start()
    {
        txt = this.gameObject.GetComponent<Text>();
        if(MenuManager.lang == "RU") txt.text = mas_text[0];
        else if(MenuManager.lang == "UA") txt.text = mas_text[1];
        else if(MenuManager.lang == "EN") txt.text = mas_text[2];
    }

    void Update()
    {
        if(MenuManager.lang == "RU") txt.text = mas_text[0];
        else if(MenuManager.lang == "UA") txt.text = mas_text[1];
        else if(MenuManager.lang == "EN") txt.text = mas_text[2];
    }
}