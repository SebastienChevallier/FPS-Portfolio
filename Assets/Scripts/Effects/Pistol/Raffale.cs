using System.Collections;
using UnityEngine;

public class Raffale : EffectPistol
{
    public int nbOfShoot;
    private bool once = false;
    public float rifleDelay = 1f;

    public override void Effect(GameObject prefab, Vector3 position, Quaternion rotation, int nb)
    {
        Once();
        float delay = 0f;
        for (int i = 0; i < nbOfShoot; i++)
        {
            StartCoroutine(Rifle(delay, position));
            delay += rifleDelay;
        }
    }

    IEnumerator Rifle(float delay, Vector3 position)
    {
        yield return new WaitForSeconds(delay);
        Quaternion rotation = Pistol.Instance.SetAim().rotation;
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
