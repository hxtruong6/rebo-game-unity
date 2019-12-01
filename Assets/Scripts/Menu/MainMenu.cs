using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    const string MISSIONS = "Missions";
    public GameObject SettingPanel;
    public void PlayGame()
    {
        StartCoroutine(LoadYourAsyncScene(MISSIONS));
    }

    public void SettingGameToggle()
    {
        SettingPanel.SetActive(!SettingPanel.activeSelf);
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

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}
