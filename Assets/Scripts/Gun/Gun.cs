using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
    }
    
     void Shoot()
     {
         RaycastHit hit;
        
         if (Physics.Raycast( fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
         {
            // Debug.DrawLine(fpsCam.transform.position, hit.point, Color.red, 1f);

             EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();

             if (enemy!=null)
             {
                 enemy.TakeDamage(damage);
             }

         }


     }
    
    
    
    
}
