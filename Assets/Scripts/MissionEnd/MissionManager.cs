using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : ReboRootObject
{
    public string MissionSceneName = "Missions";
    public string CurrentSceneName = "Mission01";

    private string MainMenuScenceName = "Menu";    

    public void SetComplete(Dictionary<EnemyType, int> numberOfEnemiesAnnihilated)
    {
        transform.Find("Completed").gameObject.SetActive(true);
        transform.Find("Completed").gameObject.GetComponent<MissionCompleted>().UpdateNumberOfEnemiesAnnihilated(numberOfEnemiesAnnihilated);

        transform.Find("Failed").gameObject.SetActive(false);
    }

    public void SetFail()
    {
        transform.Find("Completed").gameObject.SetActive(false);
        transform.Find("Failed").gameObject.SetActive(true);
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