using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace LearningCSharpByProgrammingGames.Engine;

public class ExtendedGame : Game
{
    //Standard MonoGame objects for graphics and sprites
    protected GraphicsDeviceManager _graphics;
    protected SpriteBatch _spriteBatch;
    //An object for handling keyboard and mouse input
    protected InputHelper _inputHelper;
    //The width and height of the game world, in game units
    protected Point _worldSize;
    //The width and height of the window, in pixels
    protected Point _windowSize;
    //A matrix used for scaling the game world so that it fits inside the window
    protected Matrix _spriteScale;
    public static Random Random { get; private set; }
    public static ContentManager ContentManager { get; private set; }
    protected List<GameObject> _gameWorld;
    protected bool FullScreen
    {
        get => _graphics.IsFullScreen;
        set => ApplyResolutionSettings(value);
    }
    public ExtendedGame()
    {
        Content.RootDirectory = "Content";
        _graphics = new(this);
        Random = new();
        _inputHelper = new();

        _worldSize = new Point(1024, 768);
        _windowSize = new Point(1024, 768);
        _gameWorld = new();
    }
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        ContentManager = Content;
        FullScreen = false;
    }
    protected override void Update(GameTime gameTime)
    {
        HandleInput();

        foreach (GameObject gameObject in _gameWorld)
            gameObject.Update(gameTime);
    }
    protected virtual void HandleInput()
    {
        _inputHelper.Update();

        if (_inputHelper.KeyPressed(Keys.Escape))
            Exit();

        if (_inputHelper.KeyPressed(Keys.F5))
            FullScreen = !FullScreen;

        foreach (GameObject gameObject in _gameWorld)
            gameObject.HandleInput(_inputHelper);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _spriteScale);

        foreach (GameObject gameObject in _gameWorld)
            gameObject.Draw(gameTime, _spriteBatch);

        _spriteBatch.End();
    }
    void ApplyResolutionSettings(bool fullScreen)
    {
        Point screenSize;
        if (fullScreen)
            screenSize = new Point(
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        else
            screenSize = _windowSize;

        _graphics.IsFullScreen = fullScreen;
        _graphics.PreferredBackBufferWidth = screenSize.X;
        _graphics.PreferredBackBufferHeight = screenSize.Y;
        _graphics.ApplyChanges();
        GraphicsDevice.Viewport = CalculateViewport(screenSize);

        _spriteScale = Matrix.CreateScale(
            (float)GraphicsDevice.Viewport.Width / _worldSize.X,
            (float)GraphicsDevice.Viewport.Height / _worldSize.Y,
            1f);
    }
    Viewport CalculateViewport(Point windowSize)
    {
        Viewport viewport = new();

        float gameAspectRatio = _worldSize.X / (float)_worldSize.Y;
        float windowAspectRatio = windowSize.X / (float)windowSize.Y;

        //if the window is relatively wide, use the full window height
        if (windowAspectRatio > gameAspectRatio)
        {
            viewport.Width = (int)(windowSize.Y * gameAspectRatio);
            viewport.Height = windowSize.Y;
        }
        //if the window is relatively high, use the full window width
        else
        {
            viewport.Width = windowSize.X;
            viewport.Height = (int)(windowSize.X / gameAspectRatio);
        }

        viewport.X = (windowSize.X - viewport.Width) / 2;
        viewport.Y = (windowSize.Y - viewport.Height) / 2;

        return viewport;
    }
    Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        Vector2 viewportTopLeft = new(
            GraphicsDevice.Viewport.X,
            GraphicsDevice.Viewport.Y);
        float screenToWorldScale = _worldSize.X / (float)GraphicsDevice.Viewport.Width;
        return (screenPosition - viewportTopLeft) * screenToWorldScale;
    }
}
