﻿using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.PenguinPairs.GameStates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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
    static List<LevelStatus> _progress;
    /// <summary>
    /// The total number of levels in the game.
    /// </summary>
    public static int NumberOfLevels => _progress.Count;    
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

        // load the player's progress from a file
        LoadProgress();

        // add the game states
        GameStateManager.AddGameState(StateName_Title, new TitleMenuState());
        GameStateManager.AddGameState(StateName_Help, new HelpState());
        GameStateManager.AddGameState(StateName_Options, new OptionsMenuState());
        GameStateManager.AddGameState(StateName_LevelSelect, new LevelMenuState());
        GameStateManager.AddGameState(StateName_Playing, new PlayingState());

        // start at the title screen
        GameStateManager.SwitchTo(StateName_Title);
    }
    void LoadProgress()
    {
        // prepare a list of LevelStatus values
        _progress = new List<LevelStatus>();

        // read the "levels_status" file; add a LevelStatus for each line
        StreamReader r = new StreamReader("Content/Levels/levels_status.txt");
        string line = r.ReadLine();
        while (line != null)
        {
            if (line == "locked")
                _progress.Add(LevelStatus.Locked);
            else if (line == "unlocked")
                _progress.Add(LevelStatus.Unlocked);
            else if (line == "solved")
                _progress.Add(LevelStatus.Solved);

            // go to the next line
            line = r.ReadLine();
        }
        r.Close();
    }
    public static LevelStatus GetLevelStatus(int levelIndex) => _progress[levelIndex - 1];
    public static void SetLevelStatus(int levelIndex, LevelStatus status) =>
        _progress[levelIndex - 1] = status;
    public static void MarkLevelAsSolved(int levelIndex)
    {
        // mark this level as solved
        SetLevelStatus(levelIndex, LevelStatus.Solved);

        // if there is a next level, mark it as unlocked
        if (levelIndex < NumberOfLevels)
        {
            if (GetLevelStatus(levelIndex + 1) == LevelStatus.Locked)
                SetLevelStatus(levelIndex + 1, LevelStatus.Unlocked);
        }

        // store the new level status
        SaveProgress();
    }
    public static void SaveProgress()
    {
        // write to the "levels_status" file; add a LevelStatus for each line
        StreamWriter w = new StreamWriter("Content/Levels/levels_status.txt");
        foreach (LevelStatus status in _progress)
        {
            switch(status)
            {
                case LevelStatus.Locked:
                    w.WriteLine("locked");
                    break;
                case LevelStatus.Unlocked:
                    w.WriteLine("unlocked");
                    break;
                case LevelStatus.Solved:
                    w.WriteLine("solved");
                    break;
            }
        }
        w.Close();
    }
}
