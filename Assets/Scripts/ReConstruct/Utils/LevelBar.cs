using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBar : MonoBehaviour
{
   
    public Sprite[] levelSprites;
    public int currentLevel
    {
        get
        {
            return level.currentLevel;
        }
        set
        {
            level.currentLevel = value;
        }
    }
    public int maxLevel
    {
        get
        {
            return level.maxLevel;
        }
        set
        {
            level.maxLevel = value;
        }
    }

    private Level level = new Level();

    void Start()
    {
        maxLevel = levelSprites.Length - 1;
        currentLevel = 0;
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
