
using Microsoft.Xna.Framework;
namespace CoffeeEngine.Physics;

public class BoxCollider : Component, IUpdateable
{
    private Rectangle _hitBox;

    public override void Start()
    {
        _hitBox.X = (int)GameObject.transform.Position.X;
        _hitBox.Y = (int)GameObject.transform.Position.Y;
        _hitBox = GameObject.GetComponent<SpriteRenderer>().Texture.Bounds;
        base.Start();
    }

    public void Update()
    {
        _hitBox.X = (int)GameObject.transform.Position.X;
        _hitBox.Y = (int)GameObject.transform.Position.Y;
    }

    public bool CheckCollision(BoxCollider other, out CollisionInfo collisionInfo)
    {
        bool collisionResult = _hitBox.Intersects(other._hitBox);
        collisionInfo.Other = collisionResult ? this : null;
        
        return collisionResult;
    }
}