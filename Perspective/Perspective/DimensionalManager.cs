using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perspective
{
    class DimensionalManager
    {
        private int numberOfActiveDimensions;

        public DimensionalManager()
        {
            numberOfActiveDimensions = 1;
        }

        public void StartNewGame(int startingDimensionCount)
        {
            numberOfActiveDimensions = startingDimensionCount;
        }

        public int GetNumberOfActiveDimensions()
        {
            return numberOfActiveDimensions;
        }

        public bool CanIncreaseNumberOfActiveDimensions()
        {
            return false;
        }
        
        public void IncreaseNumberOfActiveDimensions()
        {
            if (CanIncreaseNumberOfActiveDimensions())
            {
                ++numberOfActiveDimensions;
            }
            else
            {
                Console.WriteLine("Error: Attempted to increase number of active dimensions when couldn't");
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
