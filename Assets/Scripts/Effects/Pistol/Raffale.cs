using System.Collections;
using UnityEngine;

public class Raffale : EffectPistol
{
    public int nbOfShoot;
    private bool once = false;
    public float rifleDelay = 0.5f;

    public override void Effect(GameObject prefab, Vector3 position, Quaternion rotation, int nb)
    {
        Once();
        float delay = 0f;
        for (int i = 0; i < nbOfShoot; i++)
        {
            StartCoroutine(Rifle(delay, position, rotation));
            delay += rifleDelay;
        }
    }

    IEnumerator Rifle(float delay, Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(delay);
        Pistol.Instance.Shoot(position, rotation);
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
