using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GamePong.Controls;

namespace GamePong.States
{
    class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/przycisk");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Game with Computer",
            };

            newGameButton.Click += NewGameButton_Click;

            var loadGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Solo Game",
            };

            loadGameButton.Click += LoadGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Quit",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
                newGameButton,
                loadGameButton,
                quitGameButton,
            };
        }


        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content)
            {
                PlayerCount = 2,
            });

        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content)
            {
                PlayerCount = 1,
            });
        }
        
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // Remowe sprites if they are not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void LoadContent()
        {
            throw new NotImplementedException();
        }
    }
}
