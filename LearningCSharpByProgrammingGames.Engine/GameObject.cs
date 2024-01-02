using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.Engine;

public abstract class GameObject : IGameLoopObject
{
    public GameObject Parent { get; set; }
    public Vector2 LocalPosition { get; set; }
    public Vector2 GlobalPosition => 
        Parent == null ? LocalPosition : LocalPosition + Parent.GlobalPosition;
    protected Vector2 _velocity;
    public bool Visible { get; set; }
    public GameObject()
    {
        LocalPosition = Vector2.Zero;
        _velocity = Vector2.Zero;
        Visible = true;
    }
    public virtual void HandleInput(InputHelper inputHelper)
    {
    }
    public virtual void Update(GameTime gameTime)
    {
        LocalPosition += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
    public virtual void Reset()
    {
        _velocity = Vector2.Zero;
    }
}
