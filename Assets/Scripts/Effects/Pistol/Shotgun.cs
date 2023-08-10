using System.Collections;
using UnityEngine;

public class Shotgun : EffectPistol
{
    public int nbOfBullet = 3;
    public float minDisper, maxDisper;
    public float fireRate;

    private bool _once;
    public override void Effect(Vector3 position, Quaternion rotation, int nbOfShoot)
    {
        Once();
        for (int i = 0; i < nbOfBullet * nbOfShoot; i++)
        {
            rotation = Pistol.Instance.SetAim().rotation;
            Quaternion rota = new Quaternion(
                rotation.x + Random.Range(minDisper, maxDisper),
                rotation.y + Random.Range(minDisper, maxDisper),
                rotation.z + Random.Range(minDisper, maxDisper),
                rotation.w);
            
            Shoot(position, rota);
        }
    }

    public override void Once()
    {
        if (!_once)
        {
            Pistol.Instance.fireRate = fireRate;
            if(Pistol.Instance.effectPistols[0].GetOrigin())
                Pistol.Instance.effectPistols.Remove(Pistol.Instance.effectPistols[0]);
            _once = true;
        }
    }
}
