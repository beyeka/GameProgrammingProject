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

    private IEnumerator HealNearbyEnemiesRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(healInterval);

        while (!isDead)
        {
            HealNearbyEnemies();
            yield return wait;
        }
    }

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


    public override void Die()
    {
        base.Die();

        if (_healCoroutine != null)
            StopCoroutine(_healCoroutine);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRadius);
    }
#endif
}