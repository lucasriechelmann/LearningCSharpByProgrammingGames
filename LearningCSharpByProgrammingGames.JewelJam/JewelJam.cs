using LearningCSharpByProgrammingGames.Engine;
using LearningCSharpByProgrammingGames.JewelJam.Objects;
using Microsoft.Xna.Framework;

namespace LearningCSharpByProgrammingGames.JewelJam
{
    public class JewelJamGame : ExtendedGame
    {
        
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

            // initialize the game world
            _gameWorld = new JewelJamGameWorld();

            // to re-scale the game world to the screen size, we need to set the FullScreen property again
            _worldSize = GameWorld.Size;
            FullScreen = false;
        }
        public static JewelJamGameWorld GameWorld => _gameWorld as JewelJamGameWorld;
    }
}
