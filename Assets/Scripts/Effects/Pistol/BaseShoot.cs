using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : EffectPistol
{
    private bool once = false;
    public override void Effect(GameObject prefab, Vector3 position, Quaternion rotation, int nbOfShoot)
    {
        Once();
        Pistol.Instance.Shoot(position, Pistol.Instance.SetAim().rotation);
    }

    public override bool GetOrigin()
    {
        return true;
    }
    
    public override void Once()
    {
        if (!once)
        {
            Pistol.Instance.nbOfShoot += 1;
            once = true;
        }
    }
}
