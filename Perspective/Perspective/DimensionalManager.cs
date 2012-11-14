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

        List<RenderPlane> renderPlanes;
        Enemy a;
        public DimensionalManager()
        {
            numberOfActiveDimensions = 2;
            renderPlanes = new List<RenderPlane>();
            RenderPlane startingRenderPlane = new RenderPlane(new Rectangle(0,0,350,700), 0, 1, new Position(0.0f, 0.0f, 0.0f), this, Color.Red);
            RenderPlane secondRenderPlane = new RenderPlane(new Rectangle(350, 0, 350, 700), 0, 1, new Position(0.0f, 0.0f, 0.0f), this, Color.Maroon);
            secondRenderPlane.SetRender2D(true);
            renderPlanes.Add(startingRenderPlane);
            renderPlanes.Add(secondRenderPlane);
        }

        public void StartNewGame(int startingDimensionCount)
        {
            numberOfActiveDimensions = startingDimensionCount;

            a = new Enemy(new Position(0.0f, 0.0f, 0.0f));
        }

        public int GetNumberOfActiveDimensions()
        {
            return 3;
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
            a.Move(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (RenderPlane renderPlane in renderPlanes)
            {
                renderPlane.Render(a, spriteBatch);
            }
        }
    }
}
