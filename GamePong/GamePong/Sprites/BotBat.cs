using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePong.Sprites
{
    class BotBat : Sprite
    {

        public BotBat(Texture2D texture)
          : base(texture)
        {
            Speed = 3.8f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Position += Velocity;
            foreach (var sprite in sprites)
            {
                if (this.IsTouchingBottom(sprite) )
                {
                    Score.pomoc=0;
                }
            }     
        }
    }
}
