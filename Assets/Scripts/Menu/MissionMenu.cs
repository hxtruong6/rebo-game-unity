using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionMenu : MonoBehaviour
{
    public string Mission1Name = "Game";
    public string Mission2Name = "Game";
    public string Mission3Name = "Game";

    public void Mission1Play()
    {
        StartCoroutine(LoadYourAsyncScene(Mission1Name));
    }

    public void Mission2Play()
    {
        StartCoroutine(LoadYourAsyncScene(Mission2Name));
    }

    public void Mission3Play()
    {
        StartCoroutine(LoadYourAsyncScene(Mission3Name));
    }


    IEnumerator LoadYourAsyncScene(string scence)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scence);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
