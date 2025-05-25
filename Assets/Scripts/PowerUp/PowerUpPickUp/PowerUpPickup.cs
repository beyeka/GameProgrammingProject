using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [SerializeField] private PowerUpBaseSO powerUpData;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SoundManager.Instance.PlaySound(SFXKeys.PowerUp_PickUp);
        
        powerUpData.Apply(other.gameObject);
        Destroy(gameObject); 
    }
    public void SetPowerUp(PowerUpBaseSO newPowerUp)
    {
        powerUpData = newPowerUp;
    }

}