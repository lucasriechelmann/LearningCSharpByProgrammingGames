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
    ObjectColor _color;
    public Ball(ContentManager content)
    {
        _colorRed = content.Load<Texture2D>("spr_ball_red");
        _colorGreen = content.Load<Texture2D>("spr_ball_green");
        _colorBlue = content.Load<Texture2D>("spr_ball_blue");
        _origin = new Vector2(_colorRed.Width / 2.0f, _colorRed.Height / 2.0f);
        Reset();
    }
    public void HandleInput(InputHelper inputHelper)
    {
    }
    public void Update(GameTime gameTime, Cannon cannon)
    {
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
        _color = ObjectColor.Blue;
    }
    public Vector2 Position => _position;
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
