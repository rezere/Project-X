using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items_Spawn : MonoBehaviour
{
   public Vector3[] spawn;
    void Start()
    {
        int s = Random.Range(0,spawn.Length);
        this.gameObject.transform.position = new Vector3(spawn[s].x, spawn[s].y, spawn[s].z); 
    }

}
