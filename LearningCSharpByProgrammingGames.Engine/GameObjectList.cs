using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LearningCSharpByProgrammingGames.Engine;
public class GameObjectList : GameObject
{
    public List<GameObject> _list;
    public GameObjectList()
    {
        _list = new();
    }
    public void AddChild(GameObject child)
    {
        child.Parent = this;
        _list.Add(child);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        foreach (GameObject obj in _list)
            obj.HandleInput(inputHelper);
    }
    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in _list)
            obj.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (GameObject obj in _list)
            obj.Draw(gameTime, spriteBatch);
    }
    public override void Reset()
    {
        foreach (GameObject obj in _list)
            obj.Reset();
    }
}
