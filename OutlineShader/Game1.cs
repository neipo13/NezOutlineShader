using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace OutlineShader
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {
        public const int TileSize = 16;
        public const int TilesWide = 16;
        public const int TilesHigh = 9;

        public static int designWidth => TilesWide * TileSize;
        public static int designHeight => TilesHigh * TileSize;


        public Game1() : base(256 * 4, 144 * 4, windowTitle: "Outline Shader")
        {
            Scene.setDefaultDesignResolution(designWidth, designHeight, Scene.SceneResolutionPolicy.BestFit, 0, 0);
            Window.AllowUserResizing = true;
            Window.Position = new Point(0, 0);
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            scene = Scene.createWithDefaultRenderer();
            base.Update(new GameTime());
            base.Draw(new GameTime());
            scene = new GameScene();
        }
    }
}
