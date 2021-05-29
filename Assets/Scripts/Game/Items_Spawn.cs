using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Items_Spawn : MonoBehaviour
{
    public Vector3[] spawn;
    public Vector3[] spawn_lock2;
    void Start()
    {
        int s;
        if(SceneManager.GetActiveScene().name == "Level_1")
        {
            s = Random.Range(0,spawn.Length);
            this.gameObject.transform.position = new Vector3(spawn[s].x, spawn[s].y, spawn[s].z);
        }
        else if(SceneManager.GetActiveScene().name == "Level_2")
        {
            s = Random.Range(0,spawn_lock2.Length);
            this.gameObject.transform.position = new Vector3(spawn_lock2[s].x, spawn_lock2[s].y, spawn_lock2[s].z);
        }
    }
    public void DestroyItems()
    {
        Destroy(this.gameObject);
    }
}
