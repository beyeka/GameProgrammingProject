using UnityEngine;

[CreateAssetMenu(fileName = "HealthPowerUp", menuName = "PowerUps/Health")]
public class HealthPowerUpSO : PowerUpBaseSO
{
    public float healAmount = 25f;

    public override void Apply(GameObject player)
    {
        var health = player.GetComponent<PlayerHealth>();
        health?.RestoreHealth(healAmount);
    }
}