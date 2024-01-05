using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.Engine.UI;

namespace LearningCSharpByProgrammingGames.PenguinPairs.LevelObjects;

public class Arrow : Button
{
    SpriteSheet _normalSprite, _hoverSprite;
    public Arrow(int sheetIndex) : base("Sprites/LevelObjects/spr_arrow1@4", 0)
    {
        SheetIndex = sheetIndex;
        _normalSprite = sprite;
        _hoverSprite = new("Sprites/LevelObjects/spr_arrow2@4", sheetIndex);        
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if(BoundingBox.Contains(inputHelper.MousePositionWorld))
            sprite = _hoverSprite;
        else
            sprite = _normalSprite;
    }
}
