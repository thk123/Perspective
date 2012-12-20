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
        public Texture2D logo;

        public static int SCREEN_WIDTH = 700;
        public static int SCREEN_HEIGHT = 700;

        GameState gameState;

        KeyboardState oldState;

        int fadeState;

        int fadeTime = 1 * 1000;

        int levelSelected = 1;

        int maxLevelUnlocked = 1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            Content.RootDirectory = "Content";

            gameState = GameState.fadeIn;
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
            gameState = GameState.fadeIn;
            fadeState = 0;
            oldState = Keyboard.GetState();
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
            logo = Content.Load<Texture2D>("Art//FrontEnd//logo");
            dimensionalManager = new DimensionalManager(enemyManager);
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
            switch (gameState)
            {
                case GameState.fadeIn:
                    {
                        if (CheckExit(oldState))
                        {
                            this.Exit();
                        }

                        if (fadeState > fadeTime)
                        {
                            gameState = GameState.frontEnd;
                        }
                        else
                        {
                            fadeState += gameTime.ElapsedGameTime.Milliseconds;
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            gameState = GameState.frontEnd;
                        }
                    }
                    break;

                case GameState.frontEnd:
                    {
                        if (CheckExit(oldState))
                        {
                            this.Exit();
                        }
                        //arrow and space handling
                        if (Keyboard.GetState().IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                        {
                            fadeState = 0;
                            gameState = GameState.fadeOut;
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                        {
                            if (levelSelected > 1)
                            {
                                --levelSelected;
                            }
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D))
                        {
                            if (levelSelected < maxLevelUnlocked)
                            {
                                ++levelSelected;
                            }
                        }
                    }
                    break;

                case GameState.fadeOut:
                    {
                        if (CheckExit(oldState))
                        {
                            this.Exit();
                        }
                        if (fadeState > fadeTime)
                        {
                            StartNewGame(levelSelected);
                            gameState = GameState.inGame;
                        }
                        else
                        {
                            fadeState += gameTime.ElapsedGameTime.Milliseconds;
                        }

                        if (fadeState >= 0 && Keyboard.GetState().IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                        {
                            gameState = GameState.inGame;
                            StartNewGame(levelSelected);
                        }
                    }
                    break;

                case GameState.inGame:
                    {
                        if (CheckExit(oldState))
                        {
                            if (maxLevelUnlocked < dimensionalManager.GetNumberOfActiveDimensions())
                            {
                                maxLevelUnlocked = dimensionalManager.GetNumberOfActiveDimensions();
                            }
                            levelSelected = maxLevelUnlocked;
                            gameState = GameState.fadeIn;
                            fadeState = 0;
                        }
                        dimensionalManager.Update(gameTime, player);

                        KeyboardState kboard = Keyboard.GetState();
                        if (kboard.IsKeyDown(Keys.R) && oldState.IsKeyUp(Keys.R))
                        {
                            StartNewGame();
                        }

                        player.detectInput(Keyboard.GetState(), gameTime, dimensionalManager);

                        enemyManager.Update(gameTime, dimensionalManager, player);

                        foreach (Enemy enemy in enemyManager.getEnemies())
                        {
                            if (CollisionManager.CheckCollision(player, enemy, dimensionalManager))
                            {
                                if (player.ApplyDamage(enemy.GetDamageAmount()))
                                {
                                    gameState = GameState.fadeIn;
                                    fadeState = 0;
                                    if (maxLevelUnlocked < dimensionalManager.GetNumberOfActiveDimensions())
                                    {
                                        maxLevelUnlocked = dimensionalManager.GetNumberOfActiveDimensions();
                                    }
                                    levelSelected = maxLevelUnlocked;
                                }
                            }
                        }

                    }
                    break;

            }
            oldState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.fadeIn:
                    {
                        float alpha = ((float)fadeState / (float)fadeTime);
                        spriteBatch.Draw(logo, Vector2.Zero, Color.White * alpha);
                        spriteBatch.DrawString(defaultFont14, levelSelected.ToString(), new Vector2(345, 324), Color.White * alpha);
                    }
                    break;
                case GameState.frontEnd:
                    {
                        spriteBatch.Draw(logo, Vector2.Zero, Color.White);
                        spriteBatch.DrawString(defaultFont14, levelSelected.ToString(), new Vector2(345, 324), Color.White);
                    }
                    break;

                case GameState.fadeOut:
                    {
                        float alpha = 1.0f - ((float)fadeState / (float)fadeTime);
                        spriteBatch.Draw(logo, Vector2.Zero, Color.White * alpha);
                        spriteBatch.DrawString(defaultFont14, levelSelected.ToString(), new Vector2(345, 324), Color.White * alpha);
                    }
                    break;

                case GameState.inGame:
                    {

                        //draw the player underneath enemies so can see what is killing you
                        drawLocation(spriteBatch, player.getPosition());
                        dimensionalManager.Draw(spriteBatch, player);

                        String playerHealthString = "Health: " + player.GetCurrentHealth();
                        Vector2 playerHealthStringPosition = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT) - defaultFont14.MeasureString(playerHealthString);
                        spriteBatch.DrawString(defaultFont14, playerHealthString, playerHealthStringPosition, Color.Red);

                    }
                    break;
            }

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

        private void StartNewGame(int startLevel = 1)
        {
            enemyManager.reset();
            dimensionalManager.StartNewGame(startLevel);
            player.reset();
        }

        private bool CheckExit(KeyboardState oldState)
        {
            return Keyboard.GetState().IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape);
        }

        enum GameState
        {
            fadeIn,
            frontEnd,
            fadeOut,
            inGame,
            dead,
        }
    }
}
