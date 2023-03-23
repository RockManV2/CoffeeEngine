
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace CoffeeEngine;

public class SpriteRenderer : Component, IDrawable
{
    public Texture2D Texture;
    public Color Color = Color.White;
    public float Layer = 1;

    public void Draw(SpriteBatch spriteBatch)
    {
        if(!Enabled) return;
        spriteBatch.Draw(
            texture: Texture,
            position: transform.Position,
            sourceRectangle: null,
            color: Color,
            rotation: transform.RotationInRadians,
            origin: transform.Origin,
            scale: transform.Scale,
            effects: SpriteEffects.None,
            layerDepth: Layer
        );
    }

    public void LoadContent(string filePath) => Texture = Utils.ContentManager.Load<Texture2D>(filePath);
}