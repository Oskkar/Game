using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RiverRaid2.Scenes;

namespace RiverRaid2
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MenuScene menuScene;
        GameScene gameScene;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        protected override void Initialize()
        {
            menuScene = new MenuScene(this);
            gameScene = new GameScene(this);

            Menu.IsShowMainMenuScene = true;
            Menu.IsShowGameScene = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if (Menu.IsShowMainMenuScene)
                menuScene.Update(gameTime);
            if (Menu.IsShowGameScene)
                gameScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (Menu.IsShowMainMenuScene)
                menuScene.Draw(spriteBatch, gameTime);
            if (Menu.IsShowGameScene)
                gameScene.Draw(spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
