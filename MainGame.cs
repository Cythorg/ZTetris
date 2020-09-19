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

        Input input;

        Camera camera;

        Board board;
        List<IGameEntity> gameEntities;
        List<IGameComponent> gameComponents;
        List<Tetromino> tetrominoes;
        TetrominoManager tetrominoManager;

        List<GameText> gameTexts;

        Random random = new Random();

        public MainGame()
        {
            this.Window.AllowUserResizing = false;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Settings.GridSize*26;
            graphics.PreferredBackBufferWidth = Settings.GridSize*22;

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

            gameTexts = new List<GameText>();
            gameTexts.Add(new GameText());

            camera = new Camera(GraphicsDevice.Viewport);

            board = new Board(); 

            tetrominoManager = new TetrominoManager(board);



            gameEntities = new List<IGameEntity>();
            gameEntities.AddRange(gameTexts);
            gameEntities.Add(tetrominoManager);
            gameEntities.Add(board);

            gameComponents = new List<IGameComponent>();
            gameComponents.Add(tetrominoManager);


            foreach (IGameComponent gameComponent in gameComponents)
            {
                gameComponent.Initialize();
            }

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
            Block.Texture = this.Content.Load<Texture2D>("ThinBlock"); //16px*16px texture

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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) //bad code ew gross go commit seppuku
                Exit();

            if (currentState.IsKeyDown(Keys.Left) && previousState.IsKeyUp(Keys.Left))
            {
                tetrominoManager.MoveLeft();
            }
            if (currentState.IsKeyDown(Keys.Right) && previousState.IsKeyUp(Keys.Right))
            {
                tetrominoManager.MoveRight();
            }


            if (currentState.IsKeyDown(Keys.Down) && previousState.IsKeyUp(Keys.Down))
            {
                tetrominoManager.SoftDrop();
                gameTime.TotalGameTime = new TimeSpan(0, 0, 0, 0, 0);
            }

            if (currentState.IsKeyDown(Keys.Space) && previousState.IsKeyUp(Keys.Space))
            {
                tetrominoManager.HardDrop();
                gameTime.TotalGameTime = new TimeSpan(0, 0, 0, 0, 0);
            }

            if (currentState.IsKeyDown(Keys.W) && previousState.IsKeyUp(Keys.W))
            {
                tetrominoManager.HalfDrop();
                gameTime.TotalGameTime = new TimeSpan(0, 0, 0, 0, 0);
            }

            if (currentState.IsKeyDown(Keys.D) && previousState.IsKeyUp(Keys.D))
            {
                tetrominoManager.RotateClockwise();
                if (tetrominoManager.CurrentTetromino.Shape != TetrominoShape.O)
                {
                    gameTime.TotalGameTime = new TimeSpan(0, 0, 0, 0, 0);
                }
            }


            if (currentState.IsKeyDown(Keys.A) && previousState.IsKeyUp(Keys.A))
            {
                tetrominoManager.RotateAntiClockwise();
                if (tetrominoManager.CurrentTetromino.Shape != TetrominoShape.O)
                {
                    gameTime.TotalGameTime = new TimeSpan(0, 0, 0, 0, 0);
                }
            }

            if (currentState.IsKeyDown(Keys.S) && previousState.IsKeyUp(Keys.S))
            {
                tetrominoManager.Rotate180();
                if (tetrominoManager.CurrentTetromino.Shape != TetrominoShape.O)
                {
                    gameTime.TotalGameTime = new TimeSpan(0, 0, 0, 0, 0);
                }
            }

            if (currentState.IsKeyDown(Keys.LeftShift) && previousState.IsKeyUp(Keys.LeftShift))
            {
                tetrominoManager.Hold();
                gameTime.TotalGameTime = new TimeSpan(0, 0, 0, 0, 0);
            }

            //debug

            if (currentState.IsKeyDown(Keys.Q) && previousState.IsKeyUp(Keys.Q)) { }
                //






            // TODO: Add your update logic here

            camera.Update(GraphicsDevice.Viewport);

            //Board.GhostTetromino(Tetrominoes[0]).Update(gameTime); //TODO: throws null when tetromino.Y > 22

            foreach (IGameEntity gameEntity in gameEntities)
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
            spriteBatch.Begin(transformMatrix: camera.Transform, samplerState: SamplerState.PointClamp);
            //Drawing Code

            foreach (IGameEntity gameEntity in gameEntities)
            {
                gameEntity.Draw(spriteBatch);
            }

            //End Drawing Code
            spriteBatch.End();

            
            base.Draw(gameTime);
        }
    }
}
