using System.Collections;
using UnityEngine;

public class Shotgun : EffectPistol
{
    public int nbOfBullet = 3;
    public float minDisper, maxDisper;
    public float fireRate;

    private bool once;
    public override void Effect(GameObject prefab, Vector3 position, Quaternion rotation, int nbOfShoot)
    {
        Once();
        for (int i = 0; i < nbOfBullet + nbOfShoot; i++)
        {
            Quaternion rota = new Quaternion(
                rotation.x + Random.Range(minDisper, maxDisper),
                rotation.y + Random.Range(minDisper, maxDisper),
                rotation.z + Random.Range(minDisper, maxDisper),
                rotation.w);
            
            GameObject bullet = Instantiate(prefab, position, rota);
            Destroy(bullet, 5f);
        }
    }

    public override void Once()
    {
        if (!once)
        {
            Pistol.Instance.fireRate = fireRate;
            if(Pistol.Instance.effectPistols[0].GetOrigin())
                Pistol.Instance.effectPistols.Remove(Pistol.Instance.effectPistols[0]);
            once = true;
        }
    }
}
