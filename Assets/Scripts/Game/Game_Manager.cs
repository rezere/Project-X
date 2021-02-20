using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public GameObject []character;
    public GameObject batt;
    public Vector3 []battery;// все возможные местоположения батареек

    void Start()
    {
        Instantiate(character[0]); // создание персонажа
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
            Debug.Log(kol_batt[i]+"\\");
        }
        Debug.Log(kol_batt.Length);
    }

    void Update()
    {
        
    }
}
