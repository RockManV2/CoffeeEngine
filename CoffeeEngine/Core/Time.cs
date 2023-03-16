
using Microsoft.Xna.Framework;
namespace CoffeeEngine;

public static class Time
{
    private static GameTime _gameTime;
    
    public static float frameCount;
    public static float deltaTime => (float)_gameTime.ElapsedGameTime.TotalSeconds;
    public static float time => (float)_gameTime.TotalGameTime.TotalSeconds;

    public static void Start() => _gameTime = new();

    public static void UpdateTime()
    {
        frameCount += 1;
    }
}