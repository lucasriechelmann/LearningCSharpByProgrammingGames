using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.JewelJam.Objects;

public class JewelCart : SpriteGameObject
{
    // The horizontal speed at which the cart moves.
    const float speed = 10;

    // The distance by which the cart will be pushed back if the player scores points.
    const float pushDistance = 100;

    // The x coordinate at which the jewel cart starts.
    float startX;
    GlitterField _glitters;
    public JewelCart(Vector2 startPosition)
        : base("spr_jewelcart")
    {
        LocalPosition = startPosition;
        startX = startPosition.X;

        _glitters = new GlitterField(_sprite, 40, new Rectangle(275, 470, 430, 85));
        _glitters.Parent = this;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _glitters.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        _glitters.Draw(gameTime, spriteBatch);
    }
    /// <summary>
    /// Pushes the cart back by some distance, to give the player extra time.
    /// Call this method when the player scores points.
    /// </summary>
    public void PushBack()
    {
        LocalPosition = new Vector2(
            MathHelper.Max(LocalPosition.X - pushDistance, startX),
            LocalPosition.Y);
    }

    public override void Reset()
    {
        velocity.X = speed;
        LocalPosition = new Vector2(startX, LocalPosition.Y);
    }
}
