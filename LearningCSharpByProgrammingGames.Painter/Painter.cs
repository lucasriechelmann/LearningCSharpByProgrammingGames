using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LearningCSharpByProgrammingGames.Painter
{
    public class Painter : ExtendedGame
    {
        static GameWorld _gameWorld;
        public static GameWorld GameWorld => _gameWorld;
        public static Vector2 ScreenSize { get; set; }
        public Painter() 
        {
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {            
            Window.Title = "Painter";
            worldSize = new Point(800, 480);
            windowSize = new Point(800, 480);
            ScreenSize = new Vector2(windowSize.X, windowSize.Y);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            _gameWorld = new GameWorld(Content);
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _gameWorld.HandleInput(_inputHelper);
            _gameWorld.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            //base.Draw(gameTime);
            _gameWorld.Draw(gameTime, _spriteBatch);            
        }
    }
}
