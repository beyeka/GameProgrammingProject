using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] protected float duration = 5f;

    protected abstract void Apply(GameObject player);
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Apply(other.gameObject);
        Destroy(gameObject); 
    }
    
    
}