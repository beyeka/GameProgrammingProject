using System;
using UnityEngine;
using System.Collections;
using TMPro;
public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float nextTimeToFire = 0f;

    public int magazine;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReload = false;
    private bool infiniteAmmo = false;
    
    public Animator animator;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public TextMeshProUGUI ammoText;
    void Start()
    {
        currentAmmo = magazine;
        AmmoPrinter(magazine);
    }

    void OnEnable()
    {
        AmmoPrinter(currentAmmo);
        isReload = false;
        animator.SetBool("Reloading",false);
    }

    void Update()
    {
        if (isReload)
        {
            return;
        }
        if (currentAmmo <= 0|| Input.GetKeyDown("R"))
        {
            AmmoPrinter(0);
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1")&&Time.time>= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        
    }
    
     void Shoot()
     {
         muzzleFlash.Play();
         
         if(!infiniteAmmo)
         currentAmmo--;
         
         RaycastHit hit;
        
         if (Physics.Raycast( fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
         {
             EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();

             if (enemy!=null)
             {
                 enemy.TakeDamage(damage);
             }
             
             GameObject destroyEffect =Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
             Destroy(destroyEffect,0.3f);
         }

         AmmoPrinter(currentAmmo);
     }


     public void AmmoPrinter(int currentAmmo)
     {
         ammoText.text = currentAmmo + "/" + magazine; 
     }
     IEnumerator Reload()
     {
         isReload = true;
         
         animator.SetBool("Reloading",true);
         yield return new WaitForSeconds(reloadTime-0.25f);
         animator.SetBool("Reloading",false);
         yield return new WaitForSeconds(0.25f);
         
         currentAmmo = magazine;
         isReload = false;
         
     }
    
     public void ApplyFireRateBoost(float multiplier, float duration)
     {
         StartCoroutine(FireRateBoostRoutine(multiplier, duration));
     }

     private IEnumerator FireRateBoostRoutine(float multiplier, float duration)
     {
         fireRate *= multiplier;
         yield return new WaitForSeconds(duration);
         fireRate /= multiplier;
     }
     
     public void EnableInfiniteAmmo(float duration)
     {
         StartCoroutine(InfiniteAmmoRoutine(duration));
     }

     private IEnumerator InfiniteAmmoRoutine(float duration)
     {
         infiniteAmmo = true;
         yield return new WaitForSeconds(duration);
         infiniteAmmo = false;
     }
     
     
     
}
