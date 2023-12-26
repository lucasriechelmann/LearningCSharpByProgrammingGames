using LearningCSharpByProgrammingGames.Painter.Managers;
using LearningCSharpByProgrammingGames.Painter.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.Painter;
public class GameWorld
{
    Texture2D _background, _gameOver, _livesSprite;
    Cannon _cannon;
    Ball _ball;
    PaintCan _can1, _can2, _can3;
    int _lives;
    public GameWorld(ContentManager content)
    {
        _background = content.Load<Texture2D>("spr_background");
        _gameOver = content.Load<Texture2D>("spr_gameover");
        _livesSprite = content.Load<Texture2D>("spr_lives");
        _cannon = new Cannon(content);
        _ball = new Ball(content);
        _can1 = new PaintCan(content, 480f, Color.Red);
        _can2 = new PaintCan(content, 610f, Color.Green);
        _can3 = new PaintCan(content, 740f, Color.Blue);
        _lives = 5;
    }
    public void HandleInput(InputHelper inputHelper)
    {
        if (IsGameOver)
        {
            Reset();
            return;
        }

        _cannon.HandleInput(inputHelper);
        _ball.HandleInput(inputHelper);
    }
    public void Update(GameTime gameTime)
    {
        if(IsGameOver)
            return;

        _ball.Update(gameTime);
        _can1.Update(gameTime);
        _can2.Update(gameTime);
        _can3.Update(gameTime);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _ball.Draw(gameTime, spriteBatch);
        _cannon.Draw(gameTime, spriteBatch);
        _can1.Draw(gameTime, spriteBatch);
        _can2.Draw(gameTime, spriteBatch);
        _can3.Draw(gameTime, spriteBatch);

        for(int i = 0; i < _lives; i++)
        {
            spriteBatch.Draw(_livesSprite, new Vector2(i * _livesSprite.Width + 15, 20), Color.White);
        }

        if(IsGameOver)
        {
            spriteBatch.Draw(_gameOver, 
                new Vector2(Painter.ScreenSize.X - _gameOver.Width, 
                    Painter.ScreenSize.Y - _gameOver.Height) / 2, 
                Color.White);
        }

        spriteBatch.End();
    }
    void Reset()
    {
        _lives = 5;
        _ball.Reset();
        _can1.Reset();
        _can2.Reset();
        _can3.Reset();
        _can1.ResetMinSpeed();
        _can2.ResetMinSpeed();
        _can3.ResetMinSpeed();
    }
    public Cannon Cannon => _cannon;
    public Ball Ball => _ball;
    public void LoseLife()
    {
        _lives--;
    }

    bool IsGameOver
    {
        get { return _lives <= 0; }
    }
    public bool IsOutsideWorld(Vector2 position)
    {
        return position.X < 0 || position.X > Painter.ScreenSize.X || position.Y > Painter.ScreenSize.Y;
    }
}
