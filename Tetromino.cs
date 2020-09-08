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
    class Tetromino : IGameEntity
    {
        public enum PieceShape { I, O, T, L, J, S, Z }
        public PieceShape Shape;
        private Color Color;

        public int XCoordinate;
        public int YCoordinate;

        public bool[,] BlockState;
        public Block[,] Blocks;


        //Constructor
        public Tetromino(Random random)
        {
            Shape = (PieceShape)random.Next(Enum.GetValues(typeof(PieceShape)).Length);


            XCoordinate = 3;
            YCoordinate = 0;
            switch (Shape) //TODO: Stop using blockstate and start using Block (Maybe)
            {
                case PieceShape.I:
                    Color = Settings.IPieceColor;       // cyan   
                    BlockState = new bool[4, 4] {       
                        {false, true, false, false},    // []██[][]
                        {false, true, false, false},    // []██[][]
                        {false, true, false, false},    // []██[][]
                        {false, true, false, false}     // []██[][]
                    };                                  
                    break;                                 
                case PieceShape.O:                         
                    Color = Settings.OPieceColor;       // yellow   
                    BlockState = new bool[2, 2] {          
                        {true, true},                   // ████
                        {true, true}                    // ████
                    };                                  
                    XCoordinate = 4;                    
                    break;                                 
                case PieceShape.T:                         
                    Color = Settings.TPieceColor;       // magenta
                    BlockState = new bool[3, 3] {          
                        {false, true , false},          // []██[]
                        {true , true , true },          // ██████
                        {false, false, false}           // [][][]
                    };                                  
                    break;                              
                case PieceShape.L:                      
                    Color = Settings.LPieceColor;       // orange
                    BlockState = new bool[3, 3] {       
                        {false, false, true },          // [][]██
                        {true , true , true },          // ██████
                        {false, false, false}           // [][][]
                    };                                  
                    break;                              
                case PieceShape.J:                      
                    Color = Settings.JPieceColor;       // blue
                    BlockState = new bool[3, 3] {       
                        {true , false, false},          // ██[][]
                        {true , true , true },          // ██████
                        {false, false, false}           // [][][]
                    };                                  
                    break;                              
                case PieceShape.S:                      
                    Color = Settings.SPieceColor;       // green
                    BlockState = new bool[3, 3] {       
                        {false, true , true },          // []████
                        {true , true , false},          // ████[]
                        {false, false, false}           // [][][]
                    };                                  
                    break;                              
                case PieceShape.Z:                      
                    Color = Settings.ZPieceColor;       // red
                    BlockState = new bool[3, 3] {       
                        {true , true , false},          // ████[]
                        {false, true , true },          // []████
                        {false, false, false}           // [][][]
                    };
                    break;
                default:
                    break;
            }
            Blocks = CreateBlocksFromBlockState(BlockState, Color);
        }
        //End Constructor

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
            Blocks = CreateBlocksFromBlockState(BlockState, Color);
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
            Blocks = CreateBlocksFromBlockState(BlockState, Color);
        }

        //Private Methods
        private Block[,] CreateBlocksFromBlockState (bool[,] blockState, Color color)
        {
            Block[,] blocks = new Block[blockState.GetLength(0), blockState.GetLength(1)];
            for (int y = 0; y < blockState.GetLength(0); y++)
            {
                for (int x = 0; x < blockState.GetLength(1); x++)
                {
                    if (blockState[y, x] == true)
                    {
                        blocks[y, x] = new Block(x + XCoordinate, y + YCoordinate, color);
                    }
                    else if(blockState[y, x] == false)
                    {
                        blocks[y, x] = null;
                    }
                }
            }
            return blocks;
        }
        //End Private Methods

        //Interface Methods
        public void Update(GameTime gameTime)
        {
            Blocks = CreateBlocksFromBlockState(BlockState, Color);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < BlockState.GetLength(0); y++)
            {
                for (int x = 0; x < BlockState.GetLength(1); x++)
                {
                    if (BlockState[y, x])
                    {
                        Blocks[y, x].Draw(spriteBatch);
                    }
                }
            }
        }
        //End Interface Methods


    }
}


