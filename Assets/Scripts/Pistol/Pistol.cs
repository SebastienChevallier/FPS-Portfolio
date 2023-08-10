using System.Collections;
using System.Collections.Generic;
using BaseTemplate.Behaviours;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : MonoSingleton<Pistol>
{
    [Header("Basics")]
    public Transform bulletOrigin;
    public float fireRate;
    private bool _isPerformed = false;

    [Header("Pistol Infos")] 
    public int ammo = 10;
    public int maxAmmo = 10;
    public float timeForReload = 2f;
    public int nbOfShoot = 0;
    private float _cdTime;
    

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
        UIManager.Instance.ChangeAmmoText(ammo.ToString());
    }

    private void Update()
    {
        //CoolDown entre les tirs
        if (_cdTime >= 0)
        {
            _cdTime -= Time.deltaTime;
            
        }
        else
        {
            SetAim();
        }

        if (_isPerformed && ammo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(timeForReload);
        ammo = maxAmmo;
        UIManager.Instance.ChangeAmmoText(ammo.ToString());
    }

    public void AddEffect(EffectPistol effectPistol)
    {
        //Ajoute un effet de tir
        effectPistols.Add(effectPistol);
        effectPistol.Once();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        //Listener du bouton de tir
        if (context.started)
        {
            SetAim();
            _isPerformed = true;
        }
            
        if (context.canceled)
            _isPerformed = false;
        
        //Tirs
        if (context.performed && ammo > 0 && _cdTime < 0)
        {
            //Calcul de la visée, Reset du CD de tir et deduction d'une balle
            SetAim();
            _cdTime = fireRate;
            
            //Application differents effets de tirs
            foreach (EffectPistol effect in effectPistols)
            {
                effect.Effect(bulletOrigin.position, bulletOrigin.rotation, nbOfShoot);
            }
        }
    }


    public Transform SetAim()                                                                                                                                                                                             
    {
        //Tir d'un rayon
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,layerMask))
        {
            //Ajustement de notre Aim en fonction du hitPoint, fonctionne avec un AimConstraint
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
