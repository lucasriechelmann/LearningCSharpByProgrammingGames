using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.JewelJam.Objects;

public class RowSelector : SpriteGameObject
{
    int _selectedRow;
    JewelGrid _grid;
    public RowSelector(JewelGrid grid) : base("spr_selector_frame", 1)
    {
        _grid = grid;
        _selectedRow = 0;
        Origin = new(10,10);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        if(inputHelper.KeyPressed(Keys.Up))
            _selectedRow--;

        if(inputHelper.KeyPressed(Keys.Down))
            _selectedRow++;

        _selectedRow = MathHelper.Clamp(_selectedRow, 0, _grid.Height - 1);

        LocalPosition = _grid.GetCellPosition(0, _selectedRow);

        if(inputHelper.KeyPressed(Keys.Left))
            _grid.ShiftRowLeft(_selectedRow);

        if(inputHelper.KeyPressed(Keys.Right))
            _grid.ShiftRowRight(_selectedRow);
    }
}
