using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : EffectPistol
{
    private bool _once = false;
    public override void Effect(Vector3 position, Quaternion rotation, int nbOfShoot)
    {
        Once();
        Shoot(position, Pistol.Instance.SetAim().rotation);
    }

    public override bool GetOrigin()
    {
        return true;
    }
    
    public override void Once()
    {
        if (!_once)
        {
            Pistol.Instance.nbOfShoot += 1;
            _once = true;
        }
    }
}
