using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.Engine;
/// <summary>
/// A class that can represent a sprite sheet: an image containing a grid of sprites.
/// </summary>
public class SpriteSheet
{
    Texture2D _sprite;
    Rectangle _spriteRectangle;
    int _sheetIndex, _sheetColumns, _sheetRows;    
    public SpriteSheet(string assetName, int sheetIndex = 0)
    {
        // retrieve the sprite
        _sprite = ExtendedGame.AssetManager.LoadSprite(assetName);

        _sheetColumns = 1;
        _sheetRows = 1;

        // see if we can extract the number of sheet elements from the assetname
        string[] assetSplit = assetName.Split('@');
        if (assetSplit.Length >= 2)
        {
            // behind the last '@' symbol, there should be a number.
            // This number can be followed by an 'x' and then another number.
            string sheetNrData = assetSplit[assetSplit.Length - 1];
            string[] columnAndRow = sheetNrData.Split('x');
            _sheetColumns = int.Parse(columnAndRow[0]);
            if (columnAndRow.Length == 2)
                _sheetRows = int.Parse(columnAndRow[1]);
        }

        // apply the sheet index; this will also calculate spriteRectangle
        SheetIndex = sheetIndex;        
    }
    public SpriteSheet(string assetName, int sheetColumns, int sheetRows, int sheetIndex = 0)
    {
        // retrieve the sprite
        _sprite = ExtendedGame.AssetManager.LoadSprite(assetName);

        _sheetColumns = sheetColumns;
        _sheetRows = sheetRows;

        // apply the sheet index; this will also calculate spriteRectangle
        SheetIndex = sheetIndex;
    }
    public Texture2D Texture => _sprite;
    /// <summary>
    /// Draws the sprite (or the appropriate part of it) at the desired position.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object used for drawing sprites.</param>
    /// <param name="position">A position in the game world.</param>
    /// <param name="origin">An origin that should be subtracted from the drawing position.</param>
    public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin)
    {
        spriteBatch.Draw(_sprite, 
            position, 
            _spriteRectangle, 
            Color.White, 
            0, 
            origin, 
            1, 
            SpriteEffects.None, 
            0);
    }
    /// <summary>
    /// Gets the width of a single sprite in this sprite sheet.
    /// </summary>
    public int Width => _sprite.Width / _sheetColumns;
    /// <summary>
    /// Gets the height of a single sprite in this sprite sheet.
    /// </summary>
    public int Height => _sprite.Height / _sheetRows;
    /// <summary>
    /// Gets a vector that represents the center of a single sprite in this sprite sheet.
    /// </summary>
    public Vector2 Center => new Vector2(Width, Height) / 2;
    /// <summary>
    /// Gets or sets the sprite index within this sprite sheet to use. 
    /// If you set a new index, the object will recalculate which part of the sprite should be drawn.
    /// </summary>
    public int SheetIndex
    {
        get => _sheetIndex;
        set
        {
            if (value >= 0 && value < NumberOfSheetElements)
            {
                _sheetIndex = value;

                // recalculate the part of the sprite to draw
                int columnIndex = _sheetIndex % _sheetColumns;
                int rowIndex = _sheetIndex / _sheetColumns;
                _spriteRectangle = new Rectangle(columnIndex * Width, rowIndex * Height, Width, Height);
            }
        }
    }
    /// <summary>
    /// Gets a Rectangle that represents the bounds of a single sprite in this sprite sheet.
    /// </summary>
    public Rectangle Bounds => new Rectangle(0, 0, Width, Height);
    /// <summary>
    /// Gets the total number of elements in this sprite sheet.
    /// </summary>
    public int NumberOfSheetElements => _sheetColumns * _sheetRows;
}
