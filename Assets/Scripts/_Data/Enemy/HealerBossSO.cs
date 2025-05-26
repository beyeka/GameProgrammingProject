

using UnityEngine;
[CreateAssetMenu(fileName = "HealerBossData", menuName = "Characters/Healer Boss Data")]
public class HealerBossSO : EnemyDataSO
{
    [Header("Healer Boss Specific")]
    public float healRadius = 10f;
    public float healAmount = 15f;
    public float healInterval = 3f;
    public LayerMask enemyLayer;  
    
}
