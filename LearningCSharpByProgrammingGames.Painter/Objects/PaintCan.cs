using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LearningCSharpByProgrammingGames.Painter.Objects;

public class PaintCan
{
    Texture2D _colorRed, _colorGreen, _colorBlue;
    Vector2 _position, _origin, _velocity;
    ObjectColor _color, _targetColor;
    float minSpeed;
    public PaintCan(ContentManager content, float positionOffset, ObjectColor target)
    {
        _colorRed = content.Load<Texture2D>("spr_can_red");
        _colorGreen = content.Load<Texture2D>("spr_can_green");
        _colorBlue = content.Load<Texture2D>("spr_can_blue");
        _origin = new Vector2(_colorRed.Width, _colorRed.Height) / 2;
        _targetColor = target;
        minSpeed = 30;
        _position = new Vector2(positionOffset, 100);

        Reset();
    }
    public void Update(GameTime gameTime, Ball ball)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        minSpeed += 0.01f * dt;

        if(_velocity != Vector2.Zero)
        {
            _position += _velocity * dt;

            if (ball.BoundingBox.Intersects(BoundingBox))
            {
                Color = ball.Color;
                ball.Reset();
            }

            if (GameWorld.IsOutsideWorld(_position - _origin))
                Reset();

            return;
        }

        if (Painter.Random.NextDouble() < 0.01)
        {
            _velocity = CalculateRandomVelocity();
            _color = CalculateRandomColor();
        }
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Texture2D currentSprite = null;
        if (_color == ObjectColor.Red)
            currentSprite = _colorRed;
        if (_color == ObjectColor.Green)
            currentSprite = _colorGreen;
        if (_color == ObjectColor.Blue)
            currentSprite = _colorBlue;

        if (currentSprite is null)
            currentSprite = _colorRed;

        spriteBatch.Draw(currentSprite, _position, null, Color.White, 0f, _origin, 1f, SpriteEffects.None, 0f);
    }

    public void Reset()
    {
        _color = ObjectColor.Blue;
        _position.Y = -_origin.Y;
        _velocity = Vector2.Zero;
    }
    Vector2 CalculateRandomVelocity()
    {
        return new Vector2(0.0f, (float)Painter.Random.NextDouble() * 30 + minSpeed);
    }

    ObjectColor CalculateRandomColor()
    {
        int randomval = Painter.Random.Next(3);
        if (randomval == 0)
            return ObjectColor.Red;
        else if (randomval == 1)
            return ObjectColor.Green;
        else
            return ObjectColor.Blue;
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
