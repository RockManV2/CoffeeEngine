
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace CoffeeEngine;

public class SpriteRenderer : Component, IDrawable
{
    public Texture2D Texture;
    public Color Color = Color.White;

    public void Draw(SpriteBatch spriteBatch)
    {
        if(!Enabled) return;
        spriteBatch.Draw(Texture, transform.Position, Color);
    }

    public void LoadContent(string filePath) => Texture = Utils.ContentManager.Load<Texture2D>(filePath);
}