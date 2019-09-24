using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameArkanoid.Objects
{
    public class Block : Component
    {
        #region Fields
        private Texture2D _texture;
        private int _state;
        private Color _color;
        #endregion

        #region Properties
        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        public Rectangle Top
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, 0);
            }
        }
        public Rectangle Bottom
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y + _texture.Height, _texture.Width, 0);
            }
        }
        public Rectangle Left
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 0, _texture.Height);
            }
        }
        public Rectangle Right
        {
            get
            {
                return new Rectangle((int)Position.X + _texture.Width, (int)Position.Y, 0, _texture.Height);
            }
        }

        /* Possible states:
         1 - After collision block is destroyed
         2 - After collision block change state to 1
         3 - After collision block change state to 2
         4 - Block is indestructable and doesn't need to be destroyed to win level
         */
        public int State { get => _state; set => _state = value; }
        #endregion

        #region Constructors
        public Block(Texture2D texture)
        {
            _texture = texture;
            _state = 1;
        }

        public Block(Texture2D texture, int state)
        {
            _texture = texture;
            _state = state;
        }
        #endregion

        #region Methods
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _color = Color.Gray;

            if (State == 4)
                _color = Color.Black;
            else if (State == 3)
                _color = Color.DarkRed;
            else if (State == 2)
                _color = Color.DarkOrange;
            else if (State == 1)
                _color = Color.Yellow;

            spriteBatch.Draw(_texture, Rectangle, _color);
        }

        public override void Update(GameTime gameTime)
        {
            if (State == 4)
                _color = Color.Black;
            else if (State == 3)
                _color = Color.DarkRed;
            else if (State == 2)
                _color = Color.DarkOrange;
            else if (State == 1)
                _color = Color.Yellow;
        }

        #endregion
    }
}