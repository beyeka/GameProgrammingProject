using UnityEngine;

// This attribute lets you create a new EnemyDataSO asset via the Unity Editor's asset creation menu
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Characters/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    // Header for general enemy stats
    [Header("General Stats")]

    // Name of the enemy (for identification/debugging purposes)
    public string enemyName;

    // The maximum health this enemy can have
    public float maxHealth = 100f;

    // Damage dealt to the player or other entities
    public float damage = 10f;

    // Movement speed of the enemy
    public float moveSpeed = 3.5f;

    // Header for combat-related stats
    [Header("Combat")]

    // How close the enemy needs to be to attack
    public float attackRange = 1.5f;

    // Cooldown time between attacks
    public float attackCooldown = 1f;

    // XP given to the player upon defeating this enemy
    public float experienceReward = 15f;
}