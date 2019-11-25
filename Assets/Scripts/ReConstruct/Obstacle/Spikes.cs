using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {
        damage = 100f;
        pushForce = new Vector2(-200, 500);
    }

   
}
