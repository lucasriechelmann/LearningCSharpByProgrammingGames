using LearningCSharpByProgrammingGames.JewelJam.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LearningCSharpByProgrammingGames.JewelJam
{
    public class JewelJamGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D _background;
        Texture2D _jewelTest;
        Point _worldSize, _windowSize;
        Matrix _spriteScale;
        InputHelper _inputHelper;
        public JewelJamGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        bool FullScreen
        {
            get  => _graphics.IsFullScreen;
            set  => ApplyResolutionSettings(value);
        }
        protected override void Initialize()
        {
            _inputHelper = new InputHelper();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("spr_background");
            _jewelTest = Content.Load<Texture2D>("spr_single_jewel1");
            _worldSize = new Point(_background.Width, _background.Height);
            _windowSize = new Point(1024, 768);
            FullScreen = false;
        }
        void ApplyResolutionSettings(bool fullScreen)
        {
            Point screenSize;
            if (fullScreen)
                screenSize = new Point(
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            else
                screenSize = _windowSize;

            _graphics.IsFullScreen = fullScreen;
            _graphics.PreferredBackBufferWidth = screenSize.X;
            _graphics.PreferredBackBufferHeight = screenSize.Y;
            _graphics.ApplyChanges();
            GraphicsDevice.Viewport = CalculateViewport(screenSize);
            
            _spriteScale = Matrix.CreateScale(
                (float)GraphicsDevice.Viewport.Width / _worldSize.X,
                (float)GraphicsDevice.Viewport.Height / _worldSize.Y,
                1f);
        }
        Viewport CalculateViewport(Point windowSize)
        {
            Viewport viewport = new();

            float gameAspectRatio = _worldSize.X / (float)_worldSize.Y;
            float windowAspectRatio = windowSize.X / (float)windowSize.Y;

            //if the window is relatively wide, use the full window height
            if (windowAspectRatio > gameAspectRatio)
            {
                viewport.Width = (int)(windowSize.Y * gameAspectRatio);
                viewport.Height = windowSize.Y;
            }
            //if the window is relatively high, use the full window width
            else
            {
                viewport.Width = windowSize.X;
                viewport.Height = (int)(windowSize.X / gameAspectRatio);
            }

            viewport.X = (windowSize.X - viewport.Width) / 2;
            viewport.Y = (windowSize.Y - viewport.Height) / 2;

            return viewport;
        }
        protected override void Update(GameTime gameTime)
        {
            if (_inputHelper.KeyPressed(Keys.Escape))
                Exit();

            _inputHelper.Update();
            if (_inputHelper.KeyPressed(Keys.F5))
                FullScreen = !FullScreen;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _spriteScale);
            _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            //_spriteBatch.Draw(_jewelTest, _inputHelper.MousePosition, Color.White);
            //_spriteBatch.Draw(_jewelTest, ScreenToWorld(_inputHelper.MousePosition), Color.White);
            _spriteBatch.End();
        }
        Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            Vector2 viewportTopLeft = new(
                GraphicsDevice.Viewport.X,
                GraphicsDevice.Viewport.Y);
            float screenToWorldScale = _worldSize.X / (float)GraphicsDevice.Viewport.Width;
            return (screenPosition - viewportTopLeft) * screenToWorldScale;
        }
    }
}
