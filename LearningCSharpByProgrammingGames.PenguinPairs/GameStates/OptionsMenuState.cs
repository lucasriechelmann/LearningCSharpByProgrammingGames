using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;

public class  OptionsMenuState : GameState
{
    Button _backButton;
    Switch _hintsSwitch;
    Slider _musicVolumeSlider;
    public OptionsMenuState()
    {
        AddChild(new SpriteGameObject("Sprites/spr_background_options", 0));        

        _backButton = new Button("Sprites/UI/spr_button_back", 0);
        _backButton.LocalPosition = new Vector2(415, 720);
        AddChild(_backButton);

        // add a switch for enabling/disabling hints
        // - text
        TextGameObject hintsLabel = new TextGameObject("Fonts/MenuFont", 0, Color.DarkBlue);
        hintsLabel.Text = "Hints";
        hintsLabel.LocalPosition = new Vector2(150, 340);
        AddChild(hintsLabel);
        // - switch
        _hintsSwitch = new Switch("Sprites/UI/spr_switch@2", 0);
        _hintsSwitch.LocalPosition = new Vector2(650, 340);
        AddChild(_hintsSwitch);

        // add a slider to control the background music volume
        // - text
        TextGameObject musicVolumeLabel = new TextGameObject("Fonts/MenuFont", 0, Color.DarkBlue);
        musicVolumeLabel.Text = "Music Volume";
        musicVolumeLabel.LocalPosition = new Vector2(150, 480);
        AddChild(musicVolumeLabel);
        // - slider
        _musicVolumeSlider = new Slider("Sprites/UI/spr_slider_bar", "Sprites/UI/spr_slider_button", 0, 1, 8);
        _musicVolumeSlider.LocalPosition = new Vector2(650, 500);
        AddChild(_musicVolumeSlider);

        // apply the initial game settings
        _musicVolumeSlider.Value = MediaPlayer.Volume;
        _hintsSwitch.Selected = PenguinPairsGame.HintsEnabled;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (_backButton.Pressed)
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_Title);
        }

        if (_hintsSwitch.Pressed)
            PenguinPairsGame.HintsEnabled = _hintsSwitch.Selected;

        if (_musicVolumeSlider.ValueChanged)
            MediaPlayer.Volume = _musicVolumeSlider.Value;
    }
}
