// Interface for objects that support pooling. Used by the pooling system to handle lifecycle events.

public interface IPoolable
{
    // Called when the object is taken from the pool.
    void OnSpawn();
    
    // Called when the object is returned to the pool.
    void OnDespawn();
}