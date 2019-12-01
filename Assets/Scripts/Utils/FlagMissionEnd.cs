using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagMissionEnd : ReboRootObject
{
    private bool isMissionEnd = false;

    public bool IsMissionEnd()
    {
        return isMissionEnd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                isMissionEnd = true;
                break;
        }
    }

}
