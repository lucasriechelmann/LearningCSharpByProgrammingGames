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
    int _gridWidth, _gridHeight, _cellSize;
    public JewelGrid(int width, int height, int cellSize, Vector2 offset)
    {
        // copy the width, height, and cell size
        _gridWidth = width;
        _gridHeight = height;
        _cellSize = cellSize;

        // copy the position
        LocalPosition = offset;

        Reset();
    }
    public override void Reset()
    {
        // initialize the grid
        _grid = new Jewel[_gridWidth, _gridHeight];

        // fill the grid with random jewels
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                // add a new jewel to the grid
                _grid[x, y] = new Jewel(ExtendedGame.Random.Next(3));
                // set the position of that jewel
                _grid[x, y].LocalPosition = GetCellPosition(x, y);
                _grid[x, y].Parent = this;
            }
        }
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        //When the spacebar is pressed, move all rows down
        if(inputHelper.KeyPressed(Keys.Space))
            MoveRowsDown();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach(Jewel jewel in _grid)
            jewel.Draw(gameTime, spriteBatch);
    }
    /// <summary>
    /// Moves all jewels one row down, and then refills the top row of the grid with new random jewels.
    /// </summary>
    void MoveRowsDown()
    {
        for (int y = _gridHeight - 1; y > 0; y--)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                _grid[x, y] = _grid[x, y - 1];
                _grid[x, y].LocalPosition = GetCellPosition(x, y);
            }
        }

        // refill the top row
        for (int x = 0; x < _gridWidth; x++)
        {
            _grid[x, 0] = new Jewel(ExtendedGame.Random.Next(3));
            _grid[x, 0].LocalPosition = GetCellPosition(x, 0);
            _grid[x, 0].Parent = this;
        }
    }
    /// <summary>
    /// Converts cell coordinates to a position in the game world.
    /// </summary>
    /// <param name="x">The x-coordinate of a grid cell.</param>
    /// <param name="y">The y-coordinate of a grid cell.</param>
    /// <returns>A Vector2 instance that represents the game world position of the given grid cell.</returns>
    Vector2 GetCellPosition(int x, int y) =>
        new Vector2(x * _cellSize, y * _cellSize);
}
