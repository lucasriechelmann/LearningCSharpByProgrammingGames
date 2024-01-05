using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.Painter.Objects;
/// <summary>
/// Represents an object in the game that can be red, green, or blue. 
/// The Cannon, Ball, and PaintCan classes inherit from this class.
/// </summary>
public class ThreeColorGameObject
{
    // the red, green, and blue sprites
    protected Texture2D _colorRed, _colorGreen, _colorBlue;
    /// <summary>
    /// The object's current color: red, green, or blue.
    /// </summary>
    Color _color;
    protected Vector2 _position, _origin, _velocity;
    protected float _rotation;
    /// <summary>
    /// Creates a new ThreeColorGameObject instance. 
    /// This method initializes the position-related data, and it loads three sprites (one for each color).
    /// </summary>
    /// <param name="Content">A ContentManager object, required for loading assets.</param>
    /// <param name="redSprite">The name of the red sprite to load.</param>
    /// <param name="greenSprite">The name of the green sprite to load.</param>
    /// <param name="blueSprite">The name of the blue sprite to load.</param>
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
    /// <summary>
    /// Performs input handling for this game object.
    /// By default, nothing happens. Child classes can override this behavior.
    /// </summary>
    /// <param name="inputHelper">An object that contains information about the mouse and keyboard state.</param>
    public virtual void HandleInput(InputHelper inputHelper)
    {
    }
    /// <summary>
    /// Updates this game object for one frame of the game loop. 
    /// By default, the object's position is updated according to its velocity. 
    /// Child classes can override this method to add their own behavior.
    /// </summary>
    /// <param name="gameTime">An object that contains information about the game time that has passed.</param>
    public virtual void Update(GameTime gameTime)
    {
        _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    /// <summary>
    /// Draws this game object on the screen in its current color and at its current position. 
    /// Child classes can override this method to add their own behavior.
    /// </summary>
    /// <param name="gameTime">An object that contains information about the game time that has passed.</param>
    /// <param name="spriteBatch">The sprite batch used for drawing sprites and text.</param>
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
    /// <summary>
    /// Resets this game object to its initial state. 
    /// By default, the color is reset to blue, and nothing else happens.
    /// Child classes can override this method to add their own behavior.
    /// </summary>
    public virtual void Reset()
    {
        Color = Color.Blue;
    }
    /// <summary>
    /// Gets this game object's current position.
    /// </summary>
    public Vector2 Position
    {
        get { return _position; }
    }
    /// <summary>
    /// Gets a Rectangle that describes this game object's current bounding box.
    /// This is useful for collision detection.
    /// </summary>
    public Rectangle BoundingBox
    {
        get
        {
            // get the sprite's bounds
            Rectangle spriteBounds = _colorRed.Bounds;
            // add the object's position to it as an offset
            spriteBounds.Offset(_position - _origin);
            return spriteBounds;
        }
    }
    /// <summary>
    /// Gets or sets this game object's color. 
    /// Only the colors red, green, and blue are allowed.
    /// </summary>
    public Color Color
    {
        get { return _color; }
        set
        {
            // ignore invalid colors
            if (value != Color.Red && value != Color.Green && value != Color.Blue)
                return;
            // then set the color
            _color = value;
        }
    }
}
