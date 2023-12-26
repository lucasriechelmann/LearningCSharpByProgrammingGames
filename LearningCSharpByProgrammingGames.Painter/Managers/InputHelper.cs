using Microsoft.Xna.Framework.Input;
using System.Numerics;

namespace LearningCSharpByProgrammingGames.Painter.Managers;
public class InputHelper
{
    MouseState _currentMouseState;
    MouseState _previousMouseState;
    KeyboardState _currentKeyboardState;
    KeyboardState _previousKeyboardState;
    public void Update()
    {
        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
    }
    public Vector2 MousePosition => new Vector2(_currentMouseState.X, _currentMouseState.Y);
    public bool MouseLeftButtonPressed => 
        _currentMouseState.LeftButton == ButtonState.Pressed && 
        _previousMouseState.LeftButton == ButtonState.Released;
    public bool KeyPressed(Keys key) => 
        _currentKeyboardState.IsKeyDown(key) && 
        _previousKeyboardState.IsKeyUp(key);
}
