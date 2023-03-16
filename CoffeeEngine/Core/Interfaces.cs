
using Microsoft.Xna.Framework.Graphics;
namespace CoffeeEngine;

public interface IUpdateable
    {
        public void Update();
    }

    public interface IDrawable
    {
        public void Draw(SpriteBatch spriteBatch);
    }