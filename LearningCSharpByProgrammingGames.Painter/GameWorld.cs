using LearningCSharpByProgrammingGames.Painter.Managers;
using LearningCSharpByProgrammingGames.Painter.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace LearningCSharpByProgrammingGames.Painter;
/// <summary>
/// Represents the entire game world. 
/// The overall game should have a single instance of this class.
/// </summary>
public class GameWorld
{
    Texture2D _background, _gameOver, _livesSprite, _scoreBar;
    SpriteFont _gameFont;
    // game objects: ball, paint cans, and cannon.
    Ball _ball;
    List<PaintCan> _paintCans;
    Cannon _cannon;
    /// <summary>
    /// The current number of lives that the player has.
    /// </summary>
    int _lives;
    /// <summary>
    /// The current score of the player.
    /// </summary>
    public int Score { get; set; }
    /// <summary>
    /// Creates a new GameWorld instance. 
    /// This method loads all relevant MonoGame assets and initializes all game objects: 
    /// the cannon, the ball, and the paint cans. 
    /// It also initializes all other variables so that the game can start.
    /// </summary>
    /// <param name="Content">A ContentManager object, required for loading assets.</param>
    public GameWorld(ContentManager content)
    {
        // load sprites and fonts
        _gameOver = content.Load<Texture2D>("spr_gameover");
        _livesSprite = content.Load<Texture2D>("spr_lives");
        _background = content.Load<Texture2D>("spr_background");
        _scoreBar = content.Load<Texture2D>("spr_scorebar");
        _gameFont = content.Load<SpriteFont>("PainterFont");

        // initialize game objects: cannon, ball, and paint cans
        _cannon = new Cannon(content);
        _ball = new Ball(content);
        _paintCans = new List<PaintCan>();
        _paintCans.Add(new PaintCan(content, 480f, Color.Red));
        _paintCans.Add(new PaintCan(content, 610f, Color.Green));
        _paintCans.Add(new PaintCan(content, 740f, Color.Blue));

        // initialize other variables
        Score = 0;
        _lives = 5;
    }
    /// <summary>
    /// Performs input handling for the entire game world.
    /// </summary>
    /// <param name="inputHelper">An object that contains information about the mouse and keyboard state.</param>
    public void HandleInput(InputHelper inputHelper)
    {
        // the cannon and the ball both do input handling
        if (!IsGameOver)
        {
            _cannon.HandleInput(inputHelper);
            _ball.HandleInput(inputHelper);
        }

        // in the "game over" state, pressing the spacebar will reset the game
        else if (inputHelper.KeyPressed(Keys.Space))
        {
            Reset();
        }
    }
    /// <summary>
    /// Updates all game objects for one frame of the game loop.
    /// </summary>
    /// <param name="gameTime">An object that contains information about the game time that has passed.</param>
    public void Update(GameTime gameTime)
    {
        // in the "game over" state, don't update any objects
        if (IsGameOver)
            return;

        _ball.Update(gameTime);
        foreach (PaintCan can in _paintCans)
            can.Update(gameTime);
    }
    /// <summary>
    /// Draws the game world in its current state.
    /// </summary>
    /// <param name="gameTime">An object that contains information about the game time that has passed.</param>
    /// <param name="spriteBatch">The sprite batch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        // draw the background and score bar
        spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        spriteBatch.Draw(_scoreBar, new Vector2(10, 10), Color.White);

        // draw all game objects
        _ball.Draw(gameTime, spriteBatch);
        _cannon.Draw(gameTime, spriteBatch);
        
        foreach (PaintCan can in _paintCans)
            can.Draw(gameTime, spriteBatch);

        // draw the score
        spriteBatch.DrawString(_gameFont, "Score: " + Score, new Vector2(20, 18), Color.White);

        for (int i = 0; i < _lives; i++)
        {
            spriteBatch.Draw(_livesSprite, new Vector2(i * _livesSprite.Width + 15, 60), Color.White);
        }

        // if the game is over, draw the game-over sprite
        if (IsGameOver)
        {
            spriteBatch.Draw(_gameOver, 
                new Vector2(Painter.ScreenSize.X - _gameOver.Width, 
                    Painter.ScreenSize.Y - _gameOver.Height) / 2, 
                Color.White);
        }

        spriteBatch.End();
    }
    /// <summary>
    /// Resets the game world to its initial state, so that a new game is ready to begin.
    /// </summary>
    void Reset()
    {
        _lives = 5;
        Score = 0;

        // reset all game objects
        _ball.Reset();
        foreach (PaintCan can in _paintCans)
        {
            can.Reset();
            can.ResetMinSpeed();
        }           
    }
    /// <summary>
    /// Gets the game world's Cannon object.
    /// </summary>
    public Cannon Cannon => _cannon;
    /// <summary>
    /// Gets the game world's Ball object.
    /// </summary>
    public Ball Ball => _ball;
    /// <summary>
    /// Decreases the player's number of lives by one.
    /// </summary>
    public void LoseLife() => _lives--;
    /// <summary>
    /// Checks and returns whether or not the game is over. 
    /// Returns true if the player has no more lives left, and false otherwise.
    /// </summary>
    bool IsGameOver => _lives <= 0;
    /// <summary>
    /// Checks and returns whether a given position lies outside the screen.
    /// </summary>
    /// <param name="position">A position in the game world.</param>
    /// <returns>true if the given position lies outside the screen, and false otherwise.</returns>
    public bool IsOutsideWorld(Vector2 position) =>
        position.X < 0 || position.X > Painter.ScreenSize.X || position.Y > Painter.ScreenSize.Y;
}
