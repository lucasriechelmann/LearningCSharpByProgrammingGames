using LearningCSharpByProgrammingGames.Engine.UI;
using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace LearningCSharpByProgrammingGames.PenguinPairs;

public class LevelButton : Button
{
    /// <summary>
    /// Gets the index of the level that this button represents.
    /// </summary>
    public int LevelIndex { get; private set; }

    LevelStatus status;
    TextGameObject label;

    public LevelButton(int levelIndex, LevelStatus startStatus)
        : base(GetSpriteNameForStatus(startStatus), 0.1f)
    {
        LevelIndex = levelIndex;
        Status = startStatus;

        // add a label that shows the level index
        label = new TextGameObject("Fonts/ScoreFont", 0.1f,
            Color.Black, TextGameObject.Alignment.Center);
        label.LocalPosition = sprite.Center + new Vector2(0, 12);
        label.Parent = this;
        label.Text = levelIndex.ToString();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        label.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Gets or sets the status of this level button.
    /// When you change the status, the button will receive a different sprite.
    /// </summary>
    public LevelStatus Status
    {
        get { return status; }
        set
        {
            status = value;
            sprite = new SpriteSheet(GetSpriteNameForStatus(status), 0);
            SheetIndex = (LevelIndex - 1) % sprite.NumberOfSheetElements;
        }
    }

    static string GetSpriteNameForStatus(LevelStatus status)
    {
        switch(status)
        {
            case LevelStatus.Locked:
                return "Sprites/UI/spr_level_locked";
            case LevelStatus.Unlocked:
                return "Sprites/UI/spr_level_unsolved";
            case LevelStatus.Solved:
                return "Sprites/UI/spr_level_solved@6";
            default:
                throw new ArgumentException("Unknown level status: " + status);
        }
    }
}