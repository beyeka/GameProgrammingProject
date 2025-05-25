using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField] private Transform firePoint;

    private RangedEnemySO RangedData => (RangedEnemySO)GetData();

    protected override void HandleAttack()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= RangedData.attackRange && Time.time >= lastAttackTime + RangedData.attackCooldown)
        {
            lastAttackTime = Time.time;
            RotateToFacePlayer();
            Attack();
        }
    }

    public override void Attack()
    {
        var rangedProjectile =
            Instantiate(RangedData.projectilePrefab, firePoint.position, firePoint.rotation, transform);
        var projectileRigidbody = rangedProjectile.GetRigidbody();

        if (projectileRigidbody != null && playerTransform != null)
        {
            Vector3 direction = ((playerTransform.position + Vector3.up) - firePoint.position).normalized;
            projectileRigidbody.velocity = direction * RangedData.projectileSpeed;
        }


        RangedProjectile projectile = projectileRigidbody.GetComponent<RangedProjectile>();
        if (projectile != null)
            projectile.SetDamage(RangedData.projectileDamage);
    }


    private void RotateToFacePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}