using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace LearningCSharpByProgrammingGames.Painter.Objects;
/// <summary>
/// Represents the flying ball in the game. 
/// The game world should contain a single instance of this ball.
/// </summary>
public class Ball : ThreeColorGameObject
{
    /// <summary>
    /// Whether or not the ball is currently flying through the air.
    /// If not, the ball is stuck inside the cannon.
    /// </summary>
    bool _shooting;
    /// <summary>
    /// The sound to play when the ball gets launched.
    /// </summary>
    SoundEffect _soundShoot;
    /// <summary>
    /// Creates a new Ball instance.
    /// </summary>
    /// <param name="Content">A ContentManager object, required for loading assets.</param>
    public Ball(ContentManager content) : base(content, "spr_ball_red", "spr_ball_green", "spr_ball_blue")
    {
        // load the sound
        _soundShoot = content.Load<SoundEffect>("snd_shoot_paint");
    }
    /// <summary>
    /// Performs input handling for this ball. 
    /// The ball launches itself when the player clicks the left mouse button. 
    /// The launch speed depends on the mouse position.
    /// </summary>
    /// <param name="inputHelper">An object that contains information about the mouse and keyboard state.</param>
    public override void HandleInput(InputHelper inputHelper)
    {
        // if the player clicks the left mouse button and the ball isn't already flying, launch it
        if (inputHelper.MouseLeftButtonPressed() && !_shooting)
        {
            _shooting = true;
            // compute a velocity based on the mouse position
            _velocity = (inputHelper.MousePositionScreen - Painter.GameWorld.Cannon.Position) * 1.2f;
            // play a sound effect
            _soundShoot.Play();
        }
    }
    /// <summary>
    /// Updates this ball can for one frame of the game loop. 
    /// If the ball is flying through the air, then gravity is applied (to let the ball follow a nice-looking arc). 
    /// If the ball is not flying, it updates its position and color to match the cannon's current state.
    /// If the ball leaves the screen, it resets itself.
    /// </summary>
    /// <param name="gameTime">An object that contains information about the game time that has passed.</param>
    public override void Update(GameTime gameTime)
    {
        if (_shooting)
        {
            // apply gravity
            _velocity.Y += 400.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            // match the color of the cannon
            Color = Painter.GameWorld.Cannon.Color;
            // update the position so that the ball is placed nicely inside the cannon barrel
            _position = Painter.GameWorld.Cannon.BallPosition;
        }

        // reset the ball if it leaves the screen
        if (Painter.GameWorld.IsOutsideWorld(_position))
            Reset();

        // update the position based on the velocity: the parent class handles this
        base.Update(gameTime);
    }

    /// <summary>
    /// Resets the ball to its initial state: stuck inside the cannon barrel.
    /// </summary>
    public override void Reset()
    {
        base.Reset();
        _velocity = Vector2.Zero;
        _position = new Vector2(65, 390);
        _shooting = false;
    }
}
