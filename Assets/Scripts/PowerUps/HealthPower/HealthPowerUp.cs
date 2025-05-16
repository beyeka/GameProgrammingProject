using UnityEngine;

public class HealthPowerUp : PowerUp
{
    [SerializeField] private float healAmount = 25f;

    protected override void Apply(GameObject player)
    {
        var health = player.GetComponent<PlayerHealth>();
        
        if (health != null)
        {
            health.RestoreHealth(healAmount);
        }
        
        
    }
    
    
}