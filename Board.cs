using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ZTetris.Assets
{
    class Board : IGameObject
    {
        public static Texture2D Texture;

        bool[,] BlockState = new bool[22, 10];
        Block[,] Blocks = new Block[22, 10];



        public Board()
        {
            BlockState[2, 1] = true;
            Blocks[2, 1] = new Block(1, 2);
            BlockState[15, 6] = true;
            Blocks[15, 6] = new Block(6, 15);
        }

        public bool DoBlockStatesConflict(Tetromino tetromino, int xOffset, int yOffset)
        {


            for (int y = 0; y < tetromino.BlockState.GetLength(0); y++)
            {
                for (int x = 0; x < tetromino.BlockState.GetLength(1); x++)
                {
                    if (y + yOffset >= BlockState.GetLength(0) || x + xOffset >= BlockState.GetLength(1) || y + yOffset < 0 || x + xOffset < 0)
                    {
                        if (tetromino.BlockState[y, x])
                        {
                            return true;
                        }
                        continue;
                    }
                    if (BlockState[y + yOffset, x + xOffset] && tetromino.BlockState[y, x])
                    {
                        return true;
                    }
                }
            }
            return false;
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

    }
}
