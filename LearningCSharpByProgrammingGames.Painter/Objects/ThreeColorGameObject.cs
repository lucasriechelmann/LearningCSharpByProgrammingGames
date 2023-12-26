using LearningCSharpByProgrammingGames.Painter.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.Painter.Objects;

public class ThreeColorGameObject
{
    protected Texture2D _colorRed, _colorGreen, _colorBlue;
    Color _color;
    protected Vector2 _position, _origin, _velocity;
    protected float _rotation;
    protected ThreeColorGameObject(ContentManager content, string redSprite, string greenSprite, string blueSprite)
    {
        // load the three sprites
        _colorRed = content.Load<Texture2D>(redSprite);
        _colorGreen = content.Load<Texture2D>(greenSprite);
        _colorBlue = content.Load<Texture2D>(blueSprite);

        // default origin: center of a sprite
        _origin = new Vector2(_colorRed.Width / 2.0f, _colorRed.Height / 2.0f);

        // initialize other things
        _position = Vector2.Zero;        
        _velocity = Vector2.Zero;
        _rotation = 0f;

        Reset();
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
    }

    public virtual void Update(GameTime gameTime)
    {
        _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // determine the sprite based on the current color
        Texture2D currentSprite;
        if (Color == Color.Red)
            currentSprite = _colorRed;
        else if (Color == Color.Green)
            currentSprite = _colorGreen;
        else
            currentSprite = _colorBlue;

        // draw that sprite
        spriteBatch.Draw(currentSprite, 
            _position, 
            null, 
            Color.White,
            _rotation, 
            _origin, 
            1.0f, 
            SpriteEffects.None, 
            0);
    }

    public virtual void Reset()
    {
        Color = Color.Blue;
    }

    public Vector2 Position
    {
        get { return _position; }
    }

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
        get { return _color; }
        set
        {
            if (value != Color.Red && value != Color.Green && value != Color.Blue)
            {
                return;
            }
            _color = value;
        }
    }
}
