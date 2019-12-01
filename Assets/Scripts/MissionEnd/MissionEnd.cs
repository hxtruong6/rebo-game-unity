using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionEnd : ReboRootObject
{
    public string MissionSceneName = "Missions";
    public string CurrentSceneName = "Mission01";

    private string MainMenuScenceName = "Menu";
    public Character player;

    private void Start()
    {
        if (player != null)
        {
            if (player.isAlive())
            {
                transform.Find("Completed").gameObject.SetActive(true);
                transform.Find("Failed").gameObject.SetActive(false);

                //GetComponent<MissionCompleted>().UpdateNumberOfEnemiesAnnihilated(player.numberOfEnemiesAnnihilated);
            }
            else
            {
                transform.Find("Completed").gameObject.SetActive(false);
                transform.Find("Failed").gameObject.SetActive(true);
            }

            player.gameObject.SetActive(false);
        }
    }

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

    public void GoMissionsScene()
    {
        StartCoroutine(LoadYourAsyncScene(MissionSceneName));
    }

    public void GoToMainMenuGame()
    {
        StartCoroutine(LoadYourAsyncScene(MainMenuScenceName));
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