using LearningCSharpByProgrammingGames.Painter.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Metadata;

namespace LearningCSharpByProgrammingGames.Painter.Objects;
public class Cannon : ThreeColorGameObject
{    
    Texture2D _cannonBarrel;
    Vector2 _barrelOrigin;
    float _barrelRotation;
    
    public Cannon(ContentManager content) : base(content, "spr_cannon_red", "spr_cannon_green", "spr_cannon_blue")
    {         
        _cannonBarrel = content.Load<Texture2D>("spr_cannon_barrel");
        _position = new Vector2(72, 405);
        _barrelOrigin = new Vector2(_cannonBarrel.Height / 2, _cannonBarrel.Height / 2);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        // change the color when the player presses R/G/B
        if (inputHelper.KeyPressed(Keys.R))
        {
            Color = Color.Red;
        }
        else if (inputHelper.KeyPressed(Keys.G))
        {
            Color = Color.Green;
        }
        else if (inputHelper.KeyPressed(Keys.B))
        {
            Color = Color.Blue;
        }

        // change the angle depending on the mouse position
        double opposite = inputHelper.MousePosition.Y - _position.Y;
        double adjacent = inputHelper.MousePosition.X - _position.X;
        _barrelRotation = (float)Math.Atan2(opposite, adjacent);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_cannonBarrel, _position, null, Color.White, _barrelRotation, _barrelOrigin, 1.0f, SpriteEffects.None, 0);
        base.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        base.Reset();
        _barrelRotation = 0f;
    }

    public Vector2 BallPosition
    {
        get
        {
            float opposite = (float)Math.Sin(_barrelRotation) * _cannonBarrel.Width * 0.75f;
            float adjacent = (float)Math.Cos(_barrelRotation) * _cannonBarrel.Width * 0.75f;
            return _position + new Vector2(adjacent, opposite);
        }
    }
}
