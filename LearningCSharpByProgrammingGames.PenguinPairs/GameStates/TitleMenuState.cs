using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;

public class TitleMenuState : GameState
{
    public TitleMenuState()
    {
        AddChild(new SpriteGameObject("Sprites/spr_titlescreen"));
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (inputHelper.KeyPressed(Keys.H))
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_Help);
        }

        if (inputHelper.KeyPressed(Keys.O))
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_Options);
        }

        if (inputHelper.KeyPressed(Keys.L))
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_LevelSelect);
        }
    }
}
