using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPistol : MonoBehaviour
{
    public virtual void Effect(GameObject prefab, Vector3 position, Quaternion rotation, int nbOfShoot)
    {
        
    }
    
    public virtual void Once()
    {
        
    }

    public virtual bool GetOrigin()
    {
        return false;
    }
}
