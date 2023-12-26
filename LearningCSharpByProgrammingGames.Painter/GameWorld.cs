using LearningCSharpByProgrammingGames.Painter.Managers;
using LearningCSharpByProgrammingGames.Painter.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.Painter;
public class GameWorld
{
    Texture2D _background;
    Cannon _cannon;
    Ball _ball;
    PaintCan _can1, _can2, _can3;
    public GameWorld(ContentManager content)
    {
        _background = content.Load<Texture2D>("spr_background");
        _cannon = new Cannon(content);
        _ball = new Ball(content);
        _can1 = new PaintCan(content, 480f, ObjectColor.Red);
        _can2 = new PaintCan(content, 610f, ObjectColor.Green);
        _can3 = new PaintCan(content, 740f, ObjectColor.Blue);
    }
    public void HandleInput(InputHelper inputHelper)
    {
        _cannon.HandleInput(inputHelper);
        _ball.HandleInput(inputHelper, _cannon);
    }
    public void Update(GameTime gameTime)
    {
        _ball.Update(gameTime, _cannon);
        _can1.Update(gameTime, _ball);
        _can2.Update(gameTime, _ball);
        _can3.Update(gameTime, _ball);
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
        spriteBatch.End();
    }
    public Cannon Cannon => _cannon;
    public static bool IsOutsideWorld(Vector2 position)
    {
        return position.X < 0 || position.X > Painter.ScreenSize.X || position.Y > Painter.ScreenSize.Y;
    }
}
