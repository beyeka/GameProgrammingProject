// ScriptableObject for a health power-up. Restores health to the player when applied.
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPowerUp", menuName = "PowerUps/Health")]
public class HealthPowerUpSO : PowerUpBaseSO
{
    public float healAmount = 25f;

    // Gets PlayerHealth component and restores a fixed amount of health.
    public override void Apply(GameObject player)
    {
        var health = player.GetComponent<PlayerHealth>();
        health?.RestoreHealth(healAmount);
    }
    
    
}