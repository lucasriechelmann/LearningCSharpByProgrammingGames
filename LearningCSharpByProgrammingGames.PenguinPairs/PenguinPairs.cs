using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.PenguinPairs.GameStates;
using Microsoft.Xna.Framework;

namespace LearningCSharpByProgrammingGames.PenguinPairs;

public class PenguinPairsGame : ExtendedGame
{
    public const string StateName_Title = "title";
    public const string StateName_Help = "help";
    public const string StateName_Options = "options";
    public const string StateName_LevelSelect = "levelselect";
    public const string StateName_Playing = "playing";
    /// <summary>
    /// Whether or not hints are enabled in the game.
    /// </summary>
    public static bool HintsEnabled { get; set; }
    public PenguinPairsGame()
    {        
        IsMouseVisible = true;
        HintsEnabled = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        // set a custom world and window size
        _worldSize = new Point(1200, 900);
        _windowSize = new Point(1024, 768);

        // to let these settings take effect, we need to set the FullScreen property again
        FullScreen = false;

        // add the game states
        GameStateManager.AddGameState(StateName_Title, new TitleMenuState());
        GameStateManager.AddGameState(StateName_Help, new HelpState());
        GameStateManager.AddGameState(StateName_Options, new OptionsMenuState());
        GameStateManager.AddGameState(StateName_LevelSelect, new LevelMenuState());
        GameStateManager.AddGameState(StateName_Playing, new PlayingState());

        // start at the title screen
        GameStateManager.SwitchTo(StateName_Title);
    }
}
