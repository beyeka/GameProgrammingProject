using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    [SerializeField] private float moveSpeedMultiplier = 1.5f;
    [SerializeField] private float fireRateMultiplier = 1.5f;

    protected override void Apply(GameObject player)
    {
        var movement = player.GetComponent<PlayerMovement>();
        var weapon = player.GetComponent<Gun>();

        if (movement != null)
            movement.ApplySpeedBoost(moveSpeedMultiplier, duration);

        if (weapon != null)
        {
            weapon.ApplyFireRateBoost(fireRateMultiplier, duration);
            weapon.EnableInfiniteAmmo(duration);
        }
    }
    
}