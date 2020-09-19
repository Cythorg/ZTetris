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
        private TetrominoShape[] bag1;
        private TetrominoShape[] bag2;
        private int iterator;

        public TetrominoShape? HeldTetrominoShape
        {
            get => heldTetromino?.Shape;
            set
            {
                heldTetromino = new Tetromino((TetrominoShape)value);
                heldTetromino.Position = new Vector2(-((Settings.GridSize*4)+(Settings.GridSize/2)), ((Settings.GridSize * 3) + (Settings.GridSize / 2)));
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
        private Tetromino heldTetromino;
        private bool canHold = true;

        private readonly Board board;
        private readonly Random random;
        private TetrominoShape RandomPiece => (TetrominoShape)random.Next(Enum.GetValues(typeof(TetrominoShape)).Length);

        //Constructor
        public TetrominoManager(Board board)
        {
            this.board = board;
            random = new Random();
            bag1 = GenerateBag();
            if (bag1[0] == TetrominoShape.S || bag1[0] == TetrominoShape.Z) //ugly code, stops S or Z piece from being first piece
                while (bag1[0] == TetrominoShape.S || bag1[0] == TetrominoShape.Z)
                    bag1 = GenerateBag();
            bag2 = GenerateBag();

            CurrentTetromino = new Tetromino(bag1[0]);
            nextTetrominoesShape = new TetrominoShape[Settings.NextTetrominoesShown];
            for (int i = 0; i < nextTetrominoesShape.Length; i++)
                nextTetrominoesShape[i] = bag1[i+1]; //todo: fix this shit, bad
        }
        //End Constructor

        //Public Methods

        //CONTROLS
        public void MoveLeft() //TODO: implement DAS "Delayed auto shift" in input class not this class?
        {
            Tetromino futureTetromino = CurrentTetromino.Clone();
            futureTetromino.Coordinates -= new Coordinate(1, 0);
            if (board.IsConflict(futureTetromino) == false)
                CurrentTetromino.Coordinates -= new Coordinate(1, 0);
        }
        public void MoveRight()
        {
            Tetromino futureTetromino = CurrentTetromino.Clone();
            futureTetromino.Coordinates += new Coordinate(1, 0);
            if (board.IsConflict(futureTetromino) == false)
                CurrentTetromino.Coordinates += new Coordinate(1, 0);
        }

        public void SoftDrop()
        {
            Tetromino futureTetromino = CurrentTetromino.Clone();
            futureTetromino.Coordinates += new Coordinate(0, 1);
            if (board.IsConflict(futureTetromino) == false)
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
            Tetromino futureTetromino = CurrentTetromino.Clone();
            futureTetromino.RotateClockwise90();
            if (board.IsConflict(futureTetromino))
            {
                if (CanKick(futureTetromino))
                {
                    CurrentTetromino.Coordinates = futureTetromino.Coordinates;
                    CurrentTetromino.RotateClockwise90();
                    return;
                }
                return;
            }
            CurrentTetromino.RotateClockwise90();
        }
        public void RotateAntiClockwise()
        {
            Tetromino futureTetromino = CurrentTetromino.Clone();
            futureTetromino.RotateAntiClockwise90();
            if (board.IsConflict(futureTetromino))
            {
                if (CanKick(futureTetromino))
                {
                    CurrentTetromino.Coordinates = futureTetromino.Coordinates;
                    CurrentTetromino.RotateAntiClockwise90();
                    return;
                }
                return;
            }
            CurrentTetromino.RotateAntiClockwise90();
        }
        public void Rotate180()
        {
            Tetromino futureTetromino = CurrentTetromino.Clone();
            futureTetromino.Rotate180();
            if (board.IsConflict(futureTetromino))
            {
                if (CanKick(futureTetromino))
                {
                    CurrentTetromino.Coordinates = futureTetromino.Coordinates;
                    CurrentTetromino.Rotate180();
                    return;
                }
                return;
            }
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
            canHold = true;
            CurrentTetromino = new Tetromino(NextTetrominoesShape[0]);
            for (int i = 0; i < NextTetrominoesShape.Length - 1; i++)
                NextTetrominoesShape[i] = NextTetrominoesShape[i + 1];
            if (iterator < 7)
            {
                NextTetrominoesShape[NextTetrominoesShape.GetUpperBound(0)] = bag2[iterator]; //todo fix this shitty bodge code
                iterator++;
            }
            else if(iterator == 7)
            {
                bag2 = GenerateBag();
                iterator = 0;
                NextTetrominoesShape[NextTetrominoesShape.GetUpperBound(0)] = bag2[iterator];
            }
        }
        private void SwapHeldTetrominoWithCurrentTetromino()
        {
            if (canHold == false) return;
            canHold = false;
            if (HeldTetrominoShape == null)
            {
                HeldTetrominoShape = CurrentTetromino.Shape;
                UseNextTetromino();
                canHold = false;
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
            board.AddTetromino(CurrentTetromino);
        }

        private TetrominoShape[] GenerateBag()
        {
            //I know this is dumb, 
            //I just can't be bothered to change it
            //and it's funny to me that it is this bad.
            //it's got a big O of O(∞) lmfao
            //i'm actually rather proud to be quite frank.
            TetrominoShape?[] bag = new TetrominoShape?[7];
            for (int i = 0; i < bag.Length; i++)
            {
                bag[i] = RandomPiece;
                for (int j = 0; j < i; j++)
                {
                    if (bag[i] == bag[j])
                        i--;
                }
            }
            TetrominoShape[] castedBag = new TetrominoShape[7];
            for (int i = 0; i < castedBag.Length; i++)
                castedBag[i] = (TetrominoShape)bag[i];

            return castedBag;
        }

        private bool CanKick(Tetromino futureTetromino)
        {
            futureTetromino.Coordinates += new Coordinate(1, 0); //can right kick 1 tile
            if (board.IsConflict(futureTetromino) == false)
                return true;
            futureTetromino.Coordinates -= new Coordinate(1, 0);

            futureTetromino.Coordinates -= new Coordinate(1, 0); //can left Kick 1 tile
            if (board.IsConflict(futureTetromino) == false)
                return true;
            futureTetromino.Coordinates += new Coordinate(1, 0);

            futureTetromino.Coordinates += new Coordinate(0, 1); //can down Kick 1 tile
            if (board.IsConflict(futureTetromino) == false)
                return true;
            futureTetromino.Coordinates -= new Coordinate(0, 1);

            futureTetromino.Coordinates -= new Coordinate(0, 1); //can floor Kick 1 tile
            if (board.IsConflict(futureTetromino) == false)
                return true;
            futureTetromino.Coordinates += new Coordinate(0, 1);

            futureTetromino.Coordinates += new Coordinate(2, 0); //can right Kick 2 tiles
            if (board.IsConflict(futureTetromino) == false)
                return true;
            futureTetromino.Coordinates -= new Coordinate(2, 0);

            futureTetromino.Coordinates -= new Coordinate(2, 0); //can left kick 2 tiles
            if (board.IsConflict(futureTetromino) == false)
                return true;
            futureTetromino.Coordinates += new Coordinate(2, 0);

            futureTetromino.Coordinates += new Coordinate(0, 2); //can down Kick 2 tiles
            if (board.IsConflict(futureTetromino) == false)
                return true;
            futureTetromino.Coordinates -= new Coordinate(0, 2);

            futureTetromino.Coordinates -= new Coordinate(0, 2); //can floor Kick 2 tiles
            if (board.IsConflict(futureTetromino) == false)
                return true;
            futureTetromino.Coordinates += new Coordinate(0, 2);

            return false; //cannot kick
        }

        private void ApplyGravity(GameTime gameTime)
        {
            if (gameTime.TotalGameTime > new TimeSpan(0, 0, 0, 0, 1000)) //todo: gimpy way of doing timings lol (FIX)
            {
                Tetromino futureTetromino = CurrentTetromino.Clone();
                futureTetromino.Coordinates += new Coordinate(0, 1);
                if (board.IsConflict(futureTetromino))
                {
                    AddTetrominoToBoard();
                    UseNextTetromino();
                    gameTime.TotalGameTime = new TimeSpan(0, 0, 0, 0, 0);
                }
                else
                {
                    gameTime.TotalGameTime = new TimeSpan(0, 0, 0, 0, 0);
                    CurrentTetromino.Coordinates += new Coordinate(0, 1);
                }
            }
        }
        //End Private Methods

        //Interface Methods
        //IGameComponent
        public void Initialize()
        {
            //
        }
        //IGameEntity
        public void Update(GameTime gameTime)
        {
            ApplyGravity(gameTime);
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
