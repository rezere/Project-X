using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TV : MonoBehaviour
{
    public bool OnTv;
    public VideoClip[] video;
    private VideoPlayer vp;
    public GameObject screen;
    // Start is called before the first frame update
    void Start()
    {
        vp = screen.GetComponent<VideoPlayer>();
        OnTv = false;
        vp.enabled = false;
    }
    
     public void WatchTV()
     {
        if(OnTv)
        {
            vp.enabled = true;
            vp.clip = video[Random.Range(0,video.Length)];
            vp.isLooping = true;
            vp.SetDirectAudioVolume(0, MenuManager.volume/100f);
        }
        if(!OnTv)
        {
            vp.enabled = false;
        }
     }
}
