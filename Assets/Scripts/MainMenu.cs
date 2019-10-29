using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    const string MISSION_1 = "Mission1";
    public void PlayGame()
    {
        StartCoroutine(LoadYourAsyncScene());
    }


    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(MISSION_1);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}
