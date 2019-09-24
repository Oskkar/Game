using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RiverRaid2.Scenes
{
    class MenuScene : DrawableGameComponent
    {
        Game game;

        Texture2D menuBackground;
        Texture2D menuLogo;
        Texture2D buttonPlay;
        Texture2D buttonExit;

        Rectangle recMenuLogo;
        Rectangle recButtonPlay;
        Rectangle recButtonExit;

        public MenuScene(Game game) : base(game)
        {
            this.game = game;
            LoadContent();
        }
        protected override void LoadContent()
        {
            menuBackground = Game.Content.Load<Texture2D>("menu/menuBackground");
            menuLogo = Game.Content.Load<Texture2D>("menu/menuLogo");
            buttonPlay = Game.Content.Load<Texture2D>("menu/buttonPlay");
            buttonExit = Game.Content.Load<Texture2D>("menu/buttonExit");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(menuBackground, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.Draw(menuLogo, recMenuLogo, Color.White);
            spriteBatch.Draw(buttonPlay, recButtonPlay, Color.White);
            spriteBatch.Draw(buttonExit, recButtonExit, Color.White);
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            CalculateItemsPositions();
            CalculateItemsSize();
            ButtonsEvents();
        }
        private void CalculateItemsPositions()
        {
            recMenuLogo.X = GraphicsDevice.Viewport.Width / 2 - recMenuLogo.Width / 2;
            recMenuLogo.Y = 30;

            int srodekPrzycisk = GraphicsDevice.Viewport.Width / 2 - recButtonPlay.Width / 2;

            recButtonPlay.X = srodekPrzycisk;
            recButtonPlay.Y = recMenuLogo.Y + GraphicsDevice.Viewport.Height / 2;

            recButtonExit.X = srodekPrzycisk;
            recButtonExit.Y = recButtonPlay.Y + GraphicsDevice.Viewport.Height / 6;
        }
        private void CalculateItemsSize()
        {
            int Height = GraphicsDevice.Viewport.Height / 7;
            int Width = GraphicsDevice.Viewport.Width / 2 + 120;

            recMenuLogo.Height = GraphicsDevice.Viewport.Height / 3 - 60;
            recMenuLogo.Width = GraphicsDevice.Viewport.Width - 60;

            recButtonPlay.Height = Height;
            recButtonPlay.Width = Width;

            recButtonExit.Height = Height;
            recButtonExit.Width = Width;
        }
        private void ButtonsEvents()
        {
            int x = 0;
            int y = 0;
            bool isInputPressed = false;
            var touchPanelState = TouchPanel.GetState();

            if (touchPanelState.Count >= 1) //jezeli nastapilo dotkniecie ekranu
            {
                var touch = touchPanelState[0];
                x = (int)touch.Position.X;
                y = (int)touch.Position.Y;      //pobranie pozycji dotkniecia
                isInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;
            }

            if (isInputPressed && recButtonPlay.Contains(x, y))
            {
                Menu.IsShowMainMenuScene = false;
                Menu.IsShowGameScene = true;
            }
            if (isInputPressed && recButtonExit.Contains(x, y))
            {
                game.Exit();
            }
        }
    }
}