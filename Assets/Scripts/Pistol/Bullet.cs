using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("BulletComponents")] 
    private TrailRenderer _trail;
    
    [Header("Bullet Stats")]
    public float bulletSpeed;
    public float speedMultiplier;
    public float bulletDMG;
    public float DMGMultiplier;
    
    private Rigidbody _rb;
    
    [Header("Bullet Effects")]
    public List<EffectBullet> _effectBullets;

    

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;
        _trail = GetComponent<TrailRenderer>();
        Invoke("Destroy", 10f);
        _trail.Clear();
    }

    private void OnDisable()
    {
        ResetBulletInfo();
    }

    public void ApplyEffects()
    {
        if (_effectBullets != null)
        {
            foreach (EffectBullet effectBullet in _effectBullets)
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
        if (_effectBullets != null)
        {
            foreach (EffectBullet effectBullet in _effectBullets)
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
            transform.gameObject.SetActive(false);
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
        bulletDMG = 10;
        DMGMultiplier = 1;
        
        _effectBullets.Clear();
        _effectBullets = new List<EffectBullet>();
    }

    public void AddEffect(EffectBullet effectBullet)
    {
        _effectBullets.Add(effectBullet);
    }
    
}
