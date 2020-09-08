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

        int XLength = 10;
        int YLength = 22;

        public int Score;

        Block[,] Blocks;

        //Constructor
        public Board(int xLength = 10, int yLength = 22) //this is the conventional default tetris board size
        {
            XLength = xLength;
            YLength = yLength;
            Blocks = new Block[YLength, XLength];
        }
        //End Constructor

        public void TEST_FillBoard()
        {
            for (int y = 0; y < YLength; y++)
            {
                for (int x = 0; x < XLength - 2; x++)
                {
                    Blocks[y, x] = new Block(x, y, Color.White);
                }
            }
        }

        public bool IsConflict(Tetromino tetromino)
        {
            for (int y = 0; y < tetromino.BlockState.GetLength(0); y++)
            {
                for (int x = 0; x < tetromino.BlockState.GetLength(1); x++)
                {
                    if (y + tetromino.YCoordinate >= YLength || x + tetromino.XCoordinate >= XLength || y + tetromino.YCoordinate < 0 || x + tetromino.XCoordinate < 0)
                    {
                        if (tetromino.Blocks[y, x] != null)
                        {
                            return true;
                        }
                        continue;
                    }
                    if (Blocks[y + tetromino.YCoordinate, x + tetromino.XCoordinate] != null && tetromino.BlockState[y, x] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void AddTetrominoToBoard(Tetromino tetromino)
        {
            if (IsConflict(tetromino)) 
            {
                return;
            }


            for (int y = 0; y < tetromino.Blocks.GetLength(0); y++)
            {
                for (int x = 0; x < tetromino.Blocks.GetLength(1); x++)
                {
                    if (tetromino.BlockState[y, x] == true)
                    {
                        Blocks[y + tetromino.YCoordinate, x + tetromino.XCoordinate] = tetromino.Blocks[y, x];
                    }
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
            Score += 1;
            for (int y = fromY; y > 0; y--)
            {
                for (int x = 0; x < XLength; x++)
                {
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

            GameText.Score = Score.ToString();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(0, 32), Color.White); //draws the board

            for (int y = 0; y < Blocks.GetLength(0); y++)
            {
                for (int x = 0; x < Blocks.GetLength(1); x++)
                {
                    if (Blocks[y, x] != null)
                    {
                        Blocks[y, x].Draw(spriteBatch);
                    }
                }
            }
        }
        //End Interface Methods
    }
}
