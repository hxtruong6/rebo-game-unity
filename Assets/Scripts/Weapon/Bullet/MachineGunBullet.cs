using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunBullet : Bullet
{
    protected override float GetCurrentDamageFor(GameObject enemy)
    {
        float d = base.GetCurrentDamageFor(enemy);

        if (enemy.GetComponent<Enemy>().type == EnemyType.Solider)
            d *= 1.5f;
        else if (enemy.GetComponent<Enemy>().type == EnemyType.Slug)
            d *= 0.6f;

        return d;
    }
}
