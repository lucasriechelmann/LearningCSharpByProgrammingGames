using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LearningCSharpByProgrammingGames.Painter.Objects;
/// <summary>
/// Represents a cannon object in the game.
/// </summary>
public class Cannon : ThreeColorGameObject
{
    // extra sprite for the rotating cannon barrel, with its own origin and rotation
    Texture2D _cannonBarrel;
    Vector2 _barrelOrigin;
    float _barrelRotation;
    /// <summary>
    /// Creates a new Cannon instance.
    /// </summary>
    /// <param name="Content">A ContentManager object, required for loading assets.</param>
    public Cannon(ContentManager content) : base(content, "spr_cannon_red", "spr_cannon_green", "spr_cannon_blue")
    {
        // initialize the extra sprite
        _cannonBarrel = content.Load<Texture2D>("spr_cannon_barrel");
        _position = new Vector2(72, 405);

        // change the cannon's position
        _barrelOrigin = new Vector2(_cannonBarrel.Height / 2, _cannonBarrel.Height / 2);
    }
    /// <summary>
    /// Performs input handling for the cannon. 
    /// The cannon changes its color when the player presses the R/G/B keys. 
    /// Also, the cannon's barrel rotates so that it points to the current mouse position.
    /// </summary>
    /// <param name="inputHelper">An object that contains information about the mouse and keyboard state.</param>
    public override void HandleInput(InputHelper inputHelper)
    {
        // change the color when the player presses R/G/B
        if (inputHelper.KeyPressed(Keys.R))
        {
            Color = Color.Red;
        }
        else if (inputHelper.KeyPressed(Keys.G))
        {
            Color = Color.Green;
        }
        else if (inputHelper.KeyPressed(Keys.B))
        {
            Color = Color.Blue;
        }

        // change the angle depending on the mouse position
        double opposite = inputHelper.MousePositionScreen.Y - _position.Y;
        double adjacent = inputHelper.MousePositionScreen.X - _position.X;
        _barrelRotation = (float)Math.Atan2(opposite, adjacent);
    }
    /// <summary>
    /// Draws the cannon on the screen.
    /// </summary>
    /// <param name="gameTime">An object that contains information about the game time that has passed.</param>
    /// <param name="spriteBatch">The sprite batch used for drawing sprites and text.</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // draw the barrel separately
        spriteBatch.Draw(_cannonBarrel, _position, null, Color.White, _barrelRotation, _barrelOrigin, 1.0f, SpriteEffects.None, 0);
        // draw the rest of the cannon: this is defined in the parent class
        base.Draw(gameTime, spriteBatch);
    }
    /// <summary>
    /// Resets the cannon to its initial state.
    /// </summary>
    public override void Reset()
    {
        base.Reset();
        _barrelRotation = 0f;
    }
    /// <summary>
    /// Computes and returns the position that the ball should currently have, based on the cannon's current angle. 
    /// The position is chosen so that the ball lies nicely inside the tip of the cannon barrel.
    /// </summary>
    public Vector2 BallPosition
    {
        get
        {
            float opposite = (float)Math.Sin(_barrelRotation) * _cannonBarrel.Width * 0.75f;
            float adjacent = (float)Math.Cos(_barrelRotation) * _cannonBarrel.Width * 0.75f;
            return _position + new Vector2(adjacent, opposite);
        }
    }
}
