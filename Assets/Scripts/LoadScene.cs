using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string ScenceName = "Missions";
    public void LoadMissionsScence()
    {
        StartCoroutine(LoadYourAsyncScene(ScenceName));
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
