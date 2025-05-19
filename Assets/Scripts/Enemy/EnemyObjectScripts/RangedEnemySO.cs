using UnityEngine;

[CreateAssetMenu(fileName = "NewRangedEnemy", menuName = "Characters/Ranged Enemy Data")]
public class RangedEnemySO : EnemyDataSO
{
    [Header("Ranged Attack Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float projectileDamage = 15f;
    
    
}