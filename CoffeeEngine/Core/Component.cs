
using CoffeeEngine.Physics;
namespace CoffeeEngine;

public abstract class Component
{
    public GameObject GameObject;
    public Transform transform;
    public virtual void Awake() { }
    public virtual void Start() { }
    public virtual void OnCollisionStay(CollisionInfo collisionInfo) { }
}