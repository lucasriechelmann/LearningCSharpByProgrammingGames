using Microsoft.Xna.Framework;

namespace LearningCSharpByProgrammingGames.PenguinPairs.LevelObjects;

public class  Shark : Animal
{
    public Shark(Level level, Point gridPosition) 
        : base(level, gridPosition, "Sprites/LevelObjects/spr_shark")
    {

    }
}
