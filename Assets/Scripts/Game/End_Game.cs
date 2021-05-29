using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End_Game : MonoBehaviour
{
public static bool iswin;
 private AudioSource au;
public AudioClip click;
private ParticleSystem pr;
public GameObject partickle;
public Text text; 
void Start()
{
    pr = partickle.GetComponent<ParticleSystem>(); 
    pr.Play();
    if(iswin)
    {
        var main = pr.main;
        main.startColor = Color.green;
        main.startSpeed = 5f;
        switch(MenuManager.lang)
        {
        case "RU":
        {
            text.text = "Вы выиграли =)";
            break;
        }
        case "UA":
        {
            text.text = "Ви виграли =)";
            break;
        }
        case "EN":
        {
            text.text = "You win =)";
            break;
        }
        }
        text.color = Color.green;
    }
    if(!iswin)
    {
        var main = pr.main;
        main.startColor = Color.red;
        main.startSpeed = 2f;
        switch(MenuManager.lang)
        {
        case "RU":
        {
            text.text = "Вы проиграли =(";
            break;
        }
        case "UA":
        {
            text.text = "Ви програли =(";
            break;
        }
        case "EN":
        {
            text.text = "You lose =(";
            break;
        }
        }
        text.color = Color.red;
    }

}

public void ButtonUp(int index)
{
    au = this.GetComponent<AudioSource>();
    au.volume = MenuManager.volume/100f;
    au.PlayOneShot(click);
    switch(index)
    {
        case 0:
        {
            SceneManager.LoadScene(1);
            break;
        }
        case 1:
        {
            SceneManager.LoadScene(0);
            break;
        }
        case 2:
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
            break;
        }
    }
}
}
