using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;

public class HelpState : GameState
{
    Button _backButton;
    public HelpState()
    {
        AddChild(new SpriteGameObject("Sprites/spr_background_help", 0));

        _backButton = new Button("Sprites/UI/spr_button_back", 0);
        _backButton.LocalPosition = new Vector2(415, 720);
        AddChild(_backButton);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (_backButton.Pressed)
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_Title);
        }
    }
}
