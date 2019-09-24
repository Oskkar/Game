using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameArkanoid.Objects
{
    public class Bonuses : Component
    {
        #region Fields
        private bool _isGood;
        private Vector2 _position;
        private float _speed;
        private Type _state;
        private Texture2D _texture;
        #endregion

        #region Enums
        public enum Type
        {
            Life,
            BallSpeed,
            PaddleSpeed,
        }
        #endregion

        #region Properties
        public bool IsGood { get => _isGood; }

        public Vector2 Position { get => _position; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        /* Possible states:
         0 - OneUP
         1 - Paddle speed up or speed down
         2 - Ball speed up or speed down
         */
        public Type State { get => _state; set => _state = value; }
        #endregion

        #region Constructors
        public Bonuses(Texture2D texture, Type state, float speed, bool isGood, Vector2 position)
        {
            _isGood = isGood;
            _position = position;
            _state = state;
            _speed = speed / 100;
            _texture = texture;
        }
        #endregion

        #region Methods
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_isGood == true)
                spriteBatch.Draw(_texture, Rectangle, Color.Green);
            else
                spriteBatch.Draw(_texture, Rectangle, Color.Red);
        }

        public override void Update(GameTime gameTime)
        {
            _position.Y = _position.Y + _speed;
        }

        #endregion
    }
}
