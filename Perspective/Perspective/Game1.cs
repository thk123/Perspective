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
        public static Texture2D circle;
        public static Texture2D square;
        public static SpriteFont defaultFont14;

        public static int SCREEN_WIDTH = 700;
        public static int SCREEN_HEIGHT = 700;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            player = new Player(new Position(1));
            enemyManager = new EnemyManager();
            
            
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

            Game1.circle = Content.Load<Texture2D>("Art//WhiteCircle");
            Game1.square = Content.Load<Texture2D>("Art//WhiteSquare");
            defaultFont14 = Content.Load<SpriteFont>("DeafultFont14");

            dimensionalManager = new DimensionalManager(enemyManager);

            StartNewGame();
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

            dimensionalManager.Update(gameTime, player);

            KeyboardState kboard = Keyboard.GetState();
            if (kboard.IsKeyDown(Keys.R))
            {
                StartNewGame();
            }

            player.detectInput(Keyboard.GetState(), gameTime, dimensionalManager);

            enemyManager.Update(gameTime, dimensionalManager);

            foreach (Enemy enemy in enemyManager.getEnemies())
            {
                if (CollisionManager.CheckCollision(player, enemy, dimensionalManager))
                {
                    if(player.ApplyDamage(enemy.GetDamageAmount()))
                    {
                        Console.WriteLine("DIE");
                    }
                }
            }

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

            dimensionalManager.Draw(spriteBatch, player);

            drawLocation(spriteBatch, player.getPosition());

            String playerHealthString = "Health: " + player.GetCurrentHealth();
            Vector2 playerHealthStringPosition = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT) - defaultFont14.MeasureString(playerHealthString);
            spriteBatch.DrawString(defaultFont14, playerHealthString, playerHealthStringPosition, Color.Red);

            spriteBatch.End();
            base.Draw(gameTime);
        }


        private void drawLocation(SpriteBatch spriteBatch, Position position)
        {
            int getPlayerDimension = player.getCurrentDimension();
            Vector2 stringPosition = Vector2.Zero;
            spriteBatch.DrawString(defaultFont14, "{", stringPosition, Color.White);
            stringPosition.X += defaultFont14.MeasureString("{").X;

            string startingPoint = position.GetPosition(0).ToString();
            spriteBatch.DrawString(defaultFont14, startingPoint, stringPosition, getIndexColour(0, getPlayerDimension));
            stringPosition.X += defaultFont14.MeasureString(startingPoint).X;


            for (int i = 1; i < dimensionalManager.GetNumberOfActiveDimensions(); ++i)
            {
                string point = ", " + position.GetPosition(i).ToString();
                spriteBatch.DrawString(defaultFont14, point, stringPosition, getIndexColour(i, getPlayerDimension));
                stringPosition.X += defaultFont14.MeasureString(point).X;
            }

            spriteBatch.DrawString(defaultFont14, "}", stringPosition, Color.White);
        }

        private Color getIndexColour(int index, int playerIndex)
        {
            if (index == playerIndex || index == playerIndex + 1)
            {
                return Color.Red;
            }
            else if (index == playerIndex + 2 || index == playerIndex + 3)
            {
                return Color.Blue;
            }
            else
            {
                return Color.White;
            }
        }

        private void StartNewGame()
        {
            enemyManager.reset();
            dimensionalManager.StartNewGame(1);
            player.reset();
        }

        private bool CheckExit()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Escape);
        }
    }
}
