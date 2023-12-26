using LearningCSharpByProgrammingGames.Painter.Managers;
using LearningCSharpByProgrammingGames.Painter.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace LearningCSharpByProgrammingGames.Painter
{
    public class Painter : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        InputHelper _inputHelper;
        GameWorld _gameWorld;
        public static Vector2 ScreenSize { get; set; }
        public static Random Random { get; private set; }
        public Painter()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _inputHelper = new InputHelper();
            _gameWorld = new GameWorld(Content);
            Random = new Random();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _inputHelper.Update();
            _gameWorld.HandleInput(_inputHelper);
            _gameWorld.Update(gameTime);

            base.Update(gameTime);
        }
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _gameWorld.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}
