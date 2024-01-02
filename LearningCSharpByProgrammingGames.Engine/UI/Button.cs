namespace LearningCSharpByProgrammingGames.Engine.UI;
/// <summary>
/// A class that can represent a UI button in the game.
/// </summary>
public class Button : SpriteGameObject
{
    public bool Pressed { get; protected set; }

    public Button(string assetName) : base(assetName)
    {
        Pressed = false;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        Pressed = Visible && inputHelper.MouseLeftButtonPressed()
            && BoundingBox.Contains(inputHelper.MousePositionWorld);
    }

    public override void Reset()
    {
        base.Reset();
        Pressed = false;
    }
}
