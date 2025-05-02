using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour 
{
    [Header("Stats")]
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected float attackCooldown = 1f;

    protected float currentHealth;
    protected NavMeshAgent agent;
    protected Transform playerTransform;

    protected bool isDead = false;
    private float lastAttackTime = 0f;
    

    protected virtual void Start()
    {
        currentHealth = maxHealth;
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

    protected virtual void HandleAttack(){
    
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= attackRange)
        {
            
            Attack();
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
    

    protected virtual void Die()
    {
        isDead = true;
        agent.isStopped = true;
       
        Destroy(gameObject, 1f); // delay if you want death anim
    }

   
   }
