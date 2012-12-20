using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Perspective
{
    class Player
    {
        int currentDimension;
        Position pos;
        Vector2 velocityXY;
        Vector2 velocityZW;

        const float maxVelocity = 13.0f;
        const float acceleration = 13.0f;
        const float decelartion = 13.0f;

        private const int dimensionsSwitch = 4;

        KeyboardState oldState;

        private const float startingHealth = 100.0f;

        private float currentHealth;

        public Player(Position pos)
        {
            this.pos = pos;
            oldState = Keyboard.GetState();

            reset();
        }

        public Position getPosition()
        {
            return pos;
        }

        public float getRadius()
        {
            return 16.0f;
        }

        public bool ApplyDamage(float damage)
        {
            currentHealth -= damage;
            return currentHealth <= 0.0f;
        }

        public float GetCurrentHealth()
        {
            return currentHealth;
        }

        public void detectInput(KeyboardState kboard, GameTime gameTime, DimensionalManager dm)
        {
            /*if (kboard.IsKeyDown(Keys.A))
            {
                velocityXY.X -= acceleration * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f );
                velocityXY.X = Math.Min(velocityXY.X, -maxVelocity);
                //pos.Move(currentDimension, -velocity);
            }
            else if (kboard.IsKeyDown(Keys.D))
            {
                //pos.Move(currentDimension, velocity);
                velocityXY.X += acceleration * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f );
                velocityXY.X = Math.Max(velocityXY.X, maxVelocity);
            }
            else if(velocityXY.X > 0)
            {
                velocityXY.X -= decelartion * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f );
                velocityXY.X = Math.Max(velocityXY.X, 0);
            }
            else if(velocityXY.X < 0)
            {
                velocityXY.X += decelartion * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                velocityXY.X = Math.Min(velocityXY.X, 0);
            }*/

            if (kboard.IsKeyDown(Keys.A))
            {
                //pos.Move(currentDimension + 1, -velocity);
                velocityXY.X -= acceleration * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                velocityXY.X = Math.Max(velocityXY.X, -maxVelocity);
            }
            else if (kboard.IsKeyDown(Keys.D))
            {
                //pos.Move(currentDimension + 1, velocity);
                velocityXY.X += acceleration * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                velocityXY.X = Math.Min(velocityXY.X, maxVelocity);
            }
            else if (velocityXY.X < 0)
            {
                velocityXY.X += decelartion * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                velocityXY.X = Math.Min(velocityXY.X, 0);
            }
            else if (velocityXY.X > 0)
            {
                velocityXY.X -= decelartion * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                velocityXY.X = Math.Max(velocityXY.X, 0);
            }

            if (dm.GetNumberOfActiveDimensions() > currentDimension + 1)
            {
                if (kboard.IsKeyDown(Keys.W))
                {
                    //pos.Move(currentDimension + 1, -velocity);
                    velocityXY.Y -= acceleration * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f );
                    velocityXY.Y = Math.Max(velocityXY.Y, -maxVelocity);
                }
                else if (kboard.IsKeyDown(Keys.S))
                {
                    //pos.Move(currentDimension + 1, velocity);
                    velocityXY.Y += acceleration * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                    velocityXY.Y = Math.Min(velocityXY.Y, maxVelocity);
                }
                else if(velocityXY.Y < 0)
                {
                    velocityXY.Y += decelartion * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                    velocityXY.Y = Math.Min(velocityXY.Y, 0);
                }
                else if(velocityXY.Y > 0)
                {
                    velocityXY.Y -= decelartion * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                    velocityXY.Y = Math.Max(velocityXY.Y, 0);
                }
            }

            if (dm.GetNumberOfActiveDimensions() > currentDimension + 3)
            {
                if(kboard.IsKeyDown(Keys.Up))
                {
                    //pos.Move(currentDimension + 3, -velocityXY);
                    velocityZW.Y -= acceleration * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                    velocityZW.Y = Math.Max(velocityZW.Y, -maxVelocity);
                }
                else if (kboard.IsKeyDown(Keys.Down))
                {
                    //pos.Move(currentDimension + 3, velocityXY);
                    velocityZW.Y += acceleration * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                    velocityZW.Y = Math.Min(velocityZW.Y, maxVelocity);
                }
                else if (velocityZW.Y > 0)
                {
                    velocityZW.Y -= decelartion * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                    velocityZW.Y = Math.Max(velocityZW.Y, 0);
                }
                else if (velocityZW.Y < 0)
                {
                    velocityZW.Y += decelartion * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                    velocityZW.Y = Math.Min(velocityZW.Y, 0);
                }

            }

            if (dm.GetNumberOfActiveDimensions() > currentDimension + 2)
            {
                if (kboard.IsKeyDown(Keys.Right))
                {
                    //pos.Move(currentDimension + 2, velocityXY);
                    velocityZW.X += acceleration * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                    velocityZW.X = Math.Min(velocityZW.X, maxVelocity);
                }
                else if (kboard.IsKeyDown(Keys.Left))
                {
                    //pos.Move(currentDimension + 2, -velocityXY);
                    velocityZW.X -= acceleration * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                    velocityZW.X = Math.Max(velocityZW.X, -maxVelocity);
                }
                else if (velocityZW.X > 0)
                {
                    velocityZW.X -= decelartion * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                    velocityZW.X = Math.Max(velocityZW.X, 0);
                }
                else if (velocityZW.X < 0)
                {
                    velocityZW.X += decelartion * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);
                    velocityZW.X = Math.Min(velocityZW.X, 0);
                }
            }

            pos.Move(currentDimension, velocityXY.X);
            if(Math.Abs(pos.GetPosition(currentDimension)) > dm.GetScreenWidth() / 2)
            {
                pos.Move(currentDimension, -velocityXY.X);
                velocityXY.X = 0;
            }
            pos.Move(currentDimension + 1, velocityXY.Y);
            if (Math.Abs(pos.GetPosition(currentDimension + 1)) > dm.GetScreenHeight() / 2)
            {
                pos.Move(currentDimension + 1, -velocityXY.Y);
                velocityXY.Y = 0;
            }
            pos.Move(currentDimension + 2, velocityZW.X);
            if (Math.Abs(pos.GetPosition(currentDimension + 2)) > dm.GetScreenWidth() / 2)
            {
                pos.Move(currentDimension + 2, -velocityZW.X);
                velocityZW.X = 0;
            }
            pos.Move(currentDimension + 3, velocityZW.Y);
            if (Math.Abs(pos.GetPosition(currentDimension + 3)) > dm.GetScreenHeight() / 2)
            {
                pos.Move(currentDimension + 3, -velocityZW.Y);
                velocityZW.Y = 0;
            }

            if (kboard.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) )
            {
                if (dm.CanIncreaseNumberOfActiveDimensions())
                {
                    dm.IncreaseNumberOfActiveDimensions();
                }
            }

            
            if (kboard.IsKeyDown(Keys.Tab) && oldState.IsKeyUp(Keys.Tab))
            {
                currentDimension = currentDimension + dimensionsSwitch < dm.GetNumberOfActiveDimensions() ? currentDimension + dimensionsSwitch : 0;
            }

            oldState = kboard;
            
        }

        public int getCurrentDimension()
        {
            return currentDimension;
        }

        public void reset()
        {
            pos.reset(1);
            currentHealth = startingHealth;
            currentDimension = 0;
            velocityXY = Vector2.Zero;
            velocityZW = Vector2.Zero;
        }
    }

    
}
