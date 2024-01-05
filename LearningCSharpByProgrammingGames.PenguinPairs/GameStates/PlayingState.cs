using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.Engine.UI;
using LearningCSharpByProgrammingGames.PenguinPairs.LevelObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;

public class PlayingState : GameState
{
    Level level;

    Button hintButton, retryButton, quitButton;
    SpriteGameObject completedOverlay;

    public PlayingState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Sprites/spr_background_level", 0);
        gameObjects.AddChild(background);

        // add a "hint" button
        hintButton = new Button("Sprites/UI/spr_button_hint", 0);
        hintButton.LocalPosition = new Vector2(916, 20);
        gameObjects.AddChild(hintButton);

        // add a "retry" button, initially invisible
        retryButton = new Button("Sprites/UI/spr_button_retry", 0);
        retryButton.LocalPosition = new Vector2(916, 20);
        retryButton.Visible = false;
        gameObjects.AddChild(retryButton);

        // add a "quit" button
        quitButton = new Button("Sprites/UI/spr_button_quit", 0);
        quitButton.LocalPosition = new Vector2(1058, 20);
        gameObjects.AddChild(quitButton);

        // add an overlay image
        completedOverlay = new SpriteGameObject("Sprites/spr_level_finished", 0);
        gameObjects.AddChild(completedOverlay);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (completedOverlay.Visible)
        {
            // go to the next level?
            if (inputHelper.MouseLeftButtonPressed())
                PenguinPairsGame.GoToNextLevel(level.LevelIndex);
        }
        else
        {
            // if the "quit" button is pressed, return to the level selection screen
            if (quitButton.Pressed)
                ExtendedGame.GameStateManager.SwitchTo(PenguinPairsGame.StateName_LevelSelect);

            // if the "hint" button is pressed, show the hint arrow
            if (hintButton.Pressed)
                level.ShowHint();

            // if the "retry" button is pressed, reset the level
            if (retryButton.Pressed)
                level.Reset();

            if (level != null)
                level.HandleInput(inputHelper);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (level != null)
        {
            level.Update(gameTime);
            hintButton.Visible = PenguinPairsGame.HintsEnabled && !level.FirstMoveMade;
            retryButton.Visible = level.FirstMoveMade;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (level != null)
            level.Draw(gameTime, spriteBatch);
    }

    public void LoadLevel(int levelIndex)
    {
        level = new Level(levelIndex, "Content/Levels/level" + levelIndex + ".txt");
        completedOverlay.Visible = false;
    }

    public void LevelCompleted(int levelIndex)
    {
        completedOverlay.Visible = true;
        level.Visible = false;

        ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_won");

        PenguinPairsGame.MarkLevelAsSolved(levelIndex);
    }
}