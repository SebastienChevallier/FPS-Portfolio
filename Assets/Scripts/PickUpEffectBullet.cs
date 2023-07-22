using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEffectBullet : MonoBehaviour
{
    private EffectBullet _effect;
    private bool _picked;

    private void Start()
    {
        _effect = GetComponent<EffectBullet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_picked)
        {
            Pistol.Instance.effectBullets.Add(_effect);
            _picked = true;
        }
    }
}
