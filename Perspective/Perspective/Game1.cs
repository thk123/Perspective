using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Perspective
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        DimensionalManager dimensionalManager;
        Player player;
        EnemyManager enemyManager;

        //ugly
        public static Texture2D cirlce;
        public static Texture2D square;
        public static int SCREEN_WIDTH = 700;
        public static int SCREEN_HEIGHT = 700;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            Content.RootDirectory = "Content";

            dimensionalManager = new DimensionalManager();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            StartNewGame();
            base.Initialize();

            player = new Player(new Position(1));
            enemyManager = new EnemyManager();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Game1.cirlce = Content.Load<Texture2D>("Art//WhiteCircle");
            Game1.square = Content.Load<Texture2D>("Art//WhiteSquare");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            if (CheckExit())
            {
                this.Exit();
            }

            dimensionalManager.Update(gameTime);

            player.detectInput(Keyboard.GetState());
            enemyManager.removeEnemies(dimensionalManager);
            enemyManager.MoveEnemies(dimensionalManager);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            dimensionalManager.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void StartNewGame()
        {
            dimensionalManager.StartNewGame(1);
        }

        private bool CheckExit()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Escape);
        }
    }
}
