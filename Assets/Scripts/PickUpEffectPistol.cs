using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEffectPistol : MonoBehaviour
{
    private EffectPistol _effect;
    private bool _picked;

    private void Start()
    {
        _effect = GetComponent<EffectPistol>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_picked)
        {
            Pistol.Instance.AddEffect(_effect);
            _picked = true;
        }
    }
}
