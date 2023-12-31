﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace LearningCSharpByProgrammingGames.Engine;
/// <summary>
/// A class for handling all types of assets: sprites, fonts, music, and sound effects.
/// </summary>
public class AssetManager
{
    ContentManager _contentManager;

    public AssetManager(ContentManager content)
    {
        _contentManager = content;
    }

    /// <summary>
    /// Loads and returns the sprite with the given asset name.
    /// </summary>
    /// <param name="assetName">The name of the asset to load.</param>
    /// <returns>A Texture2D object containing the loaded sprite.</returns>
    public Texture2D LoadSprite(string assetName)
    {
        return _contentManager.Load<Texture2D>(assetName);
    }

    /// <summary>
    /// Loads and returns the font with the given asset name.
    /// </summary>
    /// <param name="assetName">The name of the asset to load.</param>
    /// <returns>A SpriteFont object containing the loaded font.</returns>
    public SpriteFont LoadFont(string assetName)
    {
        return _contentManager.Load<SpriteFont>(assetName);
    }

    /// <summary>
    /// Loads and plays the sound effect with the given asset name.
    /// </summary>
    /// <param name="assetName">The name of the asset to load.</param>
    public void PlaySoundEffect(string assetName)
    {
        SoundEffect snd = _contentManager.Load<SoundEffect>(assetName);
        snd.Play();
    }

    /// <summary>
    /// Loads and plays the song with the given asset name.
    /// </summary>
    /// <param name="assetName">The name of the asset to load.</param>
    /// <param name="repeat">Whether or not the song should loop.</param>
    public void PlaySong(string assetName, bool repeat)
    {
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Play(_contentManager.Load<Song>(assetName));
    }

}
