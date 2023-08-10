using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPistol : MonoBehaviour
{
    public virtual void Effect(Vector3 position, Quaternion rotation, int nbOfShoot)
    {
        //Logique des effets 
    }
    
    public virtual void Once()
    {
        //Logique a appliquer une seule fois, comme des modifs de stats
    }
    
    public virtual void Shoot(Vector3 position, Quaternion rotation)
    {
        //Tir Basic, recupere une balle du PoolingManager
        GameObject bullet = PoolingManager.Instance.GetPooledObject();
        List<EffectBullet> effectBullets = Pistol.Instance.effectBullets;

        //Set la position de la balle au bout du canon du pistolet
        if (bullet != null) {
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            
            
            foreach (EffectBullet effectBullet in effectBullets)
            {
                //Ajoute tout les effets de balles à la balle tirée
                bulletScript.AddEffect(effectBullet);
            }
            
            //Active la balle et ses effets
            bullet.SetActive(true);
            bulletScript.ApplyEffects();
            
            Pistol.Instance.ammo--;
            UIManager.Instance.ChangeAmmoText(Pistol.Instance.ammo.ToString());
        }
    }

    public virtual bool GetOrigin()
    {
        //Retourne si c'est le tir original, false par default
        return false;
    }
}
