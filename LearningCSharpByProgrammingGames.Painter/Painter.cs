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
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            Window.Title = "Painter";
            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _inputHelper = new InputHelper(this);
            _gameWorld = new GameWorld(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _inputHelper.Update();
            _gameWorld.HandleInput(_inputHelper);
            _gameWorld.Update(gameTime);
        }
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _gameWorld.Draw(gameTime, _spriteBatch);
        }
    }
}
