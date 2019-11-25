using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSolider : Enemy
{


    protected override void LookToTheLeft(bool toLeft)
    {
        characterSprite.flipX = !toLeft;
    }

    public override bool LookToTheLeft()
    {
        return !characterSprite.flipX;
    }


}
