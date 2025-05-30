// Base ScriptableObject for power-ups, containing shared data and an abstract Apply method to define custom behavior.

using UnityEngine;

public abstract class PowerUpBaseSO : ScriptableObject
{
    public string powerUpName;
    public float duration = 5f;

    public abstract void Apply(GameObject player);
}