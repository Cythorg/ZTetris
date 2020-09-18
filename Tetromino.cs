using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZTetris.Assets;

namespace ZTetris
{
    enum TetrominoShape { I, O, T, L, J, S, Z }
    class Tetromino : IGameEntity
    {
        public TetrominoShape Shape
        {
            get => shape;
            set
            {
                shape = value;
                switch (Shape) //TODO: Stop using blockstate and start using Block (Maybe)
                {
                    case TetrominoShape.I:
                        color = Settings.IPieceColor;       // cyan   
                        BlockState = new bool[4, 4] {
                        {false, false, false, false},    // [][][][]
                        {true , true , true , true },    // ████████
                        {false, false, false, false},    // [][][][]
                        {false, false, false, false}     // [][][][]
                    };
                        break;
                    case TetrominoShape.O:
                        color = Settings.OPieceColor;       // yellow   
                        BlockState = new bool[2, 2] {
                        {true, true},                   // ████
                        {true, true}                    // ████
                    };
                        break;
                    case TetrominoShape.T:
                        color = Settings.TPieceColor;       // magenta
                        BlockState = new bool[3, 3] {
                        {false, true , false},          // []██[]
                        {true , true , true },          // ██████
                        {false, false, false}           // [][][]
                    };
                        break;
                    case TetrominoShape.L:
                        color = Settings.LPieceColor;       // orange
                        BlockState = new bool[3, 3] {
                        {false, false, true },          // [][]██
                        {true , true , true },          // ██████
                        {false, false, false}           // [][][]
                    };
                        break;
                    case TetrominoShape.J:
                        color = Settings.JPieceColor;       // blue
                        BlockState = new bool[3, 3] {
                        {true , false, false},          // ██[][]
                        {true , true , true },          // ██████
                        {false, false, false}           // [][][]
                    };
                        break;
                    case TetrominoShape.S:
                        color = Settings.SPieceColor;       // green
                        BlockState = new bool[3, 3] {
                        {false, true , true },          // []████
                        {true , true , false},          // ████[]
                        {false, false, false}           // [][][]
                    };
                        break;
                    case TetrominoShape.Z:
                        color = Settings.ZPieceColor;       // red
                        BlockState = new bool[3, 3] {
                        {true , true , false},          // ████[]
                        {false, true , true },          // []████
                        {false, false, false}           // [][][]
                    };
                        break;
                    default:
                        break;
                }
            }
        }
        private TetrominoShape shape;

        public bool[,] BlockState
        {
            get => blockState;
            set
            {
                blockState = value;
                blocks = new Block[blockState.GetLength(0), blockState.GetLength(1)];
                for (int y = 0; y < blockState.GetLength(0); y++)
                    for (int x = 0; x < blockState.GetLength(1); x++)
                    {
                        if (blockState[y, x] == true)
                            blocks[y, x] = new Block(new Coordinate(x, y) + Coordinates, color);
                        else if (blockState[y, x] == false)
                            blocks[y, x] = null;
                    }
            }
        }
        private bool[,] blockState;
        public Block[,] Blocks
        {
            get => blocks;
            set => blocks = value;
        }
        private Block[,] blocks;

        public Coordinate Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value;
                for (int y = 0; y < Blocks.GetLength(0); y++)
                    for (int x = 0; x < Blocks.GetLength(1); x++)
                    {
                        if (Blocks[y, x] != null)
                        {
                            Blocks[y, x].Coordinates = value + new Coordinate(x, y);
                        }
                    }
            } 
        }
        private Coordinate coordinates;

        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                for (int y = 0; y < Blocks.GetLength(0); y++)
                    for (int x = 0; x < Blocks.GetLength(1); x++)
                    {
                        if (Blocks[y, x] != null)
                        {
                            Blocks[y, x].Position = value + new Vector2(x*Settings.GridSize, y*Settings.GridSize);
                        }
                    }
            }
        }
        private Vector2 position;

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                for (int y = 0; y < Blocks.GetLength(0); y++)
                    for (int x = 0; x < Blocks.GetLength(1); x++)
                    {
                        if (Blocks[y, x] != null)
                        {
                            Blocks[y, x].Color = value;
                        }
                    }
            }
        }
        private Color color;


        //Constructor
        public Tetromino(TetrominoShape shape)
        {
            Shape = shape;
        }
        //End Constructor

        public void RotateClockwise90()
        {
            bool[,] RotatedBlockState = new bool[BlockState.GetLength(1), BlockState.GetLength(0)];
            for (int y = 0; y < BlockState.GetLength(0); y++)
                for (int x = 0; x < BlockState.GetLength(1); x++)
                {
                    RotatedBlockState[y, x] = BlockState[BlockState.GetUpperBound(0) - x, y];
                }
            BlockState = RotatedBlockState;
        }
        public void RotateAntiClockwise90()
        {
            bool[,] RotatedBlockState = new bool[BlockState.GetLength(1), BlockState.GetLength(0)];
            for (int y = 0; y < BlockState.GetLength(0); y++)
                for (int x = 0; x < BlockState.GetLength(1); x++)
                {
                    RotatedBlockState[y, x] = BlockState[x, BlockState.GetUpperBound(1) - y];
                }
            BlockState = RotatedBlockState;
        }
        public void Rotate180()
        {
            bool[,] RotatedBlockState = new bool[BlockState.GetLength(1), BlockState.GetLength(0)];
            for (int y = 0; y < BlockState.GetLength(0); y++)
                for (int x = 0; x < BlockState.GetLength(1); x++)
                {
                    RotatedBlockState[y, x] = BlockState[BlockState.GetUpperBound(0) - y, BlockState.GetUpperBound(1) - x];
                }
            BlockState = RotatedBlockState;
        }

        public Tetromino Clone()
        {
            Tetromino cloneTetromino = (Tetromino)this.MemberwiseClone();
            cloneTetromino.BlockState = this.BlockState;

            return cloneTetromino;
        }
        //Interface Methods
        public void Update(GameTime gameTime)
        {
            //
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Blocks.GetLength(0); y++)
                for (int x = 0; x < Blocks.GetLength(1); x++)
                {
                    if (Blocks[y, x] != null)
                        Blocks[y, x].Draw(spriteBatch);
                }
        }
        //End Interface Methods


    }
}


