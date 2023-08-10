using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBullet : MonoBehaviour
{
    public bool onStart;
    public virtual void Effect(Bullet bullet)
    {
        
    }
    
    public virtual void Once()
    {
        
    }

    public virtual void Collision(Collider col, GameObject bullet)
    {
        Debug.Log("NormalCollision");
        bullet.transform.gameObject.SetActive(false);
    }
}
