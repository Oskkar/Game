using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GamePong.States;

namespace GamePong.Sprites
{
    class Bat : Sprite
    {
        BotBat _BotBat;

        public Bat(Texture2D texture)
          : base(texture)
        {
            Speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            foreach(var sprite in sprites)
            {
                if (this.IsTouchingBottom(sprite))
                {
                    Score.pomoc =1;
                }
            }
            


            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                Velocity.X = -Speed;
                Position += Velocity;
            }

            else if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Velocity.X = Speed;
                Position += Velocity;
            }

            



        }
    }
}
