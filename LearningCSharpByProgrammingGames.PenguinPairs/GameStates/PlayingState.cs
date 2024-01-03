using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.Engine.UI;
using LearningCSharpByProgrammingGames.PenguinPairs.LevelObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.PenguinPairs.GameStates;

public class PlayingState : GameState
{
    Button _hintButton, _retryButton, _quitButton;
    Level _level;
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

        if(_level is not null)
            _level.HandleInput(inputHelper);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if(_level is not null)
            _level.Update(gameTime);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);

        if(_level is not null)
            _level.Draw(gameTime, spriteBatch);
    }
    public void LoadLevel(int levelIndex)
    {
        _level = new Level(levelIndex, $"Content/Levels/level{levelIndex}.txt");
        // update the visibilty of the hint button
        _hintButton.Visible = PenguinPairsGame.HintsEnabled;
    }
}