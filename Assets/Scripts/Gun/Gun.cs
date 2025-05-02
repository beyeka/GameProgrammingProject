using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float nextTimeToFire = 0f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    
    void Update()
    {
        if (Input.GetButton("Fire1")&&Time.time>= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        
    }
    
     void Shoot()
     {
         muzzleFlash.Play();
         RaycastHit hit;
        
         if (Physics.Raycast( fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
         {
            // Debug.DrawLine(fpsCam.transform.position, hit.point, Color.red, 1f);

             EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();

             if (enemy!=null)
             {
                 enemy.TakeDamage(damage);
             }

             
             GameObject destroyEffect =Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
             Destroy(destroyEffect,0.3f);
         }


     }
    
    
    
    
}
