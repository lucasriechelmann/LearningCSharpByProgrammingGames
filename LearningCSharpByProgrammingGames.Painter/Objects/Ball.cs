using LearningCSharpByProgrammingGames.Painter.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LearningCSharpByProgrammingGames.Painter.Objects;

public class Ball
{
    Texture2D _colorRed, _colorGreen, _colorBlue;
    Vector2 _position, _origin;
    Vector2 _velocity;
    ObjectColor _color;
    bool _shooting;
    public Ball(ContentManager content)
    {
        _colorRed = content.Load<Texture2D>("spr_ball_red");
        _colorGreen = content.Load<Texture2D>("spr_ball_green");
        _colorBlue = content.Load<Texture2D>("spr_ball_blue");
        _origin = new Vector2(_colorRed.Width / 2.0f, _colorRed.Height / 2.0f);
        Reset();
    }
    public void HandleInput(InputHelper inputHelper, Cannon cannon)
    {
        if (inputHelper.MouseLeftButtonPressed && !_shooting)
        {
            _shooting = true;
            _velocity = (inputHelper.MousePosition - cannon.Position) * 1.2f;
        }
    }
    public void Update(GameTime gameTime, Cannon cannon)
    {
        if (_shooting)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _velocity.Y += 400.0f * dt;
            _position += _velocity * dt;

            if (GameWorld.IsOutsideWorld(_position))
                Reset();
            return;
        }

        Color = cannon.Color;
        _position = cannon.BallPosition;
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Texture2D currentSprite = null;

        if(_color == ObjectColor.Red)
            currentSprite = _colorRed;
        if(_color == ObjectColor.Green)
            currentSprite = _colorGreen;
        if(_color == ObjectColor.Blue)
            currentSprite = _colorBlue;

        if(currentSprite is null)
            currentSprite = _colorRed;

        spriteBatch.Draw(currentSprite, _position, null, Color.White, 0f, _origin, 1f, SpriteEffects.None, 0f);
    }
    public void Reset()
    {
        _position = new Vector2(65, 390);        
        _velocity = Vector2.Zero;
        _color = ObjectColor.Blue;
        _shooting = false;
    }
    public Vector2 Position => _position;
    public Rectangle BoundingBox
    {
        get
        {
            Rectangle spriteBounds = _colorRed.Bounds;
            spriteBounds.Offset(_position - _origin);
            return spriteBounds;
        }
    }
    public Color Color
    {
        get => _color switch
        {
            ObjectColor.Red => Color.Red,
            ObjectColor.Green => Color.Green,
            ObjectColor.Blue => Color.Blue,
            _ => throw new NotImplementedException(),
        };
        set
        {
            if (value == Color.Red)
                _color = ObjectColor.Red;

            if (value == Color.Green)
                _color = ObjectColor.Green;

            if (value == Color.Blue)
                _color = ObjectColor.Blue;
        }
    }
}
