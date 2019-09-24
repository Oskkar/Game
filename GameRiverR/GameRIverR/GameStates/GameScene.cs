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
using RiverRaid2.Elements;

namespace RiverRaid2.Scenes
{
    class GameScene : DrawableGameComponent
    {
        public Game game;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Jet jet;
        Ship ship;
        Heli heli;
        Raider raider;
        Background background, backgroundA, backgroundB, backgroundC, backgroundD, backgroundE;
        Vector2 positionStart;
        Rectangle recScreen, recLeftSide, recRightSide, recStart, recB2, recB3, recB4, recB5, recShot;

        Texture2D fire, shot;
        Boolean fired = false;
        public int backgroundSpeed = 5;
        int actual1 = 1;
        int actual2;
        public int score = 0;

        public GameScene(Game game) : base(game)
        {
            this.game = game;
            LoadContent();
            Initialize();
        }
        public override void Initialize()
        {
            recScreen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            positionStart = new Vector2(0, -1 * (recScreen.Height));
            recStart = new Rectangle((int)positionStart.X, (int)positionStart.Y , recScreen.Width, recScreen.Height);

            recLeftSide = new Rectangle(0, -1500, recScreen.Width / 4, 3000);
            recRightSide = new Rectangle(recScreen.Width - (recScreen.Width / 4), -1500, recScreen.Width / 4, 3000);
            recB2 = new Rectangle(0, (int)positionStart.Y, recScreen.Width / 2, recScreen.Height / 2);
            recB3 = new Rectangle(recScreen.Width / 2, (int)positionStart.Y, recScreen.Width / 2, recScreen.Height / 2);
            recB4 = new Rectangle(recScreen.Width / 2, (int)positionStart.Y / 2, recScreen.Width / 2, recScreen.Height / 2);
            recB5 = new Rectangle(0, (int)positionStart.Y / 2, recScreen.Width / 2, recScreen.Height / 2);

            jet = new Jet(Game.Content.Load<Texture2D>("gameplay/jet1"), new Vector2(recScreen.Width / 2, 0), 65, 36);
            ship = new Ship(Game.Content.Load<Texture2D>("gameplay/ship1"), new Vector2(recScreen.Width / 2, 400), 99, 35);
            heli = new Heli(Game.Content.Load<Texture2D>("gameplay/heli"), new Vector2(recScreen.Width / 2, 800), 65, 37);
            raider = new Raider(Game.Content.Load<Texture2D>("gameplay/raider1"), new Vector2((recScreen.Width / 2)-100, recScreen.Height / 2+400), 50, 53);

            background = new Background(Game.Content.Load<Texture2D>("gameplay/background"), recScreen);
            backgroundA = new Background(Game.Content.Load<Texture2D>("gameplay/background1"), new Rectangle(0,0, recScreen.Width, recScreen.Height));
            backgroundB = new Background(Game.Content.Load<Texture2D>("gameplay/background2"), recStart);
            backgroundC = new Background(Game.Content.Load<Texture2D>("gameplay/background3"), recStart);
            backgroundD = new Background(Game.Content.Load<Texture2D>("gameplay/background4"), recStart);
            backgroundE = new Background(Game.Content.Load<Texture2D>("gameplay/background5"), recStart);
        }
        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("label");
            fire = Game.Content.Load<Texture2D>("gameplay/fire");
            shot = Game.Content.Load<Texture2D>("gameplay/shot");
        }
        public override void Update(GameTime gameTime)
        {
            Fire();
            jet.Update(gameTime, recLeftSide, recRightSide, recB2, recB3, recB4, recB5);
            ship.Update(gameTime, recLeftSide, recRightSide, recB2, recB3, recB4, recB5);
            heli.Update(gameTime, recLeftSide, recRightSide, recB2, recB3, recB4, recB5);
            raider.Update(gameTime);

            if (fired)
            {
                recShot.Y -= 10;
                if(recShot.Y < -3)
                {
                    recShot = new Rectangle(raider.rectanglePos.X + raider.rectanglePos.Width / 2, raider.rectanglePos.Y +5, 5, 25); 
                    fired = false;
                }
                if(recShot.Intersects(heli.rectanglePos))
                {
                    heli.position.Y = 0;
                    recShot = new Rectangle(raider.rectanglePos.X + raider.rectanglePos.Width / 2, raider.rectanglePos.Y+5, 5, 25);
                    score += 2;
                    fired = false;
                }
                if (recShot.Intersects(jet.rectanglePos))
                {
                    jet.position.Y = 0;
                    recShot = new Rectangle(raider.rectanglePos.X + raider.rectanglePos.Width / 2, raider.rectanglePos.Y+5, 5, 25);
                    score += 3;
                    fired = false;
                }
                if (recShot.Intersects(ship.rectanglePos))
                {
                    ship.position.Y = 0;
                    recShot = new Rectangle(raider.rectanglePos.X + raider.rectanglePos.Width/2, raider.rectanglePos.Y+5, 5, 25);
                    score += 1;
                    fired = false;
                }
            }
            else
            {
                recShot = new Rectangle(raider.rectanglePos.X + raider.rectanglePos.Width / 2, raider.rectanglePos.Y + 5, 5, 25);  //pod raiderem
            }

            ElementsColision();
            EnemiesLoop();
            Background1Move();
            Background2Move();
            base.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            background.Draw(spriteBatch);
            backgroundA.Draw(spriteBatch);
            backgroundC.Draw(spriteBatch);
            backgroundD.Draw(spriteBatch);
            backgroundE.Draw(spriteBatch);
            backgroundB.Draw(spriteBatch);

            spriteBatch.Begin(); 
            spriteBatch.Draw(shot, recShot, Color.White);
            spriteBatch.End();

            heli.Draw(spriteBatch);
            jet.Draw(spriteBatch);
            ship.Draw(spriteBatch);       
            raider.Draw(spriteBatch);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(30, recScreen.Height - 60), Color.Red);
            spriteBatch.Draw(fire, new Rectangle((recScreen.Width - 110), (recScreen.Height - 100), 85, 85), Color.White);
            //spriteBatch.DrawString(font, "Actual1: " + actual1, new Vector2(30, recScreen.Height - 80), Color.Yellow);
            //spriteBatch.DrawString(font, "Actual2: " + actual2, new Vector2(30, recScreen.Height - 50), Color.Yellow);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        //metody
        public void Fire()
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            if (touchCollection.Count > 0)
            {
                if (touchCollection[0].Position.X > (recScreen.Width - 160) && touchCollection[0].Position.Y > recScreen.Height - 150)
                {
                    fired = true;
                }
            }
        }
        private void Background1Move()
        {
            switch (actual1)        //pierwsza czesc generowanej mapy
            {
                case 1: //mozliwe 2,3,4,5
                    CheckColision1();
                    backgroundA.rectangle.Y += backgroundSpeed;
                    if (backgroundA.rectangle.Y >= 0 && actual2 ==0) //jezeli tlo przyslania ekran
                    {  
                        System.Random x = new Random(System.DateTime.Now.Millisecond);
                        actual2 = x.Next(2, 6);         //losowanie kolejnego elemntu mapy
                    }
                    if (backgroundA.rectangle.Y >= recScreen.Height)  //jezeli tlo wychodzi poza ekran
                    {
                        backgroundA.rectangle.Y = (int)positionStart.Y;
                        actual1 = 0;
                    }
                    break;
                case 2: //1,3,5
                    backgroundB.rectangle.Y += backgroundSpeed;
                    recB2.Y += backgroundSpeed;
                    if (backgroundB.rectangle.Y >= 0 && actual2 == 0)
                    {
                        System.Random x = new Random(System.DateTime.Now.Millisecond);
                        int r = x.Next(1, 4);
                        if (r == 1)
                            actual2 = 1;
                        else if (r == 2)
                            actual2 = 3;
                        else
                            actual2 = 5;
                    }
                    if (backgroundB.rectangle.Y >= recScreen.Height)
                    {
                        backgroundB.rectangle.Y = (int)positionStart.Y;
                        recB2.Y = backgroundB.rectangle.Y;
                        actual1 = 0;
                    }
                    CheckColision2();
                    break;
                case 3: //1,2,4
                    recB3.Y += backgroundSpeed;
                    backgroundC.rectangle.Y += backgroundSpeed;
                    if (backgroundC.rectangle.Y >= 0 && actual2 == 0)
                    {
                        System.Random x = new Random(System.DateTime.Now.Millisecond);
                        int r = x.Next(1, 4);
                        if (r == 1)
                            actual2 = 1;
                        else if (r == 2)
                            actual2 = 2;
                        else
                            actual2 = 4;
                    }
                    if (backgroundC.rectangle.Y >= recScreen.Height)
                    {
                        backgroundC.rectangle.Y = (int)positionStart.Y;
                        recB3.Y = backgroundC.rectangle.Y;
                        actual1 = 0;
                    }
                    CheckColision3();
                    break;
                case 4: //1,2,3,5
                    recB4.Y += backgroundSpeed;
                    backgroundD.rectangle.Y += backgroundSpeed;
                    if (backgroundD.rectangle.Y >= 0 && actual2 == 0)
                    {
                        System.Random x = new Random(System.DateTime.Now.Millisecond);
                        int r = x.Next(1, 5);
                        if (r == 1)
                            actual2 = 1;
                        else if (r == 2)
                            actual2 = 2;
                        else if (r == 3)
                            actual2 = 3;
                        else
                            actual2 = 5;
                    }
                    if (backgroundD.rectangle.Y >= recScreen.Height)
                    {
                        backgroundD.rectangle.Y = (int)positionStart.Y;
                        recB4.Y = (int)positionStart.Y / 2;

                        actual1 = 0;
                    }
                    CheckColision4();
                    break;
                case 5: //1,2,3,4
                    recB5.Y += backgroundSpeed;
                    backgroundE.rectangle.Y += backgroundSpeed;
                    if (backgroundE.rectangle.Y >= 0 && actual2 == 0)
                    {
                        System.Random x = new Random(System.DateTime.Now.Millisecond);
                        actual2 = x.Next(1, 5);
                    }
                    if (backgroundE.rectangle.Y >= recScreen.Height)
                    {
                        backgroundE.rectangle.Y = (int)positionStart.Y;
                        recB5.Y = (int)positionStart.Y / 2;

                        actual1 = 0;
                    }
                    CheckColision5();
                    break;
                default:
                    break;
            }
        }
        private void Background2Move()
        {
            System.Random x = new Random(System.DateTime.Now.Millisecond);

            switch (actual2)        //druga czesc generowanej mapy
            {
                case 1:
                    CheckColision1();
                    backgroundA.rectangle.Y += backgroundSpeed;
                    if (backgroundA.rectangle.Y >= 0 && actual1 == 0)
                    {
                        actual1 = x.Next(2, 6);
                    }
                    if (backgroundA.rectangle.Y >= recScreen.Height)
                    {
                        backgroundA.rectangle.Y = (int)positionStart.Y;
                        actual2 = 0;
                    }
                    break;
                case 2:
                    recB2.Y += backgroundSpeed;
                    backgroundB.rectangle.Y += backgroundSpeed;
                    if (backgroundB.rectangle.Y >= 0 && actual1 == 0)
                    {
                        int r = x.Next(1, 4);
                        if (r == 1)
                            actual1 = 1;
                        else if (r == 2)
                            actual1 = 3;
                        else
                            actual1 = 5;
                    }
                    if (backgroundB.rectangle.Y >= recScreen.Height)
                    {
                        backgroundB.rectangle.Y = (int)positionStart.Y;

                        recB2.Y = backgroundB.rectangle.Y;
                        actual2 = 0;
                    }
                    CheckColision2();
                    break;
                case 3:
                    recB3.Y += backgroundSpeed;
                    backgroundC.rectangle.Y += backgroundSpeed;
                    if (backgroundC.rectangle.Y >= 0 && actual1 == 0)
                    {
                        int r = x.Next(1, 4);
                        if (r == 1)
                            actual1 = 1;
                        else if (r == 2)
                            actual1 = 2;
                        else
                            actual1 = 4;
                    }
                    if (backgroundC.rectangle.Y >= recScreen.Height)
                    {
                        backgroundC.rectangle.Y = (int)positionStart.Y;
                        recB3.Y = backgroundC.rectangle.Y;
                        actual2 = 0;
                    }
                    CheckColision3();
                    break;
                case 4:
                    recB4.Y += backgroundSpeed;
                    backgroundD.rectangle.Y += backgroundSpeed;
                    if (backgroundD.rectangle.Y >= 0 && actual1 == 0)
                    {
                        int r = x.Next(1, 5);
                        if (r == 1)
                            actual1 = 1;
                        else if (r == 2)
                            actual1 = 2;
                        else if (r == 3)
                            actual1 = 3;
                        else
                            actual1 = 5;
                    }
                    if (backgroundD.rectangle.Y >= recScreen.Height)
                    {
                        backgroundD.rectangle.Y = (int)positionStart.Y;
                        recB4.Y = (int)positionStart.Y / 2;

                        actual2 = 0;
                    }

                    CheckColision4();
                    break;
                case 5:
                    recB5.Y += backgroundSpeed;
                    backgroundE.rectangle.Y += backgroundSpeed;
                    if (backgroundE.rectangle.Y >= 0 && actual1 == 0)
                    {
                        actual1 = x.Next(1, 5);
                    }
                    if (backgroundE.rectangle.Y >= recScreen.Height)
                    {
                        backgroundE.rectangle.Y = (int)positionStart.Y;
                        recB5.Y = (int)positionStart.Y / 2;

                        actual2 = 0;
                    }
                    CheckColision5();
                    break;
                default:
                    break;
            }
        }

        private void CheckColision1()
        {
            //Raider
            if (raider.rectanglePos.Intersects(recLeftSide) || raider.rectanglePos.Intersects(recRightSide))
            {
                raider.position.X = recScreen.Width / 2;
                score = 0;
            }
        }
        private void CheckColision2()
        {
            //Raider
            if (raider.rectanglePos.Intersects(recLeftSide) || raider.rectanglePos.Intersects(recRightSide)
                || raider.rectanglePos.Intersects(recB2))
            {
                raider.position.X = recScreen.Width / 2 + 50;//+ (raider.Width / 2);
                score = 0;
            }
        }
        private void CheckColision3()
        {
            //Raider
            if (raider.rectanglePos.Intersects(recLeftSide) || raider.rectanglePos.Intersects(recRightSide)
            || raider.rectanglePos.Intersects(recB3))
            {
                raider.position.X = recScreen.Width / 2 - 100;//- raider.Width * 2;
                score = 0;
            }
        }
        private void CheckColision4()
        {
            //Raider
            if (raider.rectanglePos.Intersects(recLeftSide) || raider.rectanglePos.Intersects(recRightSide)
                || raider.rectanglePos.Intersects(recB4))
            {
                raider.position.X = recScreen.Width / 2 - 100;//- raider.Width * 2;
                score = 0;
            }
        }
        private void CheckColision5()
        {
            //Raider
            if (raider.rectanglePos.Intersects(recLeftSide) || raider.rectanglePos.Intersects(recRightSide)
                || raider.rectanglePos.Intersects(recB5))
            {
                raider.position.X = recScreen.Width / 2 + 50;//+ (raider.Width / 2);
                score = 0;
            }
        }
        private void EnemiesLoop()
        {
            if (heli.rectanglePos.Y > recScreen.Height)
            {
                heli.position.Y = 0;
                if (heli.rectanglePos.Intersects(recB2) || heli.rectanglePos.Intersects(recB5))
                    heli.position.X = recScreen.Width / 2 + heli.rectanglePos.Width + 50;
                if (heli.rectanglePos.Intersects(recB3) || heli.rectanglePos.Intersects(recB4))
                    heli.position.X = recScreen.Width / 2 - heli.rectanglePos.Width - 50;
            }
            if (jet.rectanglePos.Y > recScreen.Height)
            {
                jet.position.Y = 0;
                if (jet.rectanglePos.Intersects(recB2) || jet.rectanglePos.Intersects(recB5))
                    jet.position.X = recScreen.Width / 2 + jet.rectanglePos.Width + 50;
                if (jet.rectanglePos.Intersects(recB3) || jet.rectanglePos.Intersects(recB4))
                    jet.position.X = recScreen.Width / 2 - jet.rectanglePos.Width - 50; ;
            }
            if (ship.rectanglePos.Y > recScreen.Height)
            {
                ship.position.Y = 0;
                if (ship.rectanglePos.Intersects(recB2) || ship.rectanglePos.Intersects(recB5))
                    ship.position.X = recScreen.Width / 2 + ship.rectanglePos.Width + 50; 
                if (ship.rectanglePos.Intersects(recB3) || ship.rectanglePos.Intersects(recB4))
                    ship.position.X = recScreen.Width / 2 - ship.rectanglePos.Width - 50; ;
            }
        }
        void ElementsColision()
        {
            if (raider.rectanglePos.Intersects(heli.rectanglePos))
            {
                heli.position.Y = 0;
                if (score != 0)
                    score -= 1;
            }
            if (raider.rectanglePos.Intersects(jet.rectanglePos))
            {
                jet.position.Y = 0;
                if (score != 0)
                    score -= 1;
            }
            if (raider.rectanglePos.Intersects(ship.rectanglePos))
            {
                ship.position.Y = 0;
                if(score != 0)
                    score -= 1;
            }
        }
    }
}