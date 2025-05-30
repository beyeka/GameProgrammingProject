// ScriptableObject for ranged enemies, adds projectile attack data to the base enemy stats.

using UnityEngine;

[CreateAssetMenu(fileName = "NewRangedEnemy", menuName = "Characters/Ranged Enemy Data")]
public class RangedEnemySO : EnemyDataSO
{
    [Header("Ranged Attack Settings")]
    public RangedProjectile projectilePrefab;
    public float projectileSpeed = 10f;
    public float projectileDamage = 15f;
    
    
}