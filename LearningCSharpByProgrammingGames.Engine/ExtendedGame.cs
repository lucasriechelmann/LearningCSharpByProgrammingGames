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
    /// <summary>
    /// The width and height of the game world, in game units.
    /// </summary>
    protected Point _worldSize;
    /// <summary>
    /// The width and height of the window, in pixels.
    /// </summary>
    protected Point _windowSize;
    /// <summary>
    /// A matrix used for scaling the game world so that it fits inside the window.
    /// </summary>
    protected Matrix _spriteScale;
    /// <summary>
    /// An object for generating random numbers throughout the game.
    /// </summary>
    public static Random Random { get; private set; }
    /// <summary>
    /// An object for loading assets throughout the game.
    /// </summary>
    public static AssetManager AssetManager { get; private set; }
    /// <summary>
    /// The game world, represented by a list of game objects.
    /// </summary>
    protected static GameObjectList _gameWorld;
    public ExtendedGame()
    {
        Content.RootDirectory = "Content";
        _graphics = new(this);

        _inputHelper = new();
        Random = new();

        // default window and world size
        _worldSize = new Point(1024, 768);
        _windowSize = new Point(1024, 768);
        
        
    }
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // store a static reference to the ContentManager
        AssetManager = new(Content);        
        
        // create an empty game world
        _gameWorld = new();

        // by default, we're not running in full-screen mode
        FullScreen = false;
    }
    protected override void Update(GameTime gameTime)
    {
        HandleInput();

        // let all game objects update themselves
        _gameWorld.Update(gameTime);        
    }
    protected virtual void HandleInput()
    {
        _inputHelper.Update();

        // quit the game when the player presses ESC
        if (_inputHelper.KeyPressed(Keys.Escape))
            Exit();

        // toggle full-screen mode when the player presses F5
        if (_inputHelper.KeyPressed(Keys.F5))
            FullScreen = !FullScreen;

        // let all game objects do their own input handling
        _gameWorld.HandleInput(_inputHelper);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // start drawing sprites, applying the scaling matrix
        _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _spriteScale);

        // let all game objects draw themselves
        _gameWorld.Draw(gameTime, _spriteBatch);

        _spriteBatch.End();
    }
    /// <summary>
    /// Scales the window to the desired size, and calculates how the game world should be scaled to fit inside that window.
    /// </summary>
    void ApplyResolutionSettings(bool fullScreen)
    {
        // make the game full-screen or not
        _graphics.IsFullScreen = fullScreen;

        // get the size of the screen to use: either the window size or the full screen size
        Point screenSize;
        if (fullScreen)
            screenSize = new Point(
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        else
            screenSize = _windowSize;


        // scale the window to the desired size
        _graphics.PreferredBackBufferWidth = screenSize.X;
        _graphics.PreferredBackBufferHeight = screenSize.Y;

        _graphics.ApplyChanges();

        // calculate and set the viewport to use
        GraphicsDevice.Viewport = CalculateViewport(screenSize);

        // calculate how the graphics should be scaled, so that the game world fits inside the window
        _spriteScale = Matrix.CreateScale(
            (float)GraphicsDevice.Viewport.Width / _worldSize.X,
            (float)GraphicsDevice.Viewport.Height / _worldSize.Y,
            1f);
    }
    /// <summary>
    /// Calculates and returns the viewport to use, so that the game world fits on the screen while preserving its aspect ratio.
    /// </summary>
    /// <param name="windowSize">The size of the screen on which the world should be drawn.</param>
    /// <returns>A Viewport object that will show the game world as large as possible while preserving its aspect ratio.</returns>
    Viewport CalculateViewport(Point windowSize)
    {
        // create a Viewport object
        Viewport viewport = new();

        // calculate the two aspect ratios
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
    /// <summary>
    /// Gets or sets whether the game is running in full-screen mode.
    /// </summary>
    protected bool FullScreen
    {
        get => _graphics.IsFullScreen;
        set => ApplyResolutionSettings(value);
    }
    /// <summary>
    /// Converts a position in screen coordinates to a position in world coordinates.
    /// </summary>
    /// <param name="screenPosition">A position in screen coordinates.</param>
    /// <returns>The corresponding position in world coordinates.</returns>
    public Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        Vector2 viewportTopLeft = new(
            GraphicsDevice.Viewport.X,
            GraphicsDevice.Viewport.Y);
        float screenToWorldScale = _worldSize.X / (float)GraphicsDevice.Viewport.Width;
        return (screenPosition - viewportTopLeft) * screenToWorldScale;
    }
}
