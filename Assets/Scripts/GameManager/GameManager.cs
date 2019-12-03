using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Character player;
    public MissionManager missionManager;
    public FlagMissionEnd missionFlag;

    public bool isPausing = false;

    protected Transform gamePaused;
    protected GameObject[] arrayGameObject;

    void Start()
    {
        missionManager.gameObject.SetActive(false);
        gamePaused = transform.Find("GamePaused");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isAlive() || missionFlag.IsMissionEnd())
        {
            missionManager.gameObject.SetActive(true);
            if (player.isAlive())
                missionManager.SetComplete(player.numberOfEnemiesAnnihilated);
            else
                missionManager.SetFail();

            player.gameObject.SetActive(false);
        }
        else
        {
            if (KeyPausePressed())
            {
                if (isPausing)
                    GameContine();
                else
                    GamePause();
            }
        }
    }

    public void GamePause()
    {
        isPausing = true;
        gamePaused.gameObject.SetActive(true);

        SetGameObjectsActive(false);
    }

    public void GameContine()
    {
        isPausing = false;
        gamePaused.gameObject.SetActive(false);

        SetGameObjectsActive(true);
    }

    public void GameExit()
    {
        missionManager.gameObject.SetActive(true);
        missionManager.GoBack();
    }

    protected bool KeyPausePressed()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    protected void SetGameObjectsActive(bool value)
    {
        player.gameObject.SetActive(value);
        if (value == false)
            arrayGameObject = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < arrayGameObject.Length; i++)
            arrayGameObject[i].gameObject.SetActive(value);
    }
}
