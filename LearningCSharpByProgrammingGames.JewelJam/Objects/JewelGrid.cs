using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.JewelJam.Objects;
/// <summary>
/// Represents a grid of jewel objects.
/// </summary>
public class JewelGrid : GameObject
{
    Jewel[,] _grid;
    int _cellSize;
    public int Height { get; private set; }
    public int Width { get; private set; }
    public JewelGrid(int width, int height, int cellSize)
    {
        // copy the width, height, and cell size
        Width = width;
        Height = height;
        _cellSize = cellSize;

        Reset();
    }
    public override void Reset()
    {
        // initialize the grid
        _grid = new Jewel[Width, Height];
        ResetGridValues();
    }
    void ResetGridValues()
    {
        // fill the grid with random jewels
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                AddJewel(x, y, y);
    }
    void AddJewel(int x, int yTarget, int yStart)
    {
        // store the jewel in the grid
        _grid[x, yTarget] = new Jewel();

        // set the parent and position of the jewel
        _grid[x, yTarget].Parent = this;
        _grid[x, yTarget].LocalPosition = GetCellPosition(x, yStart);
        _grid[x, yTarget].TargetPosition = GetCellPosition(x, yTarget);
    }
    public override void Update(GameTime gameTime)
    {
        foreach (Jewel jewel in _grid)
            jewel.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (Jewel jewel in _grid)
            jewel.Draw(gameTime, spriteBatch);
    }
    /// <summary>
    /// Converts cell coordinates to a position in the game world.
    /// </summary>
    /// <param name="x">The x-coordinate of a grid cell.</param>
    /// <param name="y">The y-coordinate of a grid cell.</param>
    /// <returns>A Vector2 instance that represents the game world position of the given grid cell.</returns>
    public Vector2 GetCellPosition(int x, int y) =>
        new Vector2(x * _cellSize, y * _cellSize);
    public override void HandleInput(InputHelper inputHelper)
    {        
        if(inputHelper.KeyPressed(Keys.F1))
        {
            MoveRowsDown();
            JewelJamGame.GameWorld.AddScore(-1);
        }

        if(inputHelper.KeyPressed(Keys.F2))
        {
            ResetGridValues();
            JewelJamGame.GameWorld.AddScore(-10);
        }

        if(!inputHelper.KeyPressed(Keys.Space))
            return;

        int middleColumn = Width / 2;
        int extraScore = 10;
        int combo = 0;
        // go over the rows from top to bottom; try to find combinations in the middle column
        for (int y = 0; y < Height - 2; y++)
        {
            if (IsValidCombination(_grid[middleColumn, y], _grid[middleColumn, y + 1], _grid[middleColumn, y + 2]))
            {
                combo++;
                JewelJamGame.GameWorld.AddScore(extraScore);
                extraScore *= 2;                

                // remove the three jewels, let the jewels above that fall down, and fill the gaps that remain
                RemoveJewel(middleColumn, y, -1);
                RemoveJewel(middleColumn, y + 1, -2);
                RemoveJewel(middleColumn, y + 2, -3);
                // skip 2 extra rows, because those are now empty
                y += 2;
                
            }
        }

        switch (combo)
        {
            case 1:
                ExtendedGame.AssetManager.PlaySoundEffect("snd_single");
                break;
            case 2:
                JewelJamGame.GameWorld.DoubleComboScored();
                ExtendedGame.AssetManager.PlaySoundEffect("snd_double");
                break;
            case 3:
                JewelJamGame.GameWorld.TripleComboScored();
                ExtendedGame.AssetManager.PlaySoundEffect("snd_triple");
                break;
            default:
                ExtendedGame.AssetManager.PlaySoundEffect("snd_error");
                break;
        }
    }    
    /// <summary>
    /// Moves all jewels one row down, and then refills the top row of the grid with new random jewels.
    /// </summary>
    void MoveRowsDown()
    {
        for (int y = Height - 1; y > 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                _grid[x, y] = _grid[x, y - 1];
                _grid[x, y].TargetPosition = GetCellPosition(x, y);
            }
        }

        // refill the top row
        for (int x = 0; x < Width; x++)
        {
            AddJewel(x, 0, -1);
        }
    }
    
    /// <summary>
    /// Removes the jewel at grid cell (x,y), and then moves other jewels down to fill in the gap that has appeared.
    /// </summary>
    /// <param name="x">The x coordinate of the jewel to remove.</param>
    /// <param name="y">The y coordinate of the jewel to remove.</param>
    void RemoveJewel(int x, int y, int yStartForNewJewel)
    {
        // move the jewels above this cell
        for (int row = y; row > 0; row--)
        {
            _grid[x, row] = _grid[x, row - 1];
            _grid[x, row].TargetPosition = GetCellPosition(x, row);
        }

        // fill the top cell with a new random jewel
        AddJewel(x, 0, yStartForNewJewel);
    }
    
    // For all three properties (color, shape, and number), 
    // that property should be the same for all jewels *or* different for all jewels.
    bool IsValidCombination(Jewel a, Jewel b, Jewel c)  =>
        IsConditionValid(a.ColorType, b.ColorType, c.ColorType) && 
        IsConditionValid(a.ShapeType, b.ShapeType, c.ShapeType) && 
        IsConditionValid(a.NumberType, b.NumberType, c.NumberType);
    // a condition is valid if all three values are equal *or* all three values are different
    bool IsConditionValid(int a, int b, int c) =>
        AllEqual(a, b, c) || AllDifferent(a, b, c);
    bool AllEqual(int a, int b, int c) => a == b && b == c;
    bool AllDifferent(int a, int b, int c) => a != b && b != c && a != c;
    public void ShiftRowLeft(int selectedRow)
    {
        //store the leftmost jewel as a backup
        Jewel first = _grid[0, selectedRow];

        //Replace all jewels in the row with the jewel to their right
        for(int x = 0; x < Width - 1; x++)
        {
            _grid[x, selectedRow] = _grid[x + 1, selectedRow];
            _grid[x, selectedRow].TargetPosition = GetCellPosition(x, selectedRow);
        }

        //re-insert the old leftmost jewel on the right
        _grid[Width - 1, selectedRow] = first;
        _grid[Width - 1, selectedRow].LocalPosition = GetCellPosition(Width, selectedRow);
        _grid[Width - 1, selectedRow].TargetPosition = GetCellPosition(Width - 1, selectedRow);
    }
    public void ShiftRowRight(int selectedRow)
    {
        //store the rightmost jewel as a backup
        Jewel last = _grid[Width - 1, selectedRow];

        //Replace all jewels in the row with the jewel to their left
        for(int x = Width - 1; x > 0; x--)
        {
            _grid[x, selectedRow] = _grid[x - 1, selectedRow];
            _grid[x, selectedRow].TargetPosition = GetCellPosition(x, selectedRow);
        }

        //re-insert the old rightmost jewel on the left
        _grid[0, selectedRow] = last;
        _grid[0, selectedRow].LocalPosition = GetCellPosition(-1, selectedRow);
        _grid[0, selectedRow].TargetPosition = GetCellPosition(0, selectedRow);
    }
}
