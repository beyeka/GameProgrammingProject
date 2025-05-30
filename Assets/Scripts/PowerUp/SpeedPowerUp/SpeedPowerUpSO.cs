// ScriptableObject for a combined speed, fire rate, and infinite ammo power-up. Applies multiple temporary boosts.
using UnityEngine;
[CreateAssetMenu(fileName = "SpeedAndAmmoPowerUp", menuName = "PowerUps/SpeedAndAmmo")]
public class SpeedPowerUpSO : PowerUpBaseSO
{
    public float moveSpeedMultiplier = 1.5f;
    public float fireRateMultiplier = 1.5f;

    
    // Applies movement speed and fire rate multipliers, and enables infinite ammo for the power-up's duration.
    public override void Apply(GameObject player)
    {
        var move = player.GetComponent<PlayerMovement>();
        var weapon = player.GetComponentInChildren<Gun>();

        move?.ApplySpeedBoost(moveSpeedMultiplier, duration);
        weapon?.ApplyFireRateBoost(fireRateMultiplier, duration);
        weapon?.EnableInfiniteAmmo(duration);
    }
}