using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplode : EffectBullet
{
    [SerializeField] private GameObject explosionSphere;
    public override void Collision(Collider col, GameObject bullet)
    {
        GameObject explosion = Instantiate(explosionSphere, bullet.transform.position, bullet.transform.rotation);
        Destroy(explosion, 0.2f);
    }
}
