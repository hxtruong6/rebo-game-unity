using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Character player;
    public MissionEnd missionEnd;

    // Start is called before the first frame update
    void Start()
    {
        missionEnd.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isAlive())
        {
            missionEnd.gameObject.SetActive(true);
        }
    }
}
