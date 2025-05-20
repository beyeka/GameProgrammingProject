using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [SerializeField] private PowerUpBaseSO powerUpData;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        powerUpData.Apply(other.gameObject);
        Destroy(gameObject); 
    }
    public void SetPowerUp(PowerUpBaseSO newPowerUp)
    {
        powerUpData = newPowerUp;
    }

}