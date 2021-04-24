using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
      StartCoroutine(LoadAsync());   
    }
    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);
        while(!asyncLoad.isDone)
        {
            slider.value = asyncLoad.progress;
            yield return null;
        }
        yield return new WaitForSeconds(4.1f);
    }
}
