using GamePong.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePong.Sprites
{
    class Przod : Sprite
    {


        public Przod(Texture2D texture)
          : base(texture)
        {

        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;
                if (this.IsTouchingTop(sprite) && Score.pomoc % 2 == 0)
                    Score.score1++;
                else if (this.IsTouchingTop(sprite) && Score.pomoc % 2 == 1)
                    Score.score2++;
            }
        }
    }
}
