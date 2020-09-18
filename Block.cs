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

        public Coordinate Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value;
                position = new Vector2(Settings.GridSize * coordinates.X + Settings.GridSize, Settings.GridSize * coordinates.Y);
            }
        }
        private Coordinate coordinates;
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }
        private Vector2 position;

        public Color Color
        { 
            get => color;
            set => color = value;
        }
        private Color color = Color.White;

        //Constructors
        public Block(Coordinate coordinates, Color? color = null)
        {
            Coordinates = coordinates;
            Color = color ?? this.color;
        }
        //End Constructor


        public Block Clone() => (Block)this.MemberwiseClone();

        //Interface Methods
        public void Update(GameTime gameTime)
        {
            //
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position: position, color: Color);
        }
        //End Interface Methods
    }
}
