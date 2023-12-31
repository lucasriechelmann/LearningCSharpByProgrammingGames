using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;

public class PlayingState : GameState
{
    public PlayingState()
    {
        AddChild(new SpriteGameObject("Sprites/spr_background_level"));
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (inputHelper.KeyPressed(Keys.Back))
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_LevelSelect);
        }
    }
}