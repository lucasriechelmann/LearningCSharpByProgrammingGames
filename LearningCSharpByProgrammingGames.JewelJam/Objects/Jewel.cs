﻿using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LearningCSharpByProgrammingGames.JewelJam.Objects;
public class Jewel : SpriteGameObject
{    
    public int ColorType { get; set; }
    public int ShapeType { get; set; }
    public int NumberType { get; set; }
    public Vector2 TargetPosition { get; set; }
    Rectangle _spriteRectangle;
    GlitterField _glitters;
    /// <summary>
    /// Creates a new Jewel of a random type.
    /// </summary>
    public Jewel() : base("spr_jewels")
    {
        ColorType = ExtendedGame.Random.Next(3);
        ShapeType = ExtendedGame.Random.Next(3);
        NumberType = ExtendedGame.Random.Next(3);
        // The sprite is a single sheet that contains all possible jewel sprites.
        // Calculate the part of that sprite that we want to draw.
        int index = ColorType * 9 + ShapeType * 3 + NumberType;
        _spriteRectangle = new(index * _sprite.Height, 0, _sprite.Height, _sprite.Height);

        TargetPosition = Vector2.Zero;

        _glitters = new GlitterField(_sprite, 2, _spriteRectangle);
        _glitters.Parent = this;
        _glitters.LocalPosition = -_spriteRectangle.Location.ToVector2();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //draw the correct sprite part at the jewel's position
        spriteBatch.Draw(_sprite, GlobalPosition, _spriteRectangle, Color.White);
        _glitters.Draw(gameTime, spriteBatch);
    }
    public override void Update(GameTime gameTime)
    {
        // smoothly move to the target position
        Vector2 diff = TargetPosition - LocalPosition;
        _velocity = diff * 8;

        base.Update(gameTime);
        _glitters.Update(gameTime);
    }
}
