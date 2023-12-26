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
    public GameWorld(ContentManager content)
    {
        _background = content.Load<Texture2D>("spr_background");
        _cannon = new Cannon(content);
        _ball = new Ball(content);
    }
    public void HandleInput(InputHelper inputHelper)
    {
        _cannon.HandleInput(inputHelper);
        _ball.HandleInput(inputHelper);
    }
    public void Update(GameTime gameTime)
    {
        _ball.Update(gameTime, _cannon);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _ball.Draw(gameTime, spriteBatch);
        _cannon.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }
    public Cannon Cannon => _cannon;
}
