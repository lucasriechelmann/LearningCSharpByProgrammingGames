using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.JewelJam.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.JewelJam;

public class JewelJamGameWorld : GameObjectList
{
    /// <summary>
    /// The width of the grid: the number of cells in the horizontal direction.
    /// </summary>
    const int GridWidth = 5;
    /// <summary>
    /// The height of the grid: the number of cells in the vertical direction.
    /// </summary>
    const int GridHeight = 10;
    /// <summary>
    /// The horizontal and distance between two adjacent grid cells.
    /// </summary>
    const int CellSize = 85;

    /// <summary>
    /// The size of the game world, in game units.
    /// </summary>
    public Point Size { get; private set; }

    /// <summary>
    /// The player's current score.
    /// </summary>
    public int Score { get; private set; }
    /// <summary>
    /// A reference to the moving jewel cart.
    /// </summary>
    JewelCart _jewelCart;
    // References to the different overlays and buttons.
    SpriteGameObject _titleScreen, _gameOverScreen, _helpScreen;
    /// <summary>
    /// An enum describing the possible game states that the game can be in.
    /// </summary>
    enum GameState
    {
        TitleScreen,
        Playing,
        HelpScreen,
        GameOver
    }

    // The game state that the game is currently in.
    GameState _currentState;
    SpriteGameObject _helpButton;
    JewelJamGame _game;
    VisibilityTimer _timerDouble, _timerTriple;
    public JewelJamGameWorld(JewelJamGame game)
    {
        _game = game;
        // add the background
        SpriteGameObject background = new SpriteGameObject("spr_background");
        Size = new Point(background.Width, background.Height);
        AddChild(background);

        // add a "playing field" parent object for the grid and all related objects
        GameObjectList playingField = new GameObjectList();
        playingField.LocalPosition = new Vector2(85, 150);
        AddChild(playingField);

        // add the grid to the playing field
        JewelGrid grid = new JewelGrid(GridWidth, GridHeight, CellSize);
        playingField.AddChild(grid);

        // add the row selector to the playing field
        playingField.AddChild(new RowSelector(grid));

        //add a background sprite for the score object
        SpriteGameObject scoreFrame = new("spr_scoreframe");
        scoreFrame.LocalPosition = new Vector2(20, 20);
        AddChild(scoreFrame);

        //add the object that displays the score
        ScoreGameObject scoreObject = new();
        scoreObject.LocalPosition = new Vector2(270, 30);
        AddChild(scoreObject);

        // add the jewel cart
        _jewelCart = new JewelCart(new Vector2(410, 230));
        AddChild(_jewelCart);

        //add help button
        _helpButton = new("spr_button_help");
        _helpButton.LocalPosition = new Vector2(1270, 20);
        AddChild(_helpButton);

        // add the combo images and timers
        _timerDouble = AddcomboImageWithTime("spr_double");
        _timerTriple = AddcomboImageWithTime("spr_triple");

        _titleScreen = AddOverlay("spr_title");
        _gameOverScreen = AddOverlay("spr_gameover");
        _helpScreen = AddOverlay("spr_frame_help");

        ExtendedGame.AssetManager.PlaySong("snd_music", true);

        GoToState(GameState.TitleScreen);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        if (_currentState == GameState.Playing)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.MouseLeftButtonPressed() &&
                _helpButton.BoundingBox.Contains(_game.ScreenToWorld(inputHelper.MousePosition)))
            {
                GoToState(GameState.HelpScreen);
            }            
        }            
        else if(_currentState == GameState.TitleScreen || _currentState == GameState.GameOver)
        {
            if (inputHelper.KeyPressed(Keys.Space))
            {
                Reset();
                GoToState(GameState.Playing);
            }
        }
        else if(_currentState == GameState.HelpScreen)
        {
            if (inputHelper.KeyPressed(Keys.Space))
                GoToState(GameState.Playing);
        }
    }
    public void DoubleComboScored() => _timerDouble.StartVisible(3);
    public void TripleComboScored() => _timerTriple.StartVisible(3);
    public override void Update(GameTime gameTime)
    {
        if (_currentState == GameState.Playing)
        {
            base.Update(gameTime);

            if(_jewelCart.GlobalPosition.X > Size.X - 230)
                GoToState(GameState.GameOver);
        }
        
    }
    SpriteGameObject AddOverlay(string spriteName)
    {
        SpriteGameObject overlay = new(spriteName);
        overlay.SetOriginToCenter();
        overlay.LocalPosition = new Vector2(Size.X / 2, Size.Y / 2);
        AddChild(overlay);
        return overlay;
    }
    VisibilityTimer AddcomboImageWithTime(string spriteName)
    {
        // create and add the image
        SpriteGameObject image = new(spriteName);
        image.Visible = false;
        image.LocalPosition = new Vector2(800, 400);
        AddChild(image);
        // create and add the timer, with that image as its target
        VisibilityTimer timer = new(image);
        AddChild(timer);

        return timer;
    }
    void GoToState(GameState newState)
    {
        _currentState = newState;
        _titleScreen.Visible = _currentState == GameState.TitleScreen;
        _gameOverScreen.Visible = _currentState == GameState.GameOver;
        _helpScreen.Visible = _currentState == GameState.HelpScreen;     
    }
    public void AddScore(int points)
    {
        Score += points;
        _jewelCart.PushBack();
    }

    public override void Reset()
    {
        base.Reset();
        Score = 0;
    }
}
