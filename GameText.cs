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
        public bool IsConflict { get; set; }
        public string ConflictText { get; set; }

        public static string LinesCleared;
        public GameText()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (IsConflict)
            {
                ConflictText = "Conflict = TRUE";
            }
            else
            {
                ConflictText = "Conflict = FALSE";
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "Lines Cleared: " + LinesCleared, new Vector2(192, 352), Color.White);
            spriteBatch.DrawString(Font, ConflictText, new Vector2(192, 128), Color.White);
            spriteBatch.DrawString(Font, "Next:", new Vector2(192, 32), Color.White);
            spriteBatch.DrawString(Font, 
                "Controls:\n" +
                "ARROW KEYS - move\n" +
                "A - rotate anticlockwise\n" +
                "D - rotate clockwise\n" +
                "S - add piece to board\n" +
                "Space - new tetromino\n"
                , new Vector2(-300, 32), Color.White);

        }
    }
}
