using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTetris
{
    class TetrominoManager
    {
        public Tetromino CurrentTetromino
        {
            get => currentTetromino;
            set => currentTetromino = value;
        }
        private Tetromino currentTetromino;

        public Tetromino GhostTetromino
        {
            get => ghostTetromino;
            set => ghostTetromino = value;
        }
        private Tetromino ghostTetromino;
        public Tetromino[] NextTetrominoes
        {
            get => nextTetrominoes;
            set => nextTetrominoes = value;
        }
        private Tetromino[] nextTetrominoes;

        private readonly Random random;

        //Constructor
        public TetrominoManager()
        {
            random = new Random();
            currentTetromino = new Tetromino(RandomPiece());
            nextTetrominoes = new Tetromino[4];
        }
        //End Constructor

        //Public Methods
        public Tetromino GetNextTetromino()
        {
            Tetromino nextTetromino = nextTetrominoes[0];

            for (int i = 0; i < nextTetrominoes.Length - 1; i++)
                { nextTetrominoes[i] = nextTetrominoes[i + 1]; }
            nextTetrominoes[nextTetrominoes.Length - 1] = new Tetromino(RandomPiece());

            return nextTetromino;
        }
        //End Public Methods

        //Private Methods
        private PieceShape RandomPiece() => (PieceShape)random.Next(Enum.GetValues(typeof(PieceShape)).Length);
        //End Private Methods
    }
}
