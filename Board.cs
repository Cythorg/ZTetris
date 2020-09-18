using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ZTetris.Assets
{
    class Board : IGameEntity
    {
        public static Texture2D Texture;

        public int LinesCleared;

        public int XLength { get; private set; }
        public int YLength { get; private set; }

        Block[,] Blocks
        {
            get
            {
                return blocks;
            }
            set
            {
                blocks = value;
            }
        }
        private Block[,] blocks;


        //Constructor
        public Board(int xLength = 10, int yLength = 22) //this is the conventional default tetris board size
        {
            XLength = xLength;
            YLength = yLength;
            Blocks = new Block[YLength, XLength];
        }
        //End Constructor


        public bool IsConflict(Tetromino tetromino)
        {
            for (int y = 0; y < tetromino.Blocks.GetLength(0); y++)
                for (int x = 0; x < tetromino.Blocks.GetLength(1); x++)
                {
                    if (tetromino.Blocks[y, x] != null)
                    {
                        if (y + tetromino.Coordinates.Y >= YLength || x + tetromino.Coordinates.X >= XLength || y + tetromino.Coordinates.Y < 0 || x + tetromino.Coordinates.X < 0)
                            return true;
                        if (Blocks[y + tetromino.Coordinates.Y, x + tetromino.Coordinates.X] != null)
                            return true;
                    }
                }
            return false;
        }

        public void AddTetrominoToBoard(Tetromino tetromino)
        {
            if (IsConflict(tetromino)) return;

            for (int y = 0; y < tetromino.Blocks.GetLength(0); y++)
                for (int x = 0; x < tetromino.Blocks.GetLength(1); x++)
                {
                    if (tetromino.Blocks[y, x] != null)
                    {
                        Blocks[y + tetromino.Coordinates.Y, x + tetromino.Coordinates.X] = tetromino.Blocks[y, x].Clone();
                    }
                }
        }

        public void UpdateLines()
        {
            for (int y = 0; y < Blocks.GetLength(0); y++)
            {
                bool SkipLine = false;
                for (int x = 0; x < Blocks.GetLength(1); x++)
                {
                    if (Blocks[y, x] == null)
                    {
                        SkipLine = true;
                        break;
                    }
                }
                if (SkipLine == false)
                {
                    ClearLine(y);
                }
            }
        }

        public void ClearLine(int fromY)
        {
            LinesCleared += 1;
            for (int y = fromY; y >= 0; y--)
            {
                for (int x = 0; x < XLength; x++)
                {
                    if (y == 0)
                    {
                        Blocks[y, x] = null;
                        continue;
                    }
                    Blocks[y, x] = Blocks[y - 1, x];
                    Blocks[y - 1, x] = null;
                    if (Blocks[y, x] != null)
                    {
                        Blocks[y, x].Coordinates = new Coordinate(x, y);
                    }
                }
            }
        }

        //Interface Methods
        public void Update(GameTime gameTime)
        {

            UpdateLines();

            GameText.LinesCleared = LinesCleared.ToString();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Texture, new Vector2(0, 32), Color.White); //draws the board

            { //Draw Board
                Block boardBlock = new Block(default);
                for (int y = 2; y <= YLength; y++)
                {
                    boardBlock.Coordinates = new Coordinate(-1, y);
                    boardBlock.Draw(spriteBatch);
                    boardBlock.Coordinates = new Coordinate(XLength, y);
                    boardBlock.Draw(spriteBatch);

                }
                for (int x = 0; x < XLength; x++)
                {
                    boardBlock.Coordinates = new Coordinate(x, YLength);
                    boardBlock.Draw(spriteBatch);
                }
            }

            //Draw Blocks
            for (int y = 0; y < Blocks.GetLength(0); y++)
                for (int x = 0; x < Blocks.GetLength(1); x++)
                {
                    if (Blocks[y, x] != null)
                    {
                        Blocks[y, x].Draw(spriteBatch);
                    }
                }

            //tetrominoManager.Draw(spriteBatch);
        }
        //End Interface Methods
    }
}
