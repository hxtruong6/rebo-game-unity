using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Intro;
    public float WAIT_TIME = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence()
    {
        yield return new WaitForSeconds(WAIT_TIME);
        Menu.SetActive(true);
        Intro.SetActive(false);
        yield return new WaitForSeconds(WAIT_TIME);

    }


}
