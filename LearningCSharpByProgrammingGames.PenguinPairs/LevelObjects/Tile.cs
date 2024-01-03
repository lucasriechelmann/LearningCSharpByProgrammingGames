using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.PenguinPairs.LevelObjects;

public class Tile : GameObject
{
    public enum Type { Normal, Empty, Wall, Hole }
    public Type TileType { get; private set; }
    SpriteGameObject _image;
    public Tile(Type type, int x, int y)
    {
        TileType = type;

        switch(type)
        {
            case Type.Normal:
                _image = new("Sprites/LevelObjects/spr_field@2", (x + y) % 2);
                break;
            case Type.Wall:
                _image = new("Sprites/LevelObjects/spr_wall");
                break;
            case Type.Hole:
                _image = new("Sprites/LevelObjects/spr_hole");
                break;
        }

        if(_image is not null)
            _image.Parent = this;        
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(_image is not null)
            _image.Draw(gameTime, spriteBatch);
    }
}
