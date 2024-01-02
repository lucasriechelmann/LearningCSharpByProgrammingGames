using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;

public class PlayingState : GameState
{
    Button _hintButton, _retryButton, _quitButton;
    public PlayingState()
    {
        AddChild(new SpriteGameObject("Sprites/spr_background_level"));

        // add a "hint" button
        _hintButton = new Button("Sprites/UI/spr_button_hint");
        _hintButton.LocalPosition = new Vector2(916, 20);
        AddChild(_hintButton);

        // add a "retry" button, initially invisible
        _retryButton = new Button("Sprites/UI/spr_button_retry");
        _retryButton.LocalPosition = new Vector2(916, 20);
        _retryButton.Visible = false;
        AddChild(_retryButton);

        // add a "quit" button
        _quitButton = new Button("Sprites/UI/spr_button_quit");
        _quitButton.LocalPosition = new Vector2(1058, 20);
        AddChild(_quitButton);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (_quitButton.Pressed)
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_LevelSelect);
        }
    }
}