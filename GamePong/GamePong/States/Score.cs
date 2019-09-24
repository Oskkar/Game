using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePong.States
{
    public class Score
    {
        public int score1;

        public int score2;

        public int pomoc = 1;

        private SpriteFont _font;

        public int PlayerCount;

        public Score(SpriteFont font)
        {
            _font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (score1 > 0)
                spriteBatch.DrawString(_font, "Computer:  " + score1.ToString() + "pkt", new Vector2(10, 70), Color.White);
                spriteBatch.DrawString(_font, "Gracz :  " + score2.ToString() + "pkt", new Vector2(700, 70), Color.White);
            
        }
    }
}