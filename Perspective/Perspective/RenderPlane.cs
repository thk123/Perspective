using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Perspective
{
    class RenderPlane
    {
        Position planePosition;
        int xDimension;
        int yDimension;

        public void Render(Enemy enemy, DimensionalManager dimensionalManager, SpriteBatch spriteBatch)
        {
            Position enemyPosition = enemy.GetPosition();
            Vector2 planarPosition = new Vector2(enemyPosition.GetPosition(xDimension), enemyPosition.GetPosition(yDimension));

            float size = GetSize(enemy, dimensionalManager);

            //TODO: Render
        }

        private float GetSize(Enemy enemy, DimensionalManager dimensionalManager)
        {
            for (int i = 0; i < dimensionalManager.GetNumberOfActiveDimensions(); ++i)
            {
                if (i == xDimension || i == yDimension)
                {
                    continue; //we don't look at this plane as we are this plane!
                }

                //Does the sphere intersect with this plane eg in axis i (perpendicular to this plane) is the 
                //position of the enemy in i, +- its width, still intersecting with this plane
                //if it is not then the size is 0 (eg don't draw)
                if (Math.Abs(planePosition.GetPosition(i) - enemy.GetPosition().GetPosition(i)) >= enemy.GetWidth(i))
                {
                    return 0.0f;
                }
            }

            //Otherwise will be some deg 2 polynomial based on how far away it is in each dimension
            return 1.0f;
        }
    }
}
