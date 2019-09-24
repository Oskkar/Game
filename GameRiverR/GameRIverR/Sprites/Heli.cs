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
    class Heli
    {
        Texture2D texture;
        Rectangle rectangle;
        public Rectangle rectanglePos;
        public Vector2 position;
        Vector2 origin;
        public Vector2 velocity = new Vector2(3,5);
        
        int currentFrame;
        int frameHeight;
        int frameWidth;

        float timer;
        float interval = 75;

        public Heli(Texture2D newTexture, Vector2 newPosition, int newFrameWidth, int newFrameHeight)
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
            origin = new Vector2(0, 0);

            if (velocity.X < 0) 
            {
                AnimateLeft(gameTime);
                if (rectanglePos.Intersects(recLeftSide) || rectanglePos.Intersects(recB2)
                    || rectanglePos.Intersects(recB5))
                {
                    velocity.X = -1 * velocity.X;
                }
            }
            else
            {
                AnimateRight(gameTime);
                if (rectanglePos.Intersects(recRightSide) || rectanglePos.Intersects(recB3)
                    || rectanglePos.Intersects(recB4))
                {
                    velocity.X = -1 * velocity.X;
                }
            }
        }
        public void AnimateRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 1)
                    currentFrame = 0;
            }
        }
        public void AnimateLeft(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 3)
                    currentFrame = 2;
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