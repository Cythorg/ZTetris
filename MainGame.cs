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


        Board Board;
        List<IGameObject> GameObjects;
        List<Tetromino> Tetrominoes;

        List<GameText> GameTexts;

        Random random = new Random();

        public MainGame()
        {
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

            GameTexts = new List<GameText>();
            GameTexts.Add(new GameText());

            Board = new Board(); 

            Tetrominoes = new List<Tetromino>();
            Tetrominoes.Add(new Tetromino(random));

            GameObjects = new List<IGameObject>();
            GameObjects.AddRange(GameTexts);
            GameObjects.AddRange(Tetrominoes);
            GameObjects.Add(Board);


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
            Board.Texture = this.Content.Load<Texture2D>("Board"); //336px*192px texture

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
                //
            }


            if (Board.DoBlockStatesConflict(Tetrominoes[0], Tetrominoes[0].XCoordinate , Tetrominoes[0].YCoordinate))
            {
                GameTexts[0].IsConflict = true;
            }
            else
            {
                GameTexts[0].IsConflict = false;
            }

            if (currentState.IsKeyDown(Keys.Right) && previousState.IsKeyUp(Keys.Right))
            {
                Tetrominoes[0].XCoordinate += 1;
            }

            if (currentState.IsKeyDown(Keys.Left) && previousState.IsKeyUp(Keys.Left))
            {
                Tetrominoes[0].XCoordinate -= 1;
            }

            if (currentState.IsKeyDown(Keys.Up) && previousState.IsKeyUp(Keys.Up))
            {
                Tetrominoes[0].YCoordinate -= 1;
            }

            if (currentState.IsKeyDown(Keys.Down) && previousState.IsKeyUp(Keys.Down))
            {
                Tetrominoes[0].YCoordinate += 1;
            }


            // TODO: Add your update logic here

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

            spriteBatch.Begin();

            //Board.Draw(spriteBatch);

            //Tetrominoes[0].Draw(spriteBatch);

            foreach (IGameObject gameObject in GameObjects)
            {
                gameObject.Draw(spriteBatch);
            }


            spriteBatch.End();

            

            base.Draw(gameTime);
        }
    }
}
