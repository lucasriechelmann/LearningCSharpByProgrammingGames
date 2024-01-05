using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using System;

namespace LearningCSharpByProgrammingGames.TickTick.LevelObjects;

public class WaterDrop : SpriteGameObject
{
    protected float bounce;

    public WaterDrop() : base("Sprites/LevelObjects/spr_water", TickTickGame.Depth_LevelObjects)
    {
        SetOriginToCenter();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        double t = gameTime.TotalGameTime.TotalSeconds * 3.0f + localPosition.X;
        bounce = (float)Math.Sin(t) * 0.2f;
        localPosition.Y += bounce;
    }
}