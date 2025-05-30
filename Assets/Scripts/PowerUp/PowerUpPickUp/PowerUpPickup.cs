// Handles player collision with a power-up object. Applies the effect and destroys itself.

using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [SerializeField] private PowerUpBaseSO powerUpData;

    // When the player touches the power-up, plays sound, applies the effect, and removes the object.
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SoundManager.Instance.PlaySound(SFXKeys.PowerUp_PickUp);
        
        powerUpData.Apply(other.gameObject);
        Destroy(gameObject); 
    }
    
    // Sets or overrides the power-up data for this pickup instance.
    public void SetPowerUp(PowerUpBaseSO newPowerUp)
    {
        powerUpData = newPowerUp;
    }

}