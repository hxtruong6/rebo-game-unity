using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class InGameManager : ReboRootObject
{

    private Transform trailer;
    private Transform mainMenu;

    void Start()
    {
        trailer = transform.Find("Trailer");
        mainMenu = transform.Find("MainMenu");

        StartCoroutine(EndTrailer());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public IEnumerator EndTrailer()
    {
        yield return new WaitForSeconds((float)trailer.gameObject.GetComponent<VideoPlayer>().length);

        EndTrailerNow();
    }

    public void EndTrailerNow()
    {
        trailer.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
