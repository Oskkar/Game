using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace RiverRaid2.Elements
{
    class Jet
    {
        Texture2D texture;
        Rectangle rectangle;//, recLeftSide, recRightSide;
        public Rectangle rectanglePos;
        public Vector2 position;
        Vector2 origin;
        public Vector2 velocity = new Vector2(-4, 5);

        int currentFrame;
        int frameHeight;
        int frameWidth;

        public Jet(Texture2D newTexture, Vector2 newPosition, int newFrameWidth, int newFrameHeight)
        {
            texture = newTexture;
            position = newPosition;
            frameWidth = newFrameWidth;
            frameHeight = newFrameHeight;
        }

        public void Update(GameTime gameTime, Rectangle recLeftSide, Rectangle recRightSide, Rectangle recB2, Rectangle recB3, 
            Rectangle recB4, Rectangle recB5)
        {
            position = position + velocity;

            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            rectanglePos = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
            //origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            origin = new Vector2(0, 0);

            if (velocity.X < 0)     //w lewo
            {
                currentFrame = 1;
                if (rectanglePos.Intersects(recLeftSide) || rectanglePos.Intersects(recB2) 
                    || rectanglePos.Intersects(recB5))
                {
                    velocity.X = -1 * velocity.X;
                }
            }
            else   //w prawo
            {
                currentFrame = 0;
                if (rectanglePos.Intersects(recRightSide) || rectanglePos.Intersects(recB3)
                    || rectanglePos.Intersects(recB4))
                {
                    velocity.X = -1 * velocity.X;
                }
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}