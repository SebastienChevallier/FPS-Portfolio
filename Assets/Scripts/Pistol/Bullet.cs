using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("BulletComponents")] 
    private TrailRenderer _trail;
    
    [Header("Bullet Stats")]
    public float bulletSpeed;
    public float speedMultiplier;
     public float bulletDmg;
    public float dmgMultiplier;
    
    private Rigidbody _rb;
    
    [Header("Bullet Effects")]
    public List<EffectBullet> effectBullets;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;
        _trail = GetComponent<TrailRenderer>();
        _trail.Clear();
    }

    private void OnDisable()
    {
        ResetBulletInfo();
    }

    public void ApplyEffects()
    {
        if (effectBullets != null)
        {
            foreach (EffectBullet effectBullet in effectBullets)
            {
                if (effectBullet.onStart)
                {
                    effectBullet.Effect(this);
                }
            }
        }
    }
    
    void Update()
    {
        if (effectBullets != null)
        {
            foreach (EffectBullet effectBullet in effectBullets)
            {
                if (!effectBullet.onStart)
                {
                    effectBullet.Effect(this);
                }
            }
        }
        
        _rb.velocity = transform.forward * (bulletSpeed * speedMultiplier * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Untagged"))
        {
            if (effectBullets != null)
            {
                foreach (EffectBullet effectBullet in effectBullets.ToList())
                {
                    if (!effectBullet.onStart)
                    {
                        effectBullet.Collision(other, gameObject);
                    }
                }
            }
        }
    }

    public void Destroy()
    {
        transform.gameObject.SetActive(false);
    }
    
    public void ResetBulletInfo()
    {
        bulletSpeed = 1000;
        speedMultiplier = 1;
        bulletDmg = 10;
        dmgMultiplier = 1;
        
        effectBullets.Clear();
        effectBullets = new List<EffectBullet>();
    }

    public void AddEffect(EffectBullet effectBullet)
    {
        effectBullets.Add(effectBullet);
    }
    
}
