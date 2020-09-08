using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZTetris
{
    public class Block : IGameEntity
    {
        public static Texture2D Texture;

        private Coordinate coordinates;
        public Coordinate Coordinates
        {
            get
            {
                return coordinates;
            }
            set
            {
                coordinates = value;
                position = new Vector2(16 * coordinates.X + 16, 16 * coordinates.Y);
            }
        }


        private Vector2 position;
        public Color Color;

        //Constructors
        public Block(int x, int y)
        {
            Coordinates = new Coordinate(x, y);
            //Position = new Vector2(16 * Coordinates.X + 16, 16 * Coordinates.Y);
            Color = Color.White;
        }
        public Block(int x, int y, Color color)
        {
            Coordinates = new Coordinate(x, y);
            //Position = new Vector2(16 * Coordinates.X + 16, 16 * Coordinates.Y);
            Color = color;
        }
        //End Constructors

        //Operator Overloads
        //public static bool operator ==(Block op1, Block op2)
        //{
        //    return op1.Equals(op2);
        //}
        //public static bool operator !=(Block op1, Block op2)
        //{
        //    return !op1.Equals(op2);
        //}
        //TODO: Override 'Object.Equals' and 'Object.GetHashCode' in Block struct
        //End Operator Overloads

        //Interface Methods
        public void Update(GameTime gameTime)
        {
            //Position = new Vector2(16 * Coordinates.X + 16, 16 * Coordinates.Y);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position: position, color: Color);
        }
        //End Interface Methods
    }
}
