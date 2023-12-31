using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;
public class LevelMenuState : GameState
{
    public LevelMenuState()
    {
        AddChild(new SpriteGameObject("Sprites/spr_background_levelselect"));
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
