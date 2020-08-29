using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ZTetris
{
    public class Block : IGameObject
    {
        public static Texture2D Texture;


        public int XCoordinate;
        public int YCoordinate;

        Vector2 Position;
        Color Color = Color.White;



        public Block(int x, int y)
        {
            XCoordinate = x;
            YCoordinate = y;
            Color = new Color(255, 255, 255);
        }

        public Block(int x, int y, Color color)
        {
            XCoordinate = x;
            YCoordinate = y;
            Color = color;
        }

        //public void ChangeLocation(int x, int y)
        //{
        //    XCoordinate = x;
        //    YCoordinate = y;
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            Position = new Vector2(16 * XCoordinate + 16, 16 * YCoordinate);
            spriteBatch.Draw(Texture, position: Position, color: Color);
        }

    }
}
