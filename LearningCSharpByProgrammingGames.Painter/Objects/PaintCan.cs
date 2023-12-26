using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;

namespace LearningCSharpByProgrammingGames.Painter.Objects;
/// <summary>
/// Represents a single paint can in the game. 
/// The overall game has multiple instances of this class, one for each "column" in which the paint cans fall down.
/// </summary>
public class PaintCan : ThreeColorGameObject
{
    /// <summary>
    /// The color that this paint can should have before it leaves the screen.
    /// </summary>
    Color _targetColor;
    /// <summary>
    /// The minimum speed of the paint can, which increases over time. A random factor will always be added on top of it.
    /// </summary>
    float _minSpeed;
    /// <summary>
    /// The sound to play when the paint can has left the screen with the correct color.
    /// </summary>
    SoundEffect soundCollect;
    /// <summary>
    /// Creates a new PaintCan instance.
    /// </summary>
    /// <param name="Content">A ContentManager object, required for loading assets.</param>
    /// <param name="positionOffset">The desired x-coordinate of the paint can.</param>
    /// <param name="targetcol">The target color of the paint can</param>
    public PaintCan(ContentManager content, float positionOffset, Color target) :
        base(content, "spr_can_red", "spr_can_green", "spr_can_blue")
    {
        // set the position and target color
        _position = new Vector2(positionOffset, -_origin.Y);
        _targetColor = target;
        _minSpeed = 30;

        Reset(); // otherwise position.X is not properly set

        // load the sound
        soundCollect = content.Load<SoundEffect>("snd_collect_points");
    }
    /// <summary>
    /// Updates this paint can for one frame of the game loop. 
    /// If the paint can is falling, it checks if it has left the screen or if it is colliding with the ball.
    /// If the paint can is not falling, it will start falling with a certain probability.
    /// </summary>
    /// <param name="gameTime">An object that contains information about the game time that has passed.</param>
    public override void Update(GameTime gameTime)
    {
        // increase the minimum speed that the paint can will have the next time it respawns
        _minSpeed += 0.01f * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // update the position according to the velocity; this is defined in the parent class
        base.Update(gameTime);

        // rotate the sprite based on the Y coordinate, so that the can looks like it's swinging in the wind
        _rotation = (float)Math.Sin(_position.Y / 50.0) * 0.05f;

        if (_velocity != Vector2.Zero)
        {
            // check if this paint can collides with the ball
            if (BoundingBox.Intersects(Painter.GameWorld.Ball.BoundingBox))
            {
                Color = Painter.GameWorld.Ball.Color;
                Painter.GameWorld.Ball.Reset();
            }

            // reset the can if it leaves the screen
            if (Painter.GameWorld.IsOutsideWorld(_position - _origin))
            {
                // if the color is wrong, the player loses a life
                if (Color != _targetColor)
                    Painter.GameWorld.LoseLife();
                // otherwise, the player earns points
                else
                {
                    Painter.GameWorld.Score += 10;
                    soundCollect.Play();
                }

                Reset();
            }
        }

        // if the can is not moving, (re)spawn it with a certain probability
        else if (Painter.Random.NextDouble() < 0.01)
        {
            _velocity = CalculateRandomVelocity();
            Color = CalculateRandomColor();
        }
    }
    /// <summary>
    /// Resets the paint can's position and velocity, so that it will wait to be respawned again.
    /// Note: this method does *not* reset the minimum speed, because that should only happen when the game restarts completely. 
    /// </summary>
    public override void Reset()
    {
        base.Reset();
        _position.Y = -_origin.Y;
        _velocity = Vector2.Zero;
    }
    /// <summary>
    /// Resets the paint can's minimum speed. 
    /// Call this method when the game restarts.
    /// </summary>
    public void ResetMinSpeed()
    {
        _minSpeed = 30;
    }
    /// <summary>
    /// Computes and returns a random vertical velocity, which has the minimum speed plus a random factor.
    /// </summary>
    /// <returns>A Vector2 with an x-component of 0, and a y-component of the minimum speed plus a random factor.</returns>
    Vector2 CalculateRandomVelocity() =>
        new Vector2(0.0f, (float)Painter.Random.NextDouble() * 30 + _minSpeed);
    /// <summary>
    /// Returns a random color: red, green, or blue. All colors have the same probability of being chosen.
    /// </summary>
    /// <returns>Color.Red, Color.Green, or Color.Blue.</returns>
    public Color CalculateRandomColor()
    {
        int randomVal = Painter.Random.Next(3);
        if (randomVal == 0)
            return Color.Red;
        else if (randomVal == 1)
            return Color.Green;
        else
            return Color.Blue;
    }    
}
