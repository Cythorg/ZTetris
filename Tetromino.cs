using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTetris
{
    class Tetromino : IGameObject
    {
        public enum PieceShape { I, O, T, L, J, S, Z }
        public PieceShape Shape;


        public bool[,] BlockState;
        public int XCoordinate;
        public int YCoordinate;
        Color Color;
        Block[,] Blocks;

        public Tetromino(Random random)
        {
            Shape = (PieceShape)random.Next(Enum.GetValues(typeof(PieceShape)).Length);

            switch (Shape)
            {
                case PieceShape.I:
                    BlockState = new bool[4, 4] {
                        {false, true, false, false},   // []██[][]
                        {false, true, false, false},   // []██[][]
                        {false, true, false, false},   // []██[][]
                        {false, true, false, false}    // []██[][]
                    };                                    
                    Color = new Color(255, 0, 0);      // red   
                    break;                                
                case PieceShape.O:                        
                    BlockState = new bool[2, 2] {         
                        {true, true},                  // ████
                        {true, true}                   // ████
                    };                                    
                    Color = new Color(255, 255, 0);    // yellow   
                    break;                                
                case PieceShape.T:                        
                    BlockState = new bool[3, 3] {         
                        {false, true , false},         // []██[]
                        {true , true , true },         // ██████
                        {false, false, false}          // [][][]
                    };
                    Color = new Color(255, 0, 255);    // magenta
                    break;
                case PieceShape.L:
                    BlockState = new bool[3, 3] {
                        {false, false, true },         // [][]██
                        {true , true , true },         // ██████
                        {false, false, false}          // [][][]
                    };
                    Color = new Color(255, 102, 0);    // orange
                    break;
                case PieceShape.J:
                    BlockState = new bool[3, 3] {
                        {true , false, false},         // ██[][]
                        {true , true , true },         // ██████
                        {false, false, false}          // [][][]
                    };
                    Color = new Color(0, 0, 255);      // blue
                    break;
                case PieceShape.S:
                    BlockState = new bool[3, 3] {
                        {false, true , true },         // []████
                        {true , true , false},         // ████[]
                        {false, false, false}          // [][][]
                    };
                    Color = new Color(0, 255, 0);      // green
                    break;
                case PieceShape.Z:
                    BlockState = new bool[3, 3] {
                        {true , true , false},         // ████[]
                        {false, true , true },         // []████
                        {false, false, false}          // [][][]
                    };
                    Color = new Color(0, 255, 255);    // cyan
                    break;
                default:
                    break;
            }
            XCoordinate = 3;
            YCoordinate = 0;
            Blocks = CreateBlocksFromBlockState(BlockState, Color);
        }

        public void RotateClockwise()
        {
            bool[,] RotatedBlockState = new bool[BlockState.GetLength(1), BlockState.GetLength(0)];

            for (int y = 0; y < BlockState.GetLength(0); y++)
            {
                for (int x = 0; x < BlockState.GetLength(1); x++)
                {
                    RotatedBlockState[y, x] = BlockState[(BlockState.GetLength(0) - 1) - x, y];
                }
            }
            BlockState = RotatedBlockState;
        }

        public void RotateAntiClockwise()
        {
            bool[,] RotatedBlockState = new bool[BlockState.GetLength(1), BlockState.GetLength(0)];

            for (int y = 0; y < BlockState.GetLength(0); y++)
            {
                for (int x = 0; x < BlockState.GetLength(1); x++)
                {
                    RotatedBlockState[y, x] = BlockState[x, (BlockState.GetLength(1) - 1) - y];
                }
            }
            BlockState = RotatedBlockState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < BlockState.GetLength(0); y++)
            {
                for (int x = 0; x < BlockState.GetLength(1); x++)
                {
                    if (BlockState[y, x])
                    {
                        Blocks = CreateBlocksFromBlockState(BlockState, Color);
                        Blocks[y, x].Draw(spriteBatch);
                    }
                }
            }
        }

        private Block[,] CreateBlocksFromBlockState (bool[,] blockState, Color color)
        {
            Block[,] blocks = new Block[blockState.GetLength(0), blockState.GetLength(1)];
            for (int y = 0; y < blockState.GetLength(0); y++)
            {
                for (int x = 0; x < blockState.GetLength(1); x++)
                {
                    if (blockState[y, x])
                    {
                        blocks[y, x] = new Block(x + XCoordinate, y + YCoordinate, color);
                    }
                }
            }
            return blocks;
        }
    }
}


