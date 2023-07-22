using System.Collections;
using UnityEngine;

public class Raffale : EffectPistol
{
    public int nbOfShoot;
    private bool once = false;

    public override void Effect(GameObject prefab, Vector3 position, Quaternion rotation, int nb)
    {
        Once();
    }

    public override void Once()
    {
        if (!once)
        {
            Pistol.Instance.nbOfShoot += nbOfShoot;
            once = true;
        }
    }
    
    
}
