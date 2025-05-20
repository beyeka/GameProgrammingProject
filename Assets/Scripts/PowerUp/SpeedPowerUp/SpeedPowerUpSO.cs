using UnityEngine;
[CreateAssetMenu(fileName = "SpeedAndAmmoPowerUp", menuName = "PowerUps/SpeedAndAmmo")]
public class SpeedPowerUpSO : PowerUpBaseSO
{
    public float moveSpeedMultiplier = 1.5f;
    public float fireRateMultiplier = 1.5f;

    public override void Apply(GameObject player)
    {
        var move = player.GetComponent<PlayerMovement>();
        var weapon = player.GetComponent<Gun>();

        move?.ApplySpeedBoost(moveSpeedMultiplier, duration);
        weapon?.ApplyFireRateBoost(fireRateMultiplier, duration);
        weapon?.EnableInfiniteAmmo(duration);
    }
}