using LearningCSharpByProgrammingGames.Engine;

namespace LearningCSharpByProgrammingGames.JewelJam.Objects;
public class Jewel : SpriteGameObject
{
    public int Type { get; set; }
    public Jewel(int type) : base($"spr_single_jewel{type + 1}")
    {
        Type = type;
    }
}
