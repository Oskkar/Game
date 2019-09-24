using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameArkanoid.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameArkanoid.States
{
    public class Gameover : State
    {
        #region Fields
        private List<Component> _components;
        private SpriteFont _font;
        private int _score;

        private float _fontOpacity = 0;
        private float _fontOpacityFlag = 0;
        #endregion

        #region Contructors
        public Gameover(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, int score)
            : base(game, graphicsDevice, content)
        {
            _score = score;
            _font = _content.Load<SpriteFont>("Fonts/Font");

            _fontOpacity = 0;
            _fontOpacityFlag = 0;

            var backgroundTexture = _content.Load<Texture2D>("Backgrounds/Menu");
            Background background = new Background(backgroundTexture);

            _components = new List<Component>()
            {
                background,
            };
        }
        #endregion

        #region Methods
        private void CheckIfKeyPressed()
        {
            var keys = Keyboard.GetState().GetPressedKeys();

            if (keys.Length > 0)
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(_font, "GAME OVER", new Vector2(110, 220), Color.White * _fontOpacity, 0, new Vector2(0, 0), 3, SpriteEffects.None, 1);
            spriteBatch.DrawString(_font, "Press any key to continue", new Vector2(50, 300), Color.White * _fontOpacity, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 1);

            DrawScore(spriteBatch);

            spriteBatch.End();
        }

        private void DrawScore(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "Score", new Vector2(215, 400), Color.Red, 0, new Vector2(0, 0), 2, SpriteEffects.None, 1);

            if (_score > 9999)
            {
                spriteBatch.DrawString(_font, _score.ToString(), new Vector2(215, 450), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 1);
            }
            else if (_score > 999)
            {
                spriteBatch.DrawString(_font, "0" + _score.ToString(), new Vector2(215, 450), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 1);
            }
            else if (_score > 99)
            {
                spriteBatch.DrawString(_font, "00" + _score.ToString(), new Vector2(215, 450), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 1);
            }
            else if (_score > 9)
            {
                spriteBatch.DrawString(_font, "000" + _score.ToString(), new Vector2(215, 450), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.DrawString(_font, "0000" + _score.ToString(), new Vector2(215, 450), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 1);
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        private void StringFlashing()
        {
            if (_fontOpacityFlag >= 60)
            {
                if (_fontOpacity == 1)
                    _fontOpacity = 0;
                else
                    _fontOpacity = 1;

                _fontOpacityFlag = 0;
            }
            _fontOpacityFlag++;
        }

        public override void Update(GameTime gameTime)
        {
            StringFlashing();
            CheckIfKeyPressed();
        }

        #endregion
    }
}