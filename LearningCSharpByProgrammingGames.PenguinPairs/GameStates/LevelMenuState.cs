using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;
public class LevelMenuState : GameState
{
    Button _backButton;
    // An array of extra references to the level buttons. 
    // This makes it easier to check if a level button has been pressed.
    LevelButton[] _levelButtons;
    public LevelMenuState()
    {
        AddChild(new SpriteGameObject("Sprites/spr_background_levelselect"));

        _backButton = new Button("Sprites/UI/spr_button_back");
        _backButton.LocalPosition = new Vector2(415, 720);
        AddChild(_backButton);

        // Add a level button for each level.
        // For now, let's pretend that there are 12 levels, without actually loading them yet.
        int numberOfLevels = PenguinPairsGame.NumberOfLevels;
        _levelButtons = new LevelButton[numberOfLevels];

        Vector2 gridOffset = new Vector2(155, 230);
        const int buttonsPerRow = 5;
        const int spaceBetweenColumns = 30;
        const int spaceBetweenRows = 5;

        for (int i = 0; i < numberOfLevels; i++)
        {
            // create the button
            LevelButton levelButton = new LevelButton(i + 1, PenguinPairsGame.GetLevelStatus(i+1));

            // give it the correct position
            int row = i / buttonsPerRow;
            int column = i % buttonsPerRow;

            levelButton.LocalPosition = gridOffset + new Vector2(
                column * (levelButton.Width + spaceBetweenColumns),
                row * (levelButton.Height + spaceBetweenRows)
            );

            // add the button as a child object
            AddChild(levelButton);
            // also store it in the array of level buttons
            _levelButtons[i] = levelButton;
        }
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (_backButton.Pressed)
        {
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_Title);
        }

        // if a (non-locked) level button has been pressed, go to that level
        foreach (LevelButton button in _levelButtons)
        {
            if (button.Pressed && button.Status != LevelStatus.Locked)
            {
                // go to the playing state
                ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_Playing);

                PlayingState playingState = (PlayingState)ExtendedGame
                    .GameStateManager
                    .GetGameState(PenguinPairsGame.StateName_Playing);
                playingState.LoadLevel(button.LevelIndex);
                return;
            }
        }
    }
}
