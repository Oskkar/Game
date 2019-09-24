using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePong.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GamePong.Sprites
{
    public class Ball : Sprite
    {

        public int abc; 

        public Ball(Texture2D texture)
          : base(texture)
        {
            Speed = 4f;
            Velocity = new Vector2(1, -1);
        }


        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            Position += Velocity * Speed;


            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if (Velocity.X > 0 && this.IsTouchingLeft(sprite))
                    Velocity.X = -Velocity.X;
                if (Velocity.X < 0 && this.IsTouchingRight(sprite))
                    Velocity.X = -Velocity.X;
                if (Velocity.Y > 0 && this.IsTouchingTop(sprite))
                    Velocity.Y = -Velocity.Y;
                if (Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                    Velocity.Y = -Velocity.Y;


            }


            if (Position.Y <= 0)
                Velocity.Y = -Velocity.Y;

            if (Position.X <= 0)
            {
                Velocity.X = -Velocity.X;
            }

            if (Position.X + _texture.Width >= 800)
            {
                Velocity.X = -Velocity.X;
            }

        }
    }
}
