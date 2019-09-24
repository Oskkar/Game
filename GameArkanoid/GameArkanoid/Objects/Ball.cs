using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameArkanoid.Objects
{
    public class Ball : Component
    {
        #region Fields       
        private double _directionX;
        private double _directionY;
        private Rectangle _oldRectangle;
        private Vector2 _position;
        private double _speed;
        private int _speedCounter;
        private Texture2D _texture;
        bool _toDestroyFlag;
        #endregion

        #region Properties
        public double DirectionX { get => _directionX; set => _directionX = value; }

        public double DirectionY { get => _directionY; set => _directionY = value; }

        public Rectangle OldRectangle { get => _oldRectangle; }

        public Vector2 Position { get => _position; set => _position = value; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public double Speed { get => _speed; set => _speed = value; }

        public int SpeedCounter { get => _speedCounter; set => _speedCounter = value; }

        public bool ToDestroyFlag { get => _toDestroyFlag; }
        #endregion

        #region Constructors
        public Ball(Texture2D texture)
        {
            _texture = texture;
            DirectionX = Math.Sqrt(2);
            DirectionY = Math.Sqrt(2);
            Speed = 0.5f;
        }
        #endregion

        #region Methods
        private void CheckBallSpeedCounter()
        {
            if (_speedCounter >= 10)
            {
                _speed += 0.05f;
                _speedCounter = 0;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color.White);
        }

        private void EdgeCollisions()
        {
            if (Position.X < 20)
            {
                DirectionX = Math.Abs(DirectionX);
                _speedCounter++;
            }
            if (Position.X > 541 - _texture.Width)
            {
                DirectionX = Math.Abs(DirectionX) * -1;
                _speedCounter++;
            }
            if (Position.Y < 120)
            {
                DirectionY = Math.Abs(DirectionY);
                _speedCounter++;
            }
            if (Position.Y > 681 - _texture.Height)
            {
                _toDestroyFlag = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            _oldRectangle = Rectangle;

            if (_speed > 1.5f)
                _speed = 1.5f;
            else if (_speed < 0.4f)
                _speed = 0.4f;

            CheckBallSpeedCounter();

            Position = new Vector2((float)(Position.X + (DirectionX * _speed)), (float)(Position.Y + (DirectionY * _speed)));
            EdgeCollisions();
        }


        #endregion
    }
}
