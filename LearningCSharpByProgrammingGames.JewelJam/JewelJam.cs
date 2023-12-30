using LearningCSharpByProgrammingGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LearningCSharpByProgrammingGames.JewelJam
{
    public class JewelJamGame : ExtendedGame
    {
        int[,] _grid;
        const int GridWidth = 5;
        const int GridHeight = 10;
        const int CellSize = 85;
        Vector2 _gridOffSet = new Vector2(85, 150);
        Texture2D[] _jewels;
        
        public JewelJamGame() : base()
        {
            IsMouseVisible = true;
            _grid = new int[GridWidth, GridHeight];
            for (int x = 0; x < GridWidth; x++)
                for (int y = 0; y < GridHeight; y++)
                    _grid[x, y] = Random.Next(3);
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_inputHelper.KeyPressed(Keys.Space))
                MoveRowsDown();
        }
        void MoveRowsDown()
        {
            for (int y = GridHeight - 1; y > 0; y--)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    _grid[x, y] = _grid[x, y - 1];
                }
            }

            // refill the top row
            for (int x = 0; x < GridWidth; x++)
            {
                _grid[x, 0] = Random.Next(3);
            }
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            SpriteGameObject background = new("spr_background");
            _gameWorld.Add(background);            
            _worldSize = new Point(background.Width, background.Height);
            _windowSize = new Point(1024, 768);
            _jewels = new Texture2D[3];
            _jewels[0] = Content.Load<Texture2D>("spr_single_jewel1");
            _jewels[1] = Content.Load<Texture2D>("spr_single_jewel2");
            _jewels[2] = Content.Load<Texture2D>("spr_single_jewel3");
            FullScreen = false;
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _spriteScale);
            _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    Vector2 position = _gridOffSet + new Vector2(x, y) * CellSize;
                    _spriteBatch.Draw(_jewels[_grid[x, y]], position, Color.White);
                }
            }

            _spriteBatch.End();
        }
    }
}
