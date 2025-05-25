using UnityEngine;

public class VirusEnemy : EnemyBase
{
    private VirusEnemySO VirusData => (VirusEnemySO)GetData();

    public Transform sinWaveModel;

    public float amplitude = 0.1f; // height of wave
    public float frequency = 2f; // speed of wave
    public float offsetY = 0.5f;

    private float _startY;

    protected override void Start()
    {
        base.Start();

        if (agent != null)
            agent.speed = VirusData.overrideMoveSpeed;

        _startY = transform.position.y;
    }

    public override void DealDamage(GameObject target)
    {
        if (target.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(VirusData.damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            // Debug.Log("Virus collided with player, exploding!");
            DealDamage(other.gameObject);
            Die();
        }
    }

    public override void Update()
    {
        base.Update();

        if (isDead)
            return;

        Debug.Log("Called");

        var targetTransform = sinWaveModel.transform;
        var newY = _startY + Mathf.Sin(Time.time * frequency) * amplitude + offsetY;
        targetTransform.localPosition = new Vector3(targetTransform.localPosition.x, newY, targetTransform.localPosition.z);
    }
}