using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Characters/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    [Header("General Stats")]
    public string enemyName;
    public float maxHealth = 100f;
    public float damage = 10f;
    public float moveSpeed = 3.5f;

    [Header("Combat")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    public float experienceReward = 15f;

    
}