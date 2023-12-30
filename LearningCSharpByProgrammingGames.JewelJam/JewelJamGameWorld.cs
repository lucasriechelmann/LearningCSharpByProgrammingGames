using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.JewelJam.Objects;
using Microsoft.Xna.Framework;

namespace LearningCSharpByProgrammingGames.JewelJam;

public class JewelJamGameWorld : GameObjectList
{
    /// <summary>
    /// The width of the grid: the number of cells in the horizontal direction.
    /// </summary>
    const int GridWidth = 5;
    /// <summary>
    /// The height of the grid: the number of cells in the vertical direction.
    /// </summary>
    const int GridHeight = 10;
    /// <summary>
    /// The horizontal and distance between two adjacent grid cells.
    /// </summary>
    const int CellSize = 85;

    // The size of the game world, in game units.
    public Point Size { get; private set; }

    // The player's current score.
    public int Score { get; private set; }

    public JewelJamGameWorld()
    {
        // add the background
        SpriteGameObject background = new SpriteGameObject("spr_background");
        Size = new Point(background.Width, background.Height);
        AddChild(background);

        // add a "playing field" parent object for the grid and all related objects
        GameObjectList playingField = new GameObjectList();
        playingField.LocalPosition = new Vector2(85, 150);
        AddChild(playingField);

        // add the grid to the playing field
        JewelGrid grid = new JewelGrid(GridWidth, GridHeight, CellSize);
        playingField.AddChild(grid);

        // add the row selector to the playing field
        playingField.AddChild(new RowSelector(grid));

        //add a background sprite for the score object
        SpriteGameObject scoreFrame = new("spr_scoreframe");
        scoreFrame.LocalPosition = new Vector2(20, 20);
        AddChild(scoreFrame);

        //add the object that displays the score
        ScoreGameObject scoreObject = new();
        scoreObject.LocalPosition = new Vector2(270, 30);
        AddChild(scoreObject);

        // reset some game parameters
        Reset();
    }

    public void AddScore(int points)
    {
        Score += points;
    }

    public override void Reset()
    {
        base.Reset();
        Score = 0;
    }
}
