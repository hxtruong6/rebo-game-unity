using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBar : MonoBehaviour
{
   
    public Sprite[] levelSprites;
    public int currentLevel;
    public int maxLevel;

    private Level level;

    void Start()
    {
        maxLevel = levelSprites.Length - 1;
        currentLevel = 0;

        level = new Level(maxLevel);
    }

    private void Update()
    {
        LevelChanged();
    }

    private void LevelChanged()
    {
        transform.Find("Bar").GetComponent<SpriteRenderer>().sprite = levelSprites[currentLevel];
    }

    public int LevelUp()
    {
        currentLevel--;
        if (currentLevel < 0)
            currentLevel = 0;
            
        LevelChanged();

        return currentLevel;
    }

    public int LevelDown()
    {
        currentLevel++;
        if (currentLevel > maxLevel)
            currentLevel = maxLevel;

        LevelChanged();

        return currentLevel;
    }

    public int Reset()
    {
        currentLevel = 0;

        LevelChanged();

        return currentLevel;
    }

    public float GetAdditionDamage()
    {
        return currentLevel * 10;
    }
}
