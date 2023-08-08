using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMultiplier : EffectBullet
{
    public float multiplier = 2f;

    private void Start()
    {
        onStart = true;
    }

    public override void Effect(Bullet bullet)
    {
        bullet.speedMultiplier += multiplier;
        Debug.Log("SpeedMultiply");
    }
}
