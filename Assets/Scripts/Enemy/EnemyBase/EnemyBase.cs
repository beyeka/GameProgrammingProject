using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] protected EnemyDataSO enemyData;

    protected float currentHealth;
    protected NavMeshAgent agent;
    protected Transform playerTransform;
    protected bool isDead = false;
    protected float lastAttackTime = 0f;
    protected float attackRange;
    protected float attackCooldown;
    
    protected virtual void Start()
    {
        currentHealth = enemyData.maxHealth;
        agent = GetComponent<NavMeshAgent>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerTransform = player.transform;
    }

    protected virtual void Update()
    {
        if (isDead || playerTransform == null) return;

        Move();
        HandleAttack();
    }

    protected virtual void Move()
    {
        if (agent.isOnNavMesh && !agent.isStopped)
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    protected virtual void HandleAttack()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= enemyData.attackRange)
        {
            if (Time.time >= lastAttackTime + enemyData.attackCooldown)
            {
                lastAttackTime = Time.time;
                Attack();
            }
        }
    }

    public virtual void Attack()
    {
        
    }

    public virtual void DealDamage(GameObject target)
    {
        
    }

    public virtual void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        isDead = true;
        agent.isStopped = true;

        GiveExp(enemyData.experienceReward);

        
        Destroy(gameObject);
    }

    public virtual void GiveExp(float xpValue)
    {
        if (playerTransform == null) return;

        LevelSystem levelSystem = playerTransform.GetComponent<LevelSystem>();
        if (levelSystem != null)
        {
            levelSystem.GainExperienceFlatRate(xpValue);
        }
    }

    public EnemyDataSO GetData() => enemyData; 
}
