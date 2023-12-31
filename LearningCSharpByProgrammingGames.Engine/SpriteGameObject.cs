using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.Engine;

public class SpriteGameObject : GameObject
{
    protected Texture2D _sprite;
    protected Vector2 _origin;
    public SpriteGameObject(string spriteName)
    {
        _sprite = ExtendedGame.AssetManager.LoadSprite(spriteName);            
        _origin = Vector2.Zero;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (Visible)
        {
            spriteBatch.Draw(
                _sprite, 
                GlobalPosition, 
                null, 
                Color.White, 
                0f, 
                _origin, 
                1f, 
                SpriteEffects.None, 
                0f);
        }
    }
    public int Width => _sprite.Width;
    public int Height => _sprite.Height;
    /// <summary>
    /// Gets a Rectangle that describes this game object's current bounding box.
    /// This is useful for collision detection.
    /// </summary>
    public Rectangle BoundingBox
    {
        get
        {
            Rectangle spriteBounds = _sprite.Bounds;
            spriteBounds.Offset(LocalPosition - _origin);
            return spriteBounds;
        }
    }
    public void SetOriginToCenter()
    {
        _origin = new Vector2(Width / 2, Height / 2);
    }
}
