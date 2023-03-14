using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace CoffeeEngine;

public static class Utils
{
    public static GraphicsDeviceManager DeviceManager;
    public static SpriteBatch SpriteBatch;
    public static ContentManager ContentManager;
    public static Game Game;
    public static void Print(object x) => Console.WriteLine(x);
}