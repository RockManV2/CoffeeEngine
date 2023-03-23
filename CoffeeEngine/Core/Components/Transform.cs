
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
            Vector2 textureSize = new();

            if (GameObject.GetComponent<SpriteRenderer>() != null)
                textureSize = new Vector2(GameObject.GetComponent<SpriteRenderer>().Texture.Width,
                    GameObject.GetComponent<SpriteRenderer>().Texture.Height);
            
            if(GameObject.GetComponent<AnimatedSpriteRenderer>() != null)
                textureSize = GameObject.GetComponent<AnimatedSpriteRenderer>().TextureSize;
            
            _origin = new Vector2(
                x: textureSize.X * value.X,
                y: textureSize.Y * value.Y
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