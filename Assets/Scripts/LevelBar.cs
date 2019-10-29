using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBar : MonoBehaviour
{
    public Sprite level0, level1, level2, level3, level4, level5, level6;
    public const int maxLevel = 6;
    public int currentLevel;
    public Vector2 levelBarPos;

    private Sprite[] levels;

    void Start()
    {
        levelBarPos = new Vector2(50, 100);
        currentLevel = 0;
        levels = new Sprite[maxLevel + 1] { level0, level1, level2, level3, level4, level5, level6 };
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("Bar").GetComponent<SpriteRenderer>().sprite = levels[currentLevel];
    }

    public float GetDamage()
    {
        return currentLevel * 10;
    }
}
