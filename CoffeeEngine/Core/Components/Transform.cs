
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeEngine;

public class Transform : Component
{
    public Vector2 Position;
    public Vector2 Scale = new Vector2(1,1);

    private Vector2 _origin = new Vector2(0,0);
    public Vector2 Origin
    {
        get => _origin;
        set
        {
            Texture2D tex = GameObject.GetComponent<SpriteRenderer>().Texture;
            _origin = new Vector2(
                x: tex.Width * value.X,
                y: tex.Height * value.Y
            );
        }
    }

    private float _rotationInRadians = 0;
    public float Rotation
    {
        get => MathHelper.ToDegrees(_rotationInRadians);
        set => _rotationInRadians = MathHelper.ToRadians(value);
    }

    public float RotationInRadians => _rotationInRadians;
}