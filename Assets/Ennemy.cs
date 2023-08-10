using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public float health;
    public float maxHealth;


    private void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            float dmg = bullet.bulletDmg * bullet.dmgMultiplier;
            TakeDmg(dmg);
        }

        if (other.CompareTag("Explosion"))
        {
            Explosion explosion = other.GetComponent<Explosion>();
            TakeDmg(explosion.explosionDamage);
        }
    }

    public void TakeDmg(float dmg)
    {
        Debug.Log("Hit : " + dmg );
        if (dmg < health)
        {
            health -= dmg;
            return;
        }
        
        if(dmg >= health)
        {
            Debug.Log("Death");
            Destroy(gameObject);
        }
    }
}
