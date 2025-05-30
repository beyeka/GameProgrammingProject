// ScriptableObject for virus-type enemies, allows overriding default movement speed from base enemy stats.

using UnityEngine;

[CreateAssetMenu(fileName = "VirusEnemyData", menuName = "Characters/Virus Enemy Data")]
public class VirusEnemySO : EnemyDataSO
{
    [Header("Virus Specific")]
    public float overrideMoveSpeed ;
    
    
}