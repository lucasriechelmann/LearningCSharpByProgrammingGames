﻿using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;

namespace LearningCSharpByProgrammingGames.PenguinPairs.LevelObjects;

public class PairList : GameObjectList
{
    // How many pairs the player has already made in this level.
    int nrPairsMade;
    // The sprite objects that this PairList shows; one for each (target) pair.
    SpriteGameObject[] pairObjects;

    /// <summary>
    /// Creates a new PairList with the specified target number of pairs.
    /// </summary>
    /// <param name="nrPairs">The number of pairs that the player should make.</param>
    public PairList(int nrPairs)
    {
        // add the background image
        AddChild(new SpriteGameObject("Sprites/spr_frame_goal", 0.3f));

        // add a sprite object for each pair that the player should make
        Vector2 offset = new Vector2(100, 7);
        pairObjects = new SpriteGameObject[nrPairs];
        for (int i = 0; i < nrPairs; i++)
        {
            pairObjects[i] = new SpriteGameObject("Sprites/spr_penguin_pairs@8", 0.4f, 7);
            pairObjects[i].LocalPosition = offset + new Vector2(i * pairObjects[i].Width, 0);
            AddChild(pairObjects[i]);
        }

        // start at 0 pairs
        nrPairsMade = 0;
    }

    /// <summary>
    /// Updates this PairList to show that a new pair has been found.
    /// </summary>
    /// <param name="penguinIndex">The sheet index (color) of the new pair.</param>
    public void AddPair(int penguinIndex)
    {
        pairObjects[nrPairsMade].SheetIndex = penguinIndex;
        nrPairsMade++;
    }

    /// <summary>
    /// Returns whether or not all pairs of the level have been found.
    /// </summary>
    public bool Completed { get { return nrPairsMade == pairObjects.Length; } }
}