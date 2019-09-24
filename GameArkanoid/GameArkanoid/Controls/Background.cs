using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameArkanoid.Controls
{
    class Background : Component
    {
        #region Fields
        private Texture2D _texture;
        #endregion

        #region Constructors
        public Background(Texture2D texture)
        {
            _texture = texture;
        }
        #endregion

        #region Methods
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(0, 0), Color.White);
        }

        public override void Update(GameTime gameTime)
        {

        }

        #endregion
    }
}
