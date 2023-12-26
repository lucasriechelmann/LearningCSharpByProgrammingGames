using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LearningCSharpByProgrammingGames.Painter.Objects;

public class PaintCan : ThreeColorGameObject
{
    Color _targetColor;
    float minSpeed;
    public PaintCan(ContentManager content, float positionOffset, Color target) :
        base(content, "spr_can_red", "spr_can_green", "spr_can_blue")
    {
        _origin = new Vector2(_colorRed.Width, _colorRed.Height) / 2;
        _targetColor = target;
        minSpeed = 30;        
    }
    public override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        minSpeed += 0.01f * dt;

        base.Update(gameTime);

        if (_velocity != Vector2.Zero)
        {
            // check if this paint can collides with the ball
            if (BoundingBox.Intersects(Painter.GameWorld.Ball.BoundingBox))
            {
                Color = Painter.GameWorld.Ball.Color;
                Painter.GameWorld.Ball.Reset();
            }

            // reset the can if it leaves the screen
            if (Painter.GameWorld.IsOutsideWorld(_position - _origin))
            {
                // if the color is wrong, the player loses a life
                if (Color != _targetColor)
                    Painter.GameWorld.LoseLife();
                Reset();
            }
        }

        // if the can is not moving, (re)spawn it with a certain probability
        else if (Painter.Random.NextDouble() < 0.01)
        {
            _velocity = CalculateRandomVelocity();
            Color = CalculateRandomColor();
        }
    }

    public override void Reset()
    {
        base.Reset();
        _position.Y = -_origin.Y;
        _velocity = Vector2.Zero;
    }
    public void ResetMinSpeed()
    {
        minSpeed = 30;
    }
    Vector2 CalculateRandomVelocity() =>
        new Vector2(0.0f, (float)Painter.Random.NextDouble() * 30 + minSpeed);

    public Color CalculateRandomColor()
    {
        int randomVal = Painter.Random.Next(3);
        if (randomVal == 0)
            return Color.Red;
        else if (randomVal == 1)
            return Color.Green;
        else
            return Color.Blue;
    }    
}
