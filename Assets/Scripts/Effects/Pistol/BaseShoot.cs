using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : EffectPistol
{
    private bool once = false;
    public override void Effect(GameObject prefab, Vector3 position, Quaternion rotation, int nbOfShoot)
    {
        Once();
        float delay = 0f;
        for (int i = 0; i < nbOfShoot; i++)
        {
            StartCoroutine(Shoot(delay));
            delay += 0.1f;
        }
        
        Pistol.Instance.Shoot();
    }

    public override bool GetOrigin()
    {
        return true;
    }
    
    public override void Once()
    {
        if (!once)
        {
            Pistol.Instance.nbOfShoot += 0;
            once = true;
        }
    }
    
    IEnumerator Shoot(float delay)
    {
        yield return new WaitForSeconds(delay);
        Pistol.Instance.Shoot();
    }
}
