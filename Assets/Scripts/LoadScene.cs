using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string MissionSceneName = "Missions";
    public string CurrentSceneName = "Mission01";

    public void LoadMissions()
    {
        StartCoroutine(LoadYourAsyncScene(MissionSceneName));
    }

    public void PlayAgain()
    {
        StartCoroutine(LoadYourAsyncScene(CurrentSceneName));
    }

    public void GoBack()
    {
        StartCoroutine(LoadYourAsyncScene(MissionSceneName));
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
