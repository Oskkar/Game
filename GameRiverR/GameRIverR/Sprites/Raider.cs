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

using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace RiverRaid2.Elements
{
    class Raider
    {
        Texture2D texture;
        Rectangle rectangle;
        public Rectangle rectanglePos;
        public Vector2 position;
        Vector2 origin;

        float speed = 200;
        int currentFrame = 0;
        int frameHeight;
        int frameWidth;

        public Raider(Texture2D newTexture, Vector2 newPosition, int newFrameWidth, int newFrameHeight)
        {
            texture = newTexture;
            position = newPosition;
            frameWidth = newFrameWidth;
            frameHeight = newFrameHeight;
        }
        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateMovement(deltaTime);

            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            rectanglePos = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
            //origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            origin = new Vector2(0, 0);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        private void UpdateMovement(float deltaTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            if (touchCollection.Count > 0)
            {
                if (touchCollection[0].State == TouchLocationState.Moved || touchCollection[0].State == TouchLocationState.Pressed)
                {
                    if (touchCollection[0].Position.X > 400 && touchCollection[0].Position.Y < 1100)    //prawo
                    {
                        currentFrame = 1;
                        position.X += speed * deltaTime;
                        
                    }
                    else if (touchCollection[0].Position.X < 400 && touchCollection[0].Position.Y < 1100)   //lewo
                    {
                        currentFrame = 2;
                        position.X -= speed * deltaTime;
                    }
                }
            }
            else
            {
                currentFrame = 0;
            }
        }
 
    }
}