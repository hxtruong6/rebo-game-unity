using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionCompleted : ReboRootObject
{ 
    public void UpdateNumberOfEnemiesAnnihilated(Dictionary<EnemyType, int> numberOfEnemiesAnnihilated)
    {
        transform.Find("Solider").Find("Text").GetComponent<Text>().text = "x0";
        transform.Find("Slug").Find("Text").GetComponent<Text>().text = "x0";
        if (numberOfEnemiesAnnihilated.ContainsKey(EnemyType.Solider))
        {
            transform.Find("Solider").Find("Text").GetComponent<Text>().text = "x" + numberOfEnemiesAnnihilated[EnemyType.Solider];
        }

        if (numberOfEnemiesAnnihilated.ContainsKey(EnemyType.Slug))
        {
            transform.Find("Slug").Find("Text").GetComponent<Text>().text = "x" + numberOfEnemiesAnnihilated[EnemyType.Slug];
        }
    }
}
