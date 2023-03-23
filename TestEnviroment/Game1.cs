using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CoffeeEngine;
using CoffeeEngine.SceneManagement;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Time.Start();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        #region Utilities
        Utils.DeviceManager = _graphics;
        Utils.SpriteBatch = _spriteBatch;
        Utils.ContentManager = Content;
        Utils.Game = this;
        #endregion

        SceneManager.LoadSceneContent("Scene1");
        
    }

    protected override void Update(GameTime gameTime)
    {
        SceneManager.ActiveScene.UpdateScene();
        SceneManager.ActiveScene.CheckCollisions();

        Time.UpdateTime();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        // Draw the active scene
        SceneManager.ActiveScene.DrawScene(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}