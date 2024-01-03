using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;

namespace LearningCSharpByProgrammingGames.PenguinPairs.LevelObjects;

public class MovableAnimalSelector : GameObjectList
{
    Arrow[] _arrows;
    Point[] _directions;
    MovableAnimal _selectedAnimal;
    public MovableAnimalSelector()
    {
        // define the four directions
        _directions = new Point[]
        {            
            new Point(1, 0),
            new Point(0, -1),
            new Point(-1, 0),
            new Point(0, 1)            
        };

        // add the four arrows
        _arrows = new Arrow[4];
        for(int i = 0; i < 4; i++)
        {
            _arrows[i] = new Arrow(i);
            _arrows[i].LocalPosition = new Vector2(
                _directions[i].X * _arrows[i].Width,
                _directions[i].Y * _arrows[i].Height);
            AddChild(_arrows[i]);
        }

        SelectedAnimal = null;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        if (SelectedAnimal is null)
            return;

        base.HandleInput(inputHelper);
        // check if any of the arrow buttons have been pressed
        for (int i = 0; i < 4; i++)
        {
            if (_arrows[i].Pressed)
            {
                SelectedAnimal.TryMoveInDirection(_directions[i]);
                return;
            }
        }

        // if the player clicks anywhere else, deselect the current animal
        if (inputHelper.MouseLeftButtonPressed())
            SelectedAnimal = null;
    }
    
    
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (SelectedAnimal is not null)
        {
            LocalPosition = SelectedAnimal.LocalPosition;

            // the arrows should only be visible if the animal can move in that direction
            for (int i = 0; i < 4; i++)
                _arrows[i].Visible = SelectedAnimal.CanMoveInDirection(_directions[i]);
        }
    }
    public MovableAnimal SelectedAnimal
    {
        get => _selectedAnimal;
        set
        {
            _selectedAnimal = value;
            Visible = _selectedAnimal != null;
        }
    }
}