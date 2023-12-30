using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.JewelJam.Objects;
using Microsoft.Xna.Framework;

namespace LearningCSharpByProgrammingGames.JewelJam
{
    public class JewelJamGame : ExtendedGame
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
        /// The position of the top-left corner of the grid in the game world.
        /// </summary>
        Vector2 GridOffSet = new Vector2(85, 150);             
        public JewelJamGame() : base()
        {
            IsMouseVisible = true;
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            // add the background
            SpriteGameObject background = new("spr_background");
            _gameWorld.AddChild(background);

            // add the grid
            JewelGrid jewelGrid = new(GridWidth, GridHeight, CellSize, GridOffSet);
            _gameWorld.AddChild(jewelGrid);
            
            // set the world size to the width and height of the background sprite
            _worldSize = new Point(background.Width, background.Height);

            // to let the new world size take effect, we need to set the FullScreen property again
            FullScreen = false;
        }
        
    }
}
