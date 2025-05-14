using UnityEngine;

public class VirusEnemy : EnemyBase
{
    [SerializeField] private float virusSpeed = 8f;
    [SerializeField] private int virusDamage = 25;

    protected override void Start()
    {
        
        maxHealth = 30f;
        damage = virusDamage;
        attackCooldown = Mathf.Infinity; 
        
        base.Start();
        
        
       
        if (agent != null)
        {
            agent.speed = virusSpeed;
            
        }
    }

   
    public override void DealDamage(GameObject target)
    {
       target.GetComponent<PlayerHealth>()?.TakeDamage(virusDamage);

        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I HIT THE ENEMY");
        
        if (isDead) return;
        
        if (other.CompareTag("Player"))
        {
            DealDamage(other.gameObject);
            Die(); 
        }
    }

    
   
    

    
}