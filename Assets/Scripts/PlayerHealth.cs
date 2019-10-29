using UnityEngine;

public class PlayerHealth : HealthSystem
{
    public GameObject[] TimesAlives;
    private int currTimesAlive;
    // Start is called before the first frame update
    void Start()
    {
        currTimesAlive = TimesAlives.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DecreaseTimesAlives()
    {
        if (currTimesAlive > 0)
        {
            TimesAlives[currTimesAlive].SetActive(false);
            currTimesAlive--;
        }
    }


}
