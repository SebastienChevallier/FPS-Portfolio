using System;
using System.Collections;
using System.Collections.Generic;
using BaseTemplate.Behaviours;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Pistol : MonoSingleton<Pistol>
{
    [Header("Basics")]
    public GameObject bulletPrefab;
    public Transform bulletOrigin;
    public float fireRate;
    private bool isPerformed = false;

    [Header("Pistol Infos")] 
    public int ammo = 10;
    public int nbOfShoot = 0;

    private List<GameObject> bulletList;
    private float cdTime;

    [Header("Aim Infos")] 
    public GameObject aim;
    public LayerMask layerMask;
    private Camera _camera;

    [Header("Effects")] 
    public List<EffectPistol> effectPistols;
    public List<EffectBullet> effectBullets;

    private void Start()
    {
        effectPistols = new List<EffectPistol>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (cdTime >= 0)
        {
            cdTime -= Time.deltaTime;
        }

        if (isPerformed && ammo > 0 && cdTime < 0)
        {
            SetAim();
            cdTime = fireRate;
            ammo--;
            
            foreach (EffectPistol effect in effectPistols)
            {
                effect.Effect(bulletPrefab, bulletOrigin.position, bulletOrigin.rotation, nbOfShoot);
            }
        }
    }

    public void AddEffect(EffectPistol effectPistol)
    {
        effectPistols.Add(effectPistol);
        effectPistol.Once();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started) isPerformed = true;
        if (context.canceled) isPerformed = false;

    }

    public void Shoot(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = PoolingManager.Instance.GetPooledObject();

        if (bullet != null) {
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bullet.transform.position = SetAim().position;
            bullet.transform.rotation = rotation;
            
            
            foreach (EffectBullet effectBullet in effectBullets)
            {
                bulletScript.AddEffect(effectBullet);
            }
            
            bullet.SetActive(true);
            bulletScript.ApplyEffects();
        }
    }

    public Transform SetAim()                                                                                                                                                                                             
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,layerMask))
        {
            aim.transform.position = hit.point;
            hitPoint = hit.point;
        }
        return bulletOrigin;
    }
    
    public Vector3 hitPoint;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPoint, 0.5f); 
    }
}
