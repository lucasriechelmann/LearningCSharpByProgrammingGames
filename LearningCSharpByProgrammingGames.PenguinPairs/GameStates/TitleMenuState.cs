using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;

public class TitleMenuState : GameState
{
    Button _playButton, _optionsButton, _helpButton;
    public TitleMenuState()
    {
        AddChild(new SpriteGameObject("Sprites/spr_titlescreen"));
        
        _playButton = new Button("Sprites/UI/spr_button_play");
        _playButton.LocalPosition = new Vector2(415, 540);
        AddChild(_playButton);

        _optionsButton = new Button("Sprites/UI/spr_button_options");
        _optionsButton.LocalPosition = new Vector2(415, 650);
        AddChild(_optionsButton);

        _helpButton = new Button("Sprites/UI/spr_button_help");
        _helpButton.LocalPosition = new Vector2(415, 760);
        AddChild(_helpButton);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (_playButton.Pressed)
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_LevelSelect);
        }

        if (_optionsButton.Pressed)
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_Options);
        }

        if (_helpButton.Pressed)
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_Help);
        }
    }
}
