using LearningCSharpByProgrammingGames.Painter.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Metadata;

namespace LearningCSharpByProgrammingGames.Painter.Objects;
public class Cannon
{    
    Texture2D _cannonBarrel;
    Texture2D _colorRed, _colorGreen, _colorBlue;
    Vector2 _barrelPosition, _barrelOrigin, _colorOrigin;
    ObjectColor _currentColor;
    float _angle;
    bool _shooting;
    public Cannon(ContentManager content) 
    {         
        _cannonBarrel = content.Load<Texture2D>("spr_cannon_barrel");
        _colorRed = content.Load<Texture2D>("spr_cannon_red");
        _colorGreen = content.Load<Texture2D>("spr_cannon_green");
        _colorBlue = content.Load<Texture2D>("spr_cannon_blue");

        _currentColor = ObjectColor.Blue;

        _barrelOrigin = new Vector2(_cannonBarrel.Height, _cannonBarrel.Height) / 2;
        _barrelPosition = new Vector2(72, 405);
        _colorOrigin = new Vector2(_colorRed.Width, _colorRed.Height) / 2;
    }
    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.R))
            _currentColor = ObjectColor.Red;
        if (inputHelper.KeyPressed(Keys.G))
            _currentColor = ObjectColor.Green;
        if (inputHelper.KeyPressed(Keys.B))
            _currentColor = ObjectColor.Blue;

        double opposite = inputHelper.MousePosition.Y - _barrelPosition.Y;
        double adjacent = inputHelper.MousePosition.X - _barrelPosition.X;
        _angle = (float)Math.Atan2(opposite, adjacent);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_cannonBarrel, 
            _barrelPosition, 
            null, 
            Color.White,
            _angle, 
            _barrelOrigin, 
            1f, 
            SpriteEffects.None, 
            0f);
        
        spriteBatch.Draw(_currentColor switch
        {
            ObjectColor.Red => _colorRed,
            ObjectColor.Green => _colorGreen,
            ObjectColor.Blue => _colorBlue,
            _ => throw new NotImplementedException(),
        }, _barrelPosition, null, Color.White, 0f, _colorOrigin, 1f, SpriteEffects.None, 0f);
    }
    public void Reset()
    {
        _currentColor = ObjectColor.Blue;
        _angle = 0f;
    }
    public Vector2 Position => _barrelPosition;
    public Color Color
    {
        get => _currentColor switch
        {
            ObjectColor.Red => Color.Red,
            ObjectColor.Green => Color.Green,
            ObjectColor.Blue => Color.Blue,
            _ => throw new NotImplementedException(),
        };
        set
        {
            if(value == Color.Red)
                _currentColor = ObjectColor.Red;
            
            if (value == Color.Green)
                _currentColor = ObjectColor.Green;
            
            if (value == Color.Blue)
                _currentColor = ObjectColor.Blue;
        }
    }
    public Vector2 BallPosition
    {
        get
        {
            float opposite = (float)Math.Sin(_angle) * _cannonBarrel.Width * 0.75f;
            float adjacent = (float)Math.Cos(_angle) * _cannonBarrel.Width * 0.75f;
            return _barrelPosition + new Vector2(adjacent, opposite);
        }
    }
}
