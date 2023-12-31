﻿using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;
public class LevelMenuState : GameState
{
    Button backButton;

    // An array of extra references to the level buttons. 
    // This makes it easier to check if a level button has been pressed.
    LevelButton[] levelButtons;

    public LevelMenuState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Sprites/spr_background_levelselect", 0);
        gameObjects.AddChild(background);

        // add a back button
        backButton = new Button("Sprites/UI/spr_button_back", 0.1f);
        backButton.LocalPosition = new Vector2(415, 720);
        gameObjects.AddChild(backButton);

        // Add a level button for each level.
        // For now, let's pretend that there are 12 levels, without actually loading them yet.
        int numberOfLevels = 12;
        levelButtons = new LevelButton[numberOfLevels];

        Vector2 gridOffset = new Vector2(155, 230);
        const int buttonsPerRow = 5;
        const int spaceBetweenColumns = 30;
        const int spaceBetweenRows = 5;

        for (int i = 0; i < PenguinPairsGame.NumberOfLevels; i++)
        {
            // create the button
            LevelButton levelButton = new LevelButton(i + 1, PenguinPairsGame.GetLevelStatus(i + 1));

            // give it the correct position
            int row = i / buttonsPerRow;
            int column = i % buttonsPerRow;

            levelButton.LocalPosition = gridOffset + new Vector2(
                column * (levelButton.Width + spaceBetweenColumns),
                row * (levelButton.Height + spaceBetweenRows)
            );

            // add the button as a child object
            gameObjects.AddChild(levelButton);
            // also store it in the array of level buttons
            levelButtons[i] = levelButton;
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        // if the back button is pressed, go back to the title screen
        if (backButton.Pressed)
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_Title);

        // if a (non-locked) level button has been pressed, go to that level
        foreach (LevelButton button in levelButtons)
        {
            if (button.Pressed && button.Status != LevelStatus.Locked)
            {
                // go to the playing state
                ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_Playing);

                // load the correct level
                PlayingState playingState = (PlayingState)ExtendedGame
                    .GameStateManager.GetGameState(PenguinPairsGame.StateName_Playing);
                playingState.LoadLevel(button.LevelIndex);

                return;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        foreach (LevelButton button in levelButtons)
        {
            if (button.Status != PenguinPairsGame.GetLevelStatus(button.LevelIndex))
                button.Status = PenguinPairsGame.GetLevelStatus(button.LevelIndex);
        }
    }
}
