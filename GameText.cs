using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTetris
{
    class GameText : IGameObject
    {
        public static SpriteFont Font;
        public bool IsConflict { get; set; }
        private string Text;
        public GameText()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsConflict)
            {
                Text = "Conflict = TRUE";
            }
            else
            {
                Text = "Conflict = FALSE";
            }
            spriteBatch.DrawString(Font, Text, new Vector2(192, 128), Color.White);
            spriteBatch.DrawString(Font, "Next:", new Vector2(192, 32), Color.White);
        }
    }
}
