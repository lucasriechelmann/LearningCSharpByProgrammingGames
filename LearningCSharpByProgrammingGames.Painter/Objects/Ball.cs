using LearningCSharpByProgrammingGames.Painter.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Metadata;

namespace LearningCSharpByProgrammingGames.Painter.Objects;

public class Ball : ThreeColorGameObject
{
    bool _shooting;
    public Ball(ContentManager content) : base(content, "spr_ball_red", "spr_ball_green", "spr_ball_blue")
    {
       
    }
    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.MouseLeftButtonPressed && !_shooting)
        {
            _shooting = true;
            _velocity = (inputHelper.MousePosition - Painter.GameWorld.Cannon.Position) * 1.2f;
        }
    }
    public override void Update(GameTime gameTime)
    {
        if (_shooting)
        {
            _velocity.Y += 400.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            Color = Painter.GameWorld.Cannon.Color;
            _position = Painter.GameWorld.Cannon.BallPosition;
        }
        if (Painter.GameWorld.IsOutsideWorld(_position))
        {
            Reset();
        }

        base.Update(gameTime);
    }

    public override void Reset()
    {
        base.Reset();
        _velocity = Vector2.Zero;
        _position = new Vector2(65, 390);
        _shooting = false;
    }
}
