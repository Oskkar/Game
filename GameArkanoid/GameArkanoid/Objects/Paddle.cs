using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameArkanoid.Objects
{
    public class Paddle : Component
    {
        #region Fields
        private Texture2D _currentTexture;
        private Vector2 _position;
        private float _speed;
        #endregion

        #region Properties 
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _currentTexture.Width, _currentTexture.Height);
            }
        }

        public Vector2 Position { get => _position; set => _position = value; }
        public float Speed { get => _speed; set => _speed = value; }
        #endregion

        #region Contructors
        public Paddle(Texture2D texture, Vector2 position)
        {
            _currentTexture = texture;
            Position = position;
            Speed = 0.5f;
        }
        #endregion

        #region Methods
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_currentTexture, Rectangle, Color.White);
        }

        private void PaddleMovement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                _position.X = Position.X - Speed;

            if (Position.X < 20)
                _position.X = 20;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _position.X = Position.X + Speed;

            if (Position.X > 540 - _currentTexture.Width)
                _position.X = 540 - _currentTexture.Width;
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: Changing paddle after picking up various bonuses
            PaddleMovement();

            if (_speed > 1)
                _speed = 1;
            else if (_speed < 0.3f)
                _speed = 0.3f;
        }

        #endregion
    }
}