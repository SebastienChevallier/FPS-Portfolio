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

    [Header("Pistol Infos")] 
    public int ammo = 10;

    public int nbOfShoot = 0;

    private List<GameObject> bulletList;
    private float cdTime;
    
    

    [Header("Aim Infos")] 
    public AimConstraint _aimConstraint;
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
        SetAim();
        
        if (cdTime >= 0)
        {
            cdTime -= Time.deltaTime;
        }
    }

    public void AddEffect(EffectPistol effectPistol)
    {
        effectPistols.Add(effectPistol);
        effectPistol.Once();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && ammo > 0 && cdTime < 0)
        {
            cdTime = fireRate;
            ammo--;
            
            foreach (EffectPistol effect in effectPistols)
            {
                effect.Effect(bulletPrefab, bulletOrigin.position, bulletOrigin.rotation, nbOfShoot);
            }
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletOrigin.position, bulletOrigin.rotation);
        foreach (EffectBullet effectBullet in effectBullets)
        {
            bullet.GetComponent<Bullet>().AddEffect(effectBullet);
        }
        Destroy(bullet, 5f);
    }
    
    public void SetAim()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            aim.transform.position = hit.point;
        }
    }

    
    
   
}
