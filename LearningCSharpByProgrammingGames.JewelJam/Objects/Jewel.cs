using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.JewelJam.Objects;
public class Jewel : SpriteGameObject
{
    public int ColorType { get; set; }
    public int ShapeType { get; set; }
    public int NumberType { get; set; }
    Rectangle _spriteRectangle;
    public Jewel() : base("spr_jewels")
    {
        ColorType = ExtendedGame.Random.Next(3);
        ShapeType = ExtendedGame.Random.Next(3);
        NumberType = ExtendedGame.Random.Next(3);
        int index = ColorType * 9 + ShapeType * 3 + NumberType;
        _spriteRectangle = new(index * _sprite.Height, 0, _sprite.Height, _sprite.Height);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //draw the correct sprite part at the jewel's position
        spriteBatch.Draw(_sprite, GlobalPosition, _spriteRectangle, Color.White);
    }
}
