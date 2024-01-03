using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.Engine.UI;

namespace LearningCSharpByProgrammingGames.PenguinPairs.LevelObjects;

public class Arrow : Button
{
    SpriteSheet _normalSprite, _hoverSprite;
    public Arrow(int sheetIndex) : base("Sprites/LevelObjects/spr_arrow1@4")
    {
        SheetIndex = sheetIndex;
        _normalSprite = _sprite;
        _hoverSprite = new("Sprites/LevelObjects/spr_arrow2@4", sheetIndex);        
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if(BoundingBox.Contains(inputHelper.MousePositionWorld))
            _sprite = _hoverSprite;
        else
            _sprite = _normalSprite;
    }
}
