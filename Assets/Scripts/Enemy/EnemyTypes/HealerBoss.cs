// Specialized enemy class for the Healer Boss. Heals nearby enemies periodically using data from HealerBossSO.
using System.Collections;
using UnityEngine;

public class HealerBoss : EnemyBase
{
    private HealerBossSO HealerBossData => (HealerBossSO)GetData();
    [SerializeField] private float healRadius = 10f;
    [SerializeField] private float healAmount = 15f;
    [SerializeField] private float healInterval = 3f;
    [SerializeField] private LayerMask enemyLayer;  

    private Coroutine _healCoroutine;

    // Initializes healing values from SO and starts healing coroutine.
    protected override void Start()
    {
        base.Start();
        if (agent != null)
        {
            healAmount = HealerBossData.healAmount;
            healRadius = HealerBossData.healRadius;
            healInterval = HealerBossData.healInterval;
        }
        _healCoroutine = StartCoroutine(HealNearbyEnemiesRoutine());
    }

    // Continuously heals nearby enemies at fixed intervals while boss is alive.
    private IEnumerator HealNearbyEnemiesRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(healInterval);

        while (!isDead)
        {
            HealNearbyEnemies();
            yield return wait;
        }
    }

    // Heals all nearby alive enemies within a certain radius using Physics.OverlapSphere.
    private void HealNearbyEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, healRadius, enemyLayer);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == this.gameObject) continue;

            EnemyBase enemy = hitCollider.GetComponent<EnemyBase>();
            if (enemy != null && !enemy.IsDead)
            {
                enemy.Heal(healAmount);
            }
        }
    }


    // Stops healing coroutine on death, then runs base death logic.
    public override void Die()
    {
        base.Die();

        if (_healCoroutine != null)
            StopCoroutine(_healCoroutine);
    }

    // (Editor only) Draws a wire sphere to visualize heal radius in the Scene view.
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRadius);
    }
#endif
}