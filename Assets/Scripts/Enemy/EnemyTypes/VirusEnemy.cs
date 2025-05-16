using UnityEngine;

public class VirusEnemy : EnemyBase
{
    private VirusEnemySO VirusData => (VirusEnemySO)GetData(); 

    protected override void Start()
    {
        base.Start();

        if (agent != null)
            agent.speed = VirusData.overrideMoveSpeed;
    }

    public override void DealDamage(GameObject target)
    {
        if (target.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(VirusData.damage); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Virus collided with player, exploding!");
            DealDamage(other.gameObject);
            Die();
        }
    }
}
