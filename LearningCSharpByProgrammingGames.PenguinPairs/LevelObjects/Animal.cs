using LearningCSharpByProgrammingGames.Engine;

namespace LearningCSharpByProgrammingGames.PenguinPairs.LevelObjects;

public abstract class Animal : SpriteGameObject
{
    protected Level _level;
    protected Animal(Level level, string spriteName, int sheetIndex = 0) : base(spriteName, sheetIndex)
    {
        _level = level;
    }
}
