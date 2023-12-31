using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;

public class HelpState : GameState
{
    public HelpState()
    {
        AddChild(new SpriteGameObject("Sprites/spr_background_help"));        
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (inputHelper.KeyPressed(Keys.Back))
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_Title);
        }
    }
}
