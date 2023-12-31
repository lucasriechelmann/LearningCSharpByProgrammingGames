using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.Engine;

public class TextGameObject : GameObject
{
    protected SpriteFont _font;
    protected Color _color;
    public string Text { get; set; }
    public enum Alignment { Left, Center, Right }
    public Alignment _alignment { get; set; }
    public TextGameObject(string fontName, Color color, Alignment alignment = Alignment.Left)
    {
        _font = ExtendedGame.AssetManager.LoadFont(fontName);        
        _color = color;
        _alignment = alignment;

        Text = "";
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(!Visible)
            return;

        //calculate the origin
        Vector2 origin = new(OriginX, 0);

        //draw the text
        spriteBatch.DrawString(
            _font, 
            Text, 
            GlobalPosition, 
            _color, 
            0, 
            origin, 
            1, 
            SpriteEffects.None, 
            0);
    }
    float OriginX
    {
        get
        {
            switch(_alignment)
            {
                case Alignment.Left:
                    return 0;
                case Alignment.Center:
                    return _font.MeasureString(Text).X / 2;
                case Alignment.Right:
                    return _font.MeasureString(Text).X;
                default:
                    return 0;
            }
        }
    }
}
