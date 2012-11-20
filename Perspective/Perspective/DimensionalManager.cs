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
        EnemyManager enemyManager;

        float currentRenderPlaneWidth = 700;

        public DimensionalManager(EnemyManager enemyManager)
        {
            numberOfActiveDimensions = 0;
            renderPlanes = new List<RenderPlane>();
            //RenderPlane startingRenderPlane = new RenderPlane(new Rectangle(0,0,350,700), 2, 1, new Position(0.0f, 0.0f, 0.0f), this, Color.Red);
            //RenderPlane secondRenderPlane = new RenderPlane(new Rectangle(350, 0, 350, 700), 0, 1, new Position(0.0f, 0.0f, 0.0f), this, Color.Maroon);
            //secondRenderPlane.SetRender2D(true);
            //renderPlanes.Add(startingRenderPlane);
            //renderPlanes.Add(secondRenderPlane);

            this.enemyManager = enemyManager;
        }

        public void StartNewGame(int startingDimensionCount)
        {
            //numberOfActiveDimensions = startingDimensionCount;

            renderPlanes.Clear();
            for (int i = 0; i < startingDimensionCount; ++i)
            {
                addRenderPlane();
            }

            enemyManager.addEnemy(EnemyType.ZigZag0, new Position(0.0f, 0.0f, 0.0f) + new Position(30f, 30f));
            enemyManager.addEnemy(EnemyType.StraightLine, new Position(0.0f, 0.0f, 0.0f));
            enemyManager.addEnemy(EnemyType.ZigZag1, new Position(0.0f, 0.0f, 0.0f), 40 * 1000);

        }

        private void addRenderPlane()
        {
            if (numberOfActiveDimensions % 2 != 0)
            {
                renderPlanes.Last<RenderPlane>().SetRender2D(true);
            }
            else
            {
                //we resize all the existing render planes and add another 1d one
                if (renderPlanes.Count > 0)
                {
                    currentRenderPlaneWidth = currentRenderPlaneWidth * ((float)renderPlanes.Count / (float)(renderPlanes.Count + 1));
                }

                float xPos = 0;
                foreach (RenderPlane renderPlane in renderPlanes)
                {
                    Rectangle r = new Rectangle((int)xPos, 0, (int)currentRenderPlaneWidth, GetScreenHeight());
                    renderPlane.Resize(r);
                    xPos += currentRenderPlaneWidth;
                }

                Rectangle r2 = new Rectangle((int)xPos, 0, (int)currentRenderPlaneWidth, GetScreenHeight());
                RenderPlane newRenderPlane = new RenderPlane(r2, GetNumberOfActiveDimensions(), GetNumberOfActiveDimensions() + 1, this);

                renderPlanes.Add(newRenderPlane);
            }
            ++numberOfActiveDimensions;
        }

        public int GetNumberOfActiveDimensions()
        {
            return numberOfActiveDimensions;
        }

        public bool CanIncreaseNumberOfActiveDimensions()
        {
            return true;
        }
        
        public void IncreaseNumberOfActiveDimensions()
        {
            if (CanIncreaseNumberOfActiveDimensions())
            {
                addRenderPlane();
            }
            else
            {
                Console.WriteLine("Error: Attempted to increase number of active dimensions when couldn't");
            }
        }

        public int GetScreenHeight()
        {
            return 700;
        }

        public int GetScreenWidth()
        {
            return 700;
        }

        public void Update(GameTime gameTime, Player player)
        {
            foreach (RenderPlane renderplane in renderPlanes)
            {
                renderplane.UpdatePosition(player.getPosition());
            }
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            foreach (RenderPlane renderPlane in renderPlanes)
            {
                renderPlane.Render(enemyManager, player, spriteBatch);
            }
        }
    }
}
