using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;

namespace LearningCSharpByProgrammingGames.PenguinPairs.LevelObjects;

public class Animal : SpriteGameObject
{
    protected Level level;
    protected Point currentGridPosition;

    protected Animal(Level level, Point gridPosition, string spriteName, int sheetIndex = 0)
        : base(spriteName, 0.3f, sheetIndex)
    {
        this.level = level;
        currentGridPosition = gridPosition;
        ApplyCurrentPosition();
    }

    protected virtual void ApplyCurrentPosition()
    {
        // set the position in the game world
        LocalPosition = level.GetCellPosition(currentGridPosition.X, currentGridPosition.Y);

        // add the animal to the level's grid
        level.AddAnimalToGrid(this, currentGridPosition);
    }
}
