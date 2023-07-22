using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [Header("Bullet Stats")]
    public float bulletSpeed;
    public float speedMultiplier;
    public float bulletDMG;
    public float DMGMultiplier;
    
    private Rigidbody _rb;
    
    [Header("Bullet Effects")]
    public List<EffectBullet> _effectBullets;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        foreach (EffectBullet effectBullet in _effectBullets)
        {
            if (effectBullet.onStart)
            {
                effectBullet.Effect(this);
            }
        }
    }

    public void AddEffect(EffectBullet effectBullet)
    {
        _effectBullets.Add(effectBullet);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (EffectBullet effectBullet in _effectBullets)
        {
            if (!effectBullet.onStart)
            {
                effectBullet.Effect(this);
            }
        }
        
        _rb.velocity = transform.forward * (bulletSpeed * speedMultiplier * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Untagged"))
        {
            Destroy(gameObject);
        }
    }
}
