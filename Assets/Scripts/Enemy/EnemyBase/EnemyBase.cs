// Abstract base class for enemy behavior. Handles movement, attacking, health, pooling, and interaction with player systems.
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour, IPoolable
{
    [Header("Config")] [SerializeField] protected EnemyDataSO enemyData;
    [SerializeField] private GameObject pooledPrefabReference;

    protected float currentHealth;
    protected NavMeshAgent agent;
    protected Transform playerTransform;
    protected bool isDead = false;
    protected float lastAttackTime = 0f;
    protected float attackRange;
    protected float attackCooldown;
    public bool IsDead => isDead;
    

    // Initializes health, NavMeshAgent, and locates the player.
    protected virtual void Start()
    {
        currentHealth = enemyData.maxHealth;
        agent = GetComponent<NavMeshAgent>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerTransform = player.transform;
    }

    // Skips logic if dead or player not found. Handles movement and attack logic.
    public virtual void Update()
    {
        if (isDead || playerTransform == null) return;

        Move();
        HandleAttack();
    }

    // Uses NavMeshAgent to follow the player if valid.
    protected virtual void Move()
    {
        if (agent.isOnNavMesh && !agent.isStopped)
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    // Checks distance and cooldown to trigger Attack.
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

    // To be overridden by child classes to implement custom attack logic.
    public virtual void Attack()
    {
    }

    // To be overridden for applying damage logic to a target.
    public virtual void DealDamage(GameObject target)
    {
    }

    // Reduces health, triggers Die() if health <= 0.
    public virtual void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Gives experience to player through LevelSystem.
    public virtual void GiveExp(float xpValue)
    {
        if (playerTransform == null) return;

        LevelSystem levelSystem = playerTransform.GetComponent<LevelSystem>();
        if (levelSystem != null)
        {
            levelSystem.GainExperienceFlatRate(xpValue);
        }
    }

    // Resets state on enemy spawn.
    public virtual void OnSpawn()
    {
        isDead = false;
        gameObject.SetActive(true);
        
    }

    // Optional logic for when despawned.
    public virtual void OnDespawn()
    {
        
    }
    
    // Increases health up to the max limit.
    public virtual void Heal(float amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, enemyData.maxHealth);
    }

    // Plays SFX, handles cleanup, notifies GameManager, and returns to pool.
    public virtual void Die()
    {
        SoundManager.Instance.PlaySound(SFXKeys.EnemyDeath);
        
        isDead = true;
        agent.isStopped = true;

        GameManager.Instance.gameplayController.PlayEnemyDeadPS(transform.position);
        
        GameManager.Instance.gameplayController.GiveExp(enemyData.experienceReward);

        EnemyPoolManager.Instance.Despawn(gameObject, pooledPrefabReference);
    }

    // Returns the associated ScriptableObject data.
    public EnemyDataSO GetData() => enemyData;
    
}