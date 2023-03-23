using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoffeeEngine;

public class AnimatedSpriteRenderer : Component, IDrawable, IUpdateable
{
    public Texture2D Texture;
    public Vector2 TextureSize = new Vector2(140, 140);

    private Rectangle[] _sourceRectangles =
    {
        new Rectangle(0,0,140,140),
        new Rectangle(140,0,140,140),
        new Rectangle(280,0,140,140),
        new Rectangle(420,0,140,140),
        new Rectangle(560,0,140,140),
        
        new Rectangle(0,140,140,140),
        new Rectangle(140,140,140,140),
        new Rectangle(280,140,140,140),
        new Rectangle(420,140,140,140),
        new Rectangle(560,140,140,140),

    };

    private float _timeBetween = 0.25f;
    
    private int _currentSourceRectangle = 1;
    
    public Color Color = Color.White;
    public float Layer = 1;

    public override void Start() => _currentSourceRectangle = 0;

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            transform.Position,
            _sourceRectangles[_currentSourceRectangle],
            Color,
            transform.Rotation,
            transform.Origin,
            transform.Scale,
            SpriteEffects.None,
            Layer
        );
    }

    public void Update()
    {
        if (!(Time.time >= _timeBetween)) return;
        _timeBetween += 0.1f;
        _currentSourceRectangle++;
            
        if (_currentSourceRectangle <= _sourceRectangles.Length - 1) return;
        _currentSourceRectangle = 0;
    }
    
    public void LoadContent(string filePath) => Texture = Utils.ContentManager.Load<Texture2D>(filePath);
}