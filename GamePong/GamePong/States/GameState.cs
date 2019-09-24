using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamePong.Controls;
using GamePong.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GamePong.Models;
using Microsoft.Xna.Framework.Input;


namespace GamePong.States
{
    class GameState : State
    {

        SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        private List<Sprite> _sprites;

        private Score _score;

        private Ball _ball;

        int pomocnicza;

        public int PlayerCount;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(graphicsDevice);


            var batTexture = content.Load<Texture2D>("Controls/paddle");
            var batTexture2 = content.Load<Texture2D>("Controls/paddle2");
            var ballTexture = content.Load<Texture2D>("Controls/ball");
            var przod = content.Load<Texture2D>("Controls/przod");
            var bokLewy = content.Load<Texture2D>("Controls/bok");
            var bokPrawy = content.Load<Texture2D>("Controls/bok");

            _score = new Score(content.Load<SpriteFont>("Fonts/Font"));

            _sprites = new List<Sprite>()
      {

        new Bat(batTexture)
        {
          Position = new Vector2(300,430),
          Score = _score,
          Input = new Input()
          {
            Right = Keys.Right,
            Left = Keys.Left,
          }
        },
        new Ball(ballTexture)
        {
          Position = new Vector2(650,180),
          Score = _score,

        },
        new Przod(przod)
        {
            Position = new Vector2(200,50),
            Score = _score,
        },
        new BokPrawy(bokPrawy)
        {
            Position = new Vector2(180,50),
            Score = _score,
        },
        new BokLewy(bokLewy)
        {
            Position = new Vector2(600,50),
            Score = _score,
        },
         new BotBat(batTexture2)
                {
                    Position = new Vector2(300, 480),
                    Score = _score,
                },

      };
           
        }

        public override void LoadContent()
        {
           

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            for (int i = 0; i <5; i ++)
                _sprites[i].Draw(spriteBatch);

            if (PlayerCount == 2)
                _sprites[5].Draw(spriteBatch);


            _score.Draw(spriteBatch);


                spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
            }
            if (_sprites[1].Position.Y >= 400)
            {
                _game.Exit();
            }

            if (PlayerCount == 2)
            {


                if (_sprites[5].Position.X > _sprites[1].Position.X)
                {
                    _sprites[5].Velocity.X = -_sprites[5].Speed;
                }
                else
                    _sprites[5].Velocity.X = _sprites[5].Speed;
                /*
                            if (_score.pomoc % 2 == 0)
                            {
                                _sprites[1].Position.Y += 10;
                            }
                            */
                if (_score.pomoc == 1 && _sprites[1].Velocity.Y < 0)
                {
                    _sprites[0].Position.Y = 480;
                    _sprites[5].Position.Y = 430;
                }

                if (_score.pomoc == 0 && _sprites[1].Velocity.Y < 0)
                {
                    _sprites[0].Position.Y = 430;
                    _sprites[5].Position.Y = 480;
                }

            }




            // base.Update(gameTime);
        }

    }
}
