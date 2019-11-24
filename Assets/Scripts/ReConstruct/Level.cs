using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Object
{
   
    public int maxLevel = 6;
    public int currentLevel;

    public Level(int maxLevel)
    {
        this.maxLevel = maxLevel;
    }

    private void LevelChanged()
    {
        if (currentLevel > maxLevel)
            currentLevel = maxLevel;
        if (currentLevel < 0)
            currentLevel = 0;
    }

    public int LevelUp()
    {
        currentLevel++;
        LevelChanged();
        return currentLevel;
    }

    public int LevelDown()
    {
        currentLevel--;
        LevelChanged();

        return currentLevel;
    }

    public int Reset()
    {
        currentLevel = 0;
        LevelChanged();

        return currentLevel;
    }

    public float GetDamage()
    {
        return currentLevel * 10;
    }
}
