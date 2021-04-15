using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public GameObject []character;
    public GameObject batt;
    public GameObject[] items;
    public static float time;
    public Vector3 []battery;// все возможные местоположения батареек
        
    public void Awake()
    {
        int amount = Random.Range(1,items.Length+1);
        time = 120 + 30*amount;
        Debug.Log(time);
        int []items_spawn = new int [amount];
        for(int i = 0; i<amount; i++)
        {
            metka:
            int k = Random.Range(0,items.Length);
            for(int j = 0 ;j<i;j++)
            {
                if(items_spawn[j] == k) goto metka;
                else 
                {
                    items_spawn[j] = k;
                    Instantiate(items[k]);
                }
            }
        }
    }

    void Start()
    {
        Instantiate(character[MenuManager.chart_pick]); // создание персонажа
        character[0].transform.position = new Vector3(0f, 0.67f, 0f); // начальное место положение персонажа 
        
        int[] kol_batt = new int[Random.Range(1, battery.Length)];
        int pos;
        for (int i = 0; i<kol_batt.Length; i++)
        {
            met:
            pos = Random.Range(1, battery.Length);
            for(int j = 0;j<=0;j++)
            {
                if (kol_batt[j] == pos) goto met;
            }
            kol_batt[i] = pos;
            Instantiate(batt);
            this.batt.transform.position = new Vector3(battery[kol_batt[i]].x, battery[kol_batt[i]].y, battery[kol_batt[i]].z);
            //Debug.Log(kol_batt[i]+"\\");
        }
        Debug.Log(kol_batt.Length);
    }
    public void FixedUpdate()
    {
        if(!FirstPersonMovement.pause || time>0)
        time-= Time.deltaTime;
        if(time<=0)
        {
            Lose_Game();
        }      
    }

    public void Lose_Game()
    {
        SceneManager.LoadScene(0);
    }
}
