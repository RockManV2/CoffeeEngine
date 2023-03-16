
using CoffeeEngine.Physics;
namespace CoffeeEngine;

public abstract class MonoBehaviour : Component
{
    public virtual void OnCollisionStay(CollisionInfo collisionInfo) { }

    public virtual void OnEnable() { }
    public virtual void OnDisable() { }
}