using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZTetris.Assets;

namespace ZTetris
{
    class TetrominoManager : IGameComponent, IGameEntity
    {
        public Tetromino CurrentTetromino
        {
            get => currentTetromino;
            set
            { 
                currentTetromino = value;
                currentTetromino.Coordinates = currentTetromino.Shape switch
                {
                    TetrominoShape.I => new Coordinate(3, 0),
                    TetrominoShape.O => new Coordinate(4, 0),
                    TetrominoShape.T => new Coordinate(3, 0),
                    TetrominoShape.L => new Coordinate(3, 0),
                    TetrominoShape.J => new Coordinate(3, 0),
                    TetrominoShape.S => new Coordinate(3, 0),
                    TetrominoShape.Z => new Coordinate(3, 0),
                    _ => throw new MissingFieldException(),
                };
            }
        }
        private Tetromino currentTetromino;

        public Tetromino GhostTetromino
        {
            get
            {
                for (int y = currentTetromino.Coordinates.Y; y < board.YLength; y++)
                {
                    ghostTetromino = currentTetromino.Clone();
                    ghostTetromino.Color = Settings.GhostPieceColor;
                    ghostTetromino.Coordinates = new Coordinate(currentTetromino.Coordinates.X, y);
                    if (board.IsConflict(ghostTetromino))
                    {
                        ghostTetromino.Coordinates -= new Coordinate(0, 1);
                        return ghostTetromino;
                    }
                }
                return null;
            }
        }
        private Tetromino ghostTetromino;
        public TetrominoShape[] NextTetrominoesShape
        {
            get => nextTetrominoesShape;
        }
        private TetrominoShape[] nextTetrominoesShape;
        private Tetromino[] nextTetrominoes;

        public TetrominoShape? HeldTetrominoShape
        {
            get => heldTetrominoShape;
            set
            {
                heldTetrominoShape = value;
                heldTetromino = new Tetromino((TetrominoShape)value);
                heldTetromino.Position = new Vector2(-((Settings.GridSize*4)+(Settings.GridSize/2)), ((Settings.GridSize * 3) + (Settings.GridSize / 2)));
                //heldTetromino.Position += heldTetromino.Shape switch
                //{
                //    TetrominoShape.I => new Vector2(0, -4),
                //    TetrominoShape.O => new Vector2(16, 4),
                //    TetrominoShape.T => new Vector2(8, 4),
                //    TetrominoShape.L => new Vector2(8, 4),
                //    TetrominoShape.J => new Vector2(8, 4),
                //    TetrominoShape.S => new Vector2(8, 4),
                //    TetrominoShape.Z => new Vector2(8, 4),
                //    _ => throw new MissingFieldException(),
                //};
                heldTetromino.Position += heldTetromino.Shape switch
                {
                    TetrominoShape.I => new Vector2(0, -(Settings.GridSize/4)),
                    TetrominoShape.O => new Vector2(Settings.GridSize, (Settings.GridSize/4)),
                    TetrominoShape.T => new Vector2((Settings.GridSize / 2), (Settings.GridSize / 4)),
                    TetrominoShape.L => new Vector2((Settings.GridSize / 2), (Settings.GridSize / 4)),
                    TetrominoShape.J => new Vector2((Settings.GridSize / 2), (Settings.GridSize / 4)),
                    TetrominoShape.S => new Vector2((Settings.GridSize / 2), (Settings.GridSize / 4)),
                    TetrominoShape.Z => new Vector2((Settings.GridSize / 2), (Settings.GridSize / 4)),
                    _ => throw new MissingFieldException(),
                };
            }
        }  
        private TetrominoShape? heldTetrominoShape;
        private Tetromino heldTetromino;


        private readonly Board board;
        private readonly Random random;
        private TetrominoShape RandomPiece => (TetrominoShape)random.Next(Enum.GetValues(typeof(TetrominoShape)).Length);

        //Constructor
        public TetrominoManager(Board board)
        {
            this.board = board;
            random = new Random();
            CurrentTetromino = new Tetromino(RandomPiece);
            nextTetrominoesShape = new TetrominoShape[Settings.NextTetrominoesShown];
            for (int i = 0; i < nextTetrominoesShape.Length; i++)
                nextTetrominoesShape[i] = RandomPiece;
        }
        //End Constructor

        //Public Methods

        //CONTROLS
        public void MoveLeft() //TODO: implement DAS "Delayed auto shift" in input class not this class?
        {
            CurrentTetromino.Coordinates -= new Coordinate(1, 0);
        }

        public void MoveRight()
        {
            CurrentTetromino.Coordinates += new Coordinate(1, 0);
        }

        public void SoftDrop()
        {
            CurrentTetromino.Coordinates += new Coordinate(0, 1);
        }
        public void HalfDrop()
        {
            MoveTetrominoToGhost();
        }
        public void HardDrop()
        {
            MoveTetrominoToGhost();
            AddTetrominoToBoard();
            UseNextTetromino();
        }

        public void RotateClockwise()
        {
            CurrentTetromino.RotateClockwise90();
        }

        public void RotateAntiClockwise()
        {
            CurrentTetromino.RotateAntiClockwise90();
        }

        public void Rotate180()
        {
            CurrentTetromino.Rotate180();
        }

        public void Hold()
        {
            SwapHeldTetrominoWithCurrentTetromino();
        }
        //END CONTROLS

        //End Public Methods

        //Private Methods
        private void UseNextTetromino()
        {
            CurrentTetromino = new Tetromino(NextTetrominoesShape[0]);
            for (int i = 0; i < NextTetrominoesShape.Length - 1; i++)
                NextTetrominoesShape[i] = NextTetrominoesShape[i + 1];
            NextTetrominoesShape[NextTetrominoesShape.GetUpperBound(0)] = RandomPiece;
        }
        private void SwapHeldTetrominoWithCurrentTetromino()
        {
            if (HeldTetrominoShape == null)
            {
                HeldTetrominoShape = CurrentTetromino.Shape;
                UseNextTetromino();
                return;
            }
            TetrominoShape tempTetrominoShape = CurrentTetromino.Shape;
            CurrentTetromino = new Tetromino((TetrominoShape)HeldTetrominoShape);
            HeldTetrominoShape = tempTetrominoShape;
        }
        private void MoveTetrominoToGhost()
        {
            CurrentTetromino.Coordinates = GhostTetromino.Coordinates;
        }

        private void AddTetrominoToBoard()
        {
            board.AddTetrominoToBoard(CurrentTetromino);
        }
        //End Private Methods

        //Interface Methods
        //IGameComponent
        public void Initialize()
        {
            Console.WriteLine("Initialised from tetrominomanager class");
            //
        }
        //IGameEntity????
        public void Update(GameTime gameTime)
        {
            //
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < nextTetrominoesShape.Count(); i++)
            {
                Tetromino nextTetromino = new Tetromino(nextTetrominoesShape[i]);
                nextTetromino.Position = new Vector2((Settings.GridSize*12)+(Settings.GridSize/2), (i*((Settings.GridSize*2)+(Settings.GridSize/2))) + ((Settings.GridSize*3)+(Settings.GridSize/2)));
                nextTetromino.Position += nextTetromino.Shape switch
                {
                    TetrominoShape.I => new Vector2(0, -(Settings.GridSize / 4)),
                    TetrominoShape.O => new Vector2(Settings.GridSize, (Settings.GridSize / 4)),
                    TetrominoShape.T => new Vector2((Settings.GridSize / 2), (Settings.GridSize / 4)),
                    TetrominoShape.L => new Vector2((Settings.GridSize / 2), (Settings.GridSize / 4)),
                    TetrominoShape.J => new Vector2((Settings.GridSize / 2), (Settings.GridSize / 4)),
                    TetrominoShape.S => new Vector2((Settings.GridSize / 2), (Settings.GridSize / 4)),
                    TetrominoShape.Z => new Vector2((Settings.GridSize / 2), (Settings.GridSize / 4)),
                    _ => throw new MissingFieldException(),
                };
                nextTetromino.Draw(spriteBatch);
            }
            heldTetromino?.Draw(spriteBatch);
            GhostTetromino?.Draw(spriteBatch);
            CurrentTetromino.Draw(spriteBatch);
        }
        //End Interface Methods
    }
}
