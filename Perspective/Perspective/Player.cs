﻿using System;
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
        int currentDimension = 0;
        Position pos;
        float velocity = 2.0f;

        private const int dimensionsSwitch = 2;

        KeyboardState oldState;

        private const float startingHealth = 100.0f;

        private float currentHealth;

        public Player(Position pos)
        {
            this.pos = pos;
            oldState = Keyboard.GetState();

            currentHealth = startingHealth;
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

        public void detectInput(KeyboardState kboard, DimensionalManager dm)
        {
            if (kboard.IsKeyDown(Keys.A))
            {
                pos.Move(currentDimension, -velocity);
            }
            if (kboard.IsKeyDown(Keys.D))
            {
                pos.Move(currentDimension, velocity);
            }

            if (dm.GetNumberOfActiveDimensions() > currentDimension + 1)
            {
                if (kboard.IsKeyDown(Keys.W))
                {
                    pos.Move(currentDimension + 1, -velocity);
                }
                if (kboard.IsKeyDown(Keys.S))
                {
                    pos.Move(currentDimension + 1, velocity);
                }
            }

            if (dm.GetNumberOfActiveDimensions() > currentDimension + 3)
            {
                if(kboard.IsKeyDown(Keys.Up))
                {
                    pos.Move(currentDimension + 3, -velocity);
                }
                if (kboard.IsKeyDown(Keys.Down))
                {
                    pos.Move(currentDimension + 3, velocity);
                }
            }

            if (dm.GetNumberOfActiveDimensions() > currentDimension + 2)
            {
                if (kboard.IsKeyDown(Keys.Right))
                {
                    pos.Move(currentDimension + 2, velocity);
                }
                if (kboard.IsKeyDown(Keys.Left))
                {
                    pos.Move(currentDimension + 2, -velocity);
                }
            }

            if (kboard.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) )
            {
                dm.IncreaseNumberOfActiveDimensions();
            }

            
            if (kboard.IsKeyDown(Keys.Tab) && oldState.IsKeyUp(Keys.Tab))
            {
                currentDimension = currentDimension + dimensionsSwitch <= dm.GetNumberOfActiveDimensions() ? currentDimension + dimensionsSwitch : 0;
            }

            oldState = kboard;
            
        }

        public int getCurrentDimension()
        {
            return currentDimension;
        }
    }

    
}
