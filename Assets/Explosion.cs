using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionDamage = 10;
    public float explosionForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Touched");
            Movement playerMovement = other.GetComponent<Movement>();
            Vector3 dir = (other.transform.position - transform.position).normalized;
            dir *= explosionForce;
            
            playerMovement._controller.Move(dir);
        }
    }
}
