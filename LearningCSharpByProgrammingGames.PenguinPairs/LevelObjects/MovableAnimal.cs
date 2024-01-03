using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;

namespace LearningCSharpByProgrammingGames.PenguinPairs.LevelObjects;

public class MovableAnimal : Animal
{
    bool _isInHole;
    public MovableAnimal(Level level, int animalIndex, bool isInHole) : 
        base(level, GetSpriteName(isInHole), animalIndex)
    {
        _isInHole = isInHole;
    }
    public int AnimalIndex => SheetIndex;
    static string GetSpriteName(bool isInHole) => isInHole ?
        "Sprites/LevelObjects/spr_penguin_boxed@8" : "Sprites/LevelObjects/spr_penguin@8";
    public bool IsInHole
    {
        get => _isInHole;
        set
        {
            _isInHole = value;
            _sprite = new(GetSpriteName(value), AnimalIndex);
        }
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        if (Visible && BoundingBox.Contains(inputHelper.MousePositionWorld) && 
            inputHelper.MouseLeftButtonPressed())
        {
            _level.SelectAnimal(this);
        }
    }
    public void TryMoveInDirection(Point direction)
    {

    }
    public bool CanMoveInDirection(Point direction)
    {
        return true;
    }
}