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

        public Player(Position pos)
        {
            this.pos = pos;
        }

        public void detectInput(KeyboardState kboard)
        {
            if (kboard.IsKeyDown(Keys.W))
            {
                pos.Move(currentDimension, -velocity);
            }
            if (kboard.IsKeyDown(Keys.S))
            {
                pos.Move(currentDimension, velocity);
            }

            if(true)
            {
                if (kboard.IsKeyDown(Keys.A))
                {
                    pos.Move(currentDimension + 1, velocity);
                }
                if (kboard.IsKeyDown(Keys.D))
                {
                    pos.Move(currentDimension + 1, -velocity);
                }
            }

            if(true/*&&enough dimensions*/)
            {
                if(kboard.IsKeyDown(Keys.Up))
                {
                    pos.Move(currentDimension + 2, -velocity);
                }
                if (kboard.IsKeyDown(Keys.Down))
                {
                    pos.Move(currentDimension + 2, velocity);
                }
            }
            if(true)
            {
                if (kboard.IsKeyDown(Keys.Right))
                {
                    pos.Move(currentDimension + 3, velocity);
                }
                if (kboard.IsKeyDown(Keys.Left))
                {
                    pos.Move(currentDimension + 3, -velocity);
                }
            }

            if (kboard.IsKeyDown(Keys.Space))
            {
                currentDimension = true ? currentDimension + 4 : 0; 
            }
            
        }
    }

    
}