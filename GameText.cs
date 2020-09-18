using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTetris.Assets;

namespace ZTetris
{
    class GameText : IGameEntity
    {
        public static SpriteFont Font;

        public static string LinesCleared;
        public GameText()
        {

        }

        public void Update(GameTime gameTime)
        {
            //
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "Lines Cleared: " + LinesCleared, new Vector2(0, 368), Color.White);
            spriteBatch.DrawString(Font, "Next:", new Vector2(192, 32), Color.White);
            spriteBatch.DrawString(Font, "Hold:", new Vector2(-72, 32), Color.White);
        }
    }
}
