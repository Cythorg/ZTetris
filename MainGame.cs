using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using ZTetris.Assets;

namespace ZTetris
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState previousState;
        KeyboardState currentState;

        Camera camera;

        Board Board;
        List<IGameEntity> GameEntities;
        List<Tetromino> Tetrominoes;

        List<GameText> GameTexts;

        Random random = new Random();

        public MainGame()
        {
            this.Window.AllowUserResizing = true;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Assets";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true;

            GameTexts = new List<GameText>();
            GameTexts.Add(new GameText());

            camera = new Camera(GraphicsDevice.Viewport);

            Board = new Board(); 

            Tetrominoes = new List<Tetromino>();
            Tetrominoes.Add(new Tetromino((PieceShape)random.Next(Enum.GetValues(typeof(PieceShape)).Length)));

            GameEntities = new List<IGameEntity>();
            GameEntities.AddRange(GameTexts);
            GameEntities.AddRange(Tetrominoes);
            GameEntities.Add(Board);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Block.Texture = this.Content.Load<Texture2D>("Block"); //16px*16px texture
            Board.Texture = this.Content.Load<Texture2D>("Board"); //192px*336px texture

            GameText.Font = this.Content.Load<SpriteFont>("PressStart");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            

            previousState = currentState;
            currentState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (currentState.IsKeyDown(Keys.D) && previousState.IsKeyUp(Keys.D))
            {
                Tetrominoes[0].RotateClockwise();
            }

            if (currentState.IsKeyDown(Keys.A) && previousState.IsKeyUp(Keys.A))
            {
                Tetrominoes[0].RotateAntiClockwise();
            }

            if (currentState.IsKeyDown(Keys.S) && previousState.IsKeyUp(Keys.S))
            {
                Board.AddTetrominoToBoard(Tetrominoes[0]);
                //
            }
            if (currentState.IsKeyDown(Keys.Space) && previousState.IsKeyUp(Keys.Space))
            {
                Board.MoveTetrominoToGhost(Tetrominoes[0]);
                Board.AddTetrominoToBoard(Tetrominoes[0]);
                GameEntities.Remove(Tetrominoes[0]);
                Tetrominoes[0] = new Tetromino((PieceShape)random.Next(Enum.GetValues(typeof(PieceShape)).Length));
                GameEntities.Add(Tetrominoes[0]);
                //
            }


            if (Board.IsConflict(Tetrominoes[0]))
            {
                GameTexts[0].IsConflict = true;
            }
            else
            {
                GameTexts[0].IsConflict = false;
            }

            if (currentState.IsKeyDown(Keys.Right) && previousState.IsKeyUp(Keys.Right))
            {
                Tetrominoes[0].Coordinates += new Coordinate(1, 0);
            }

            if (currentState.IsKeyDown(Keys.Left) && previousState.IsKeyUp(Keys.Left))
            {
                Tetrominoes[0].Coordinates -= new Coordinate(1, 0);
            }

            if (currentState.IsKeyDown(Keys.Up) && previousState.IsKeyUp(Keys.Up))
            {
                Tetrominoes[0].Coordinates -= new Coordinate(0, 1);
            }

            if (currentState.IsKeyDown(Keys.Down) && previousState.IsKeyUp(Keys.Down))
            {
                Tetrominoes[0].Coordinates += new Coordinate(0, 1);
            }

            if (currentState.IsKeyDown(Keys.Q) && previousState.IsKeyUp(Keys.Q))
            {
                Board.MoveTetrominoToGhost(Tetrominoes[0]);
            }

            if (currentState.IsKeyDown(Keys.E) && previousState.IsKeyUp(Keys.E))
            {
                Board.TEST_FillBoard();
            }



            // TODO: Add your update logic here

            camera.Update(GraphicsDevice.Viewport);

            //Board.GhostTetromino(Tetrominoes[0]).Update(gameTime); //TODO: throws null when tetromino.Y > 22

            foreach (IGameEntity gameEntity in GameEntities)
            {
                gameEntity.Update(gameTime);
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin(transformMatrix: camera.Transform);
            //Drawing Code

            Board.GhostTetromino(Tetrominoes[0]).Draw(spriteBatch);

            foreach (IGameEntity gameEntity in GameEntities)
            {
                gameEntity.Draw(spriteBatch);
            }

            //End Drawing Code
            spriteBatch.End();

            
            base.Draw(gameTime);
        }
    }
}
