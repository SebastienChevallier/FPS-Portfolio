using System.Collections;
using UnityEngine;

public class Raffale : EffectPistol
{
    public int bulletsPerBurst = 3; 
    private bool _once = false;
    public float timeBetweenBullets = 0.2f; 
    public float burstInterval = 1.0f;
    private bool isShooting;

    public override void Effect(Vector3 position, Quaternion rotation, int nb)
    {
        Once();
        StartCoroutine(ShootBurst(position, rotation));
    }

    IEnumerator ShootBurst(Vector3 position, Quaternion rotation)
    {
        isShooting = true;
        for (int i = 0; i < bulletsPerBurst; i++)
        {
            if (Pistol.Instance.ammo <= 0){ yield break; }

            rotation = Pistol.Instance.SetAim().rotation;
            position = Pistol.Instance.SetAim().position;
            Shoot(position, rotation);
            yield return new WaitForSeconds(timeBetweenBullets);
        } 
        yield return new WaitForSeconds(burstInterval);
        isShooting = false;
    }
}
