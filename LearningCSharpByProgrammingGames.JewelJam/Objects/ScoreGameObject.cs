using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;

namespace LearningCSharpByProgrammingGames.JewelJam.Objects;

public class ScoreGameObject : TextGameObject
{
    public ScoreGameObject() : base("JewelJamFont", 1, Color.White, Alignment.Right)
    {
        
    }
    public override void Update(GameTime gameTime)
    {
        Text = JewelJamGame.GameWorld.Score.ToString();
    }
}
