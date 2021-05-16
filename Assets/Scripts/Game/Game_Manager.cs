﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public GameObject []character;      
    public GameObject batt;             // Префаб батарейки 
    public GameObject[] items;          // массив всех предметов для поиска
    public static float time;           // время на поиски 2 минуты + 30сек на предмет
    public static int amount;           // количество предметов
    public Vector3[] battery;           // все возможные местоположения батареек
    public static int[] items_spawn;
        
    public void Awake()
    {
        amount = Random.Range(1,items.Length+1);
        time = 120 + 30*amount;
        items_spawn = new int [amount];
        for(int i = 0; i<amount; i++) items_spawn[i] = -1;


        for(int i = 0; i<amount; i++)
        {
            metka:
            int k = Random.Range(0,items.Length);
            for(int j = 0 ;j<i;j++)
            {
                if(items_spawn[j] == k) goto metka;
            }
            items_spawn[i] = k;
        }
        for(int i = 0; i<amount; i++) Instantiate(items[items_spawn[i]]);
    }

    void Start()
    {
        Instantiate(character[MenuManager.chart_pick]); // создание персонажа
        character[MenuManager.chart_pick].transform.position = new Vector3(0f, 0.67f, 0f); // начальное место положение персонажа 
        
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
        }
    }
    public void FixedUpdate()
    {
        bool endGame;
        if(!FirstPersonMovement.pause && time>0)
        time-= Time.deltaTime;
        if(time<=0)
        {
            endGame = false;
            Lose_Game(endGame);
        }
        if(time > 0 && amount == 0)
        {
            endGame = true;
            Lose_Game(endGame);
        }     
    }
    public static void ItemsUp()
    {
        amount--;
    }

    public void Lose_Game(bool win)
    {
        End_Game.iswin = win;
        SceneManager.LoadScene("endGame");
    }
}
