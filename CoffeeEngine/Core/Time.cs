
using Microsoft.Xna.Framework;
namespace CoffeeEngine;

public static class Time
{
    private static GameTime _gameTime = new();
    
    public static float frameCount;
    public static float deltaTime => (float)_gameTime.ElapsedGameTime.TotalSeconds;
    public static float time => (float)_gameTime.TotalGameTime.TotalSeconds;
    
    public static void UpdateTime(GameTime gameTime)
    {
        frameCount += 1;
        _gameTime = gameTime;
    }
}