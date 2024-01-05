using Microsoft.Xna.Framework;

namespace LearningCSharpByProgrammingGames.Engine.CollisionDetection;

public struct Circle
{
    public float Radius { get; private set; }
    public Vector2 Center { get; private set; }

    public Circle(float radius, Vector2 center)
    {
        Radius = radius;
        Center = center;
    }
}
