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
        public static Rectangle plane = new Rectangle(-350, -350, 700, 700);
        

        Position planePosition;
        int xDimension;
        int yDimension;

        Rectangle renderTarget;
        bool render2d;

        DimensionalManager dimensionalManager;

        Color bg;

        public RenderPlane(Rectangle screenPosition, int xDimension, int yDimension, Position startingPosition, DimensionalManager dimensionalManager, Color backgroundColour)
        {
            this.xDimension = xDimension;
            this.yDimension = yDimension;

            planePosition = startingPosition;
            this.dimensionalManager = dimensionalManager;

            render2d = false;
            renderTarget = screenPosition;

            this.bg = backgroundColour;
        }

        public void UpdatePosition(Position newPosition)
        {
            planePosition = newPosition;
        }

        public void Render(Enemy enemy, SpriteBatch spriteBatch)
        {
            //Draw background square
            spriteBatch.Draw(Game1.square, renderTarget, bg);


            Position enemyPosition = enemy.GetPosition();
            Vector2 planarPosition;
            if (render2d)
            {
                planarPosition = new Vector2(enemyPosition.GetPosition(xDimension), enemyPosition.GetPosition(yDimension));
            }
            else
            {
                planarPosition = new Vector2(enemyPosition.GetPosition(xDimension), 0 );
            }

            //if we are rendering in 1D, the GetSize takes in to account the distance in the y axis
            float size = GetSize(enemy);

            if (IsInRenderFrame(planarPosition, size))
            {
                Vector2 drawPosition = GetScaledPosition(planarPosition);

                spriteBatch.Draw(Game1.cirlce, drawPosition, Color.Red);
            }
        }

        private float GetSize(Enemy enemy)
        {
            for (int i = 0; i <= dimensionalManager.GetNumberOfActiveDimensions(); ++i)
            {
                //We don't look in our plane, but if this plane is currently 1D, then the y axis also contributes to size like any other perp dim
                if (i == xDimension || (i == yDimension && render2d))
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

        private bool IsInRenderFrame(Vector2 planarPosition, float size)
        {
            return plane.Contains(new Point((int)planarPosition.X, (int)planarPosition.Y)) && size > 0;
        }

        private Vector2 GetScaledPosition(Vector2 absolutePosition)
        {
            Vector2 absolutePositionOffset = absolutePosition - new Vector2(plane.X, plane.Y);
            float xPos = (float)renderTarget.X + ((absolutePositionOffset.X * (float)renderTarget.Width) / (float)plane.Width);
            float yPos = (float)renderTarget.Y + ((absolutePositionOffset.Y * (float)renderTarget.Height) / (float)plane.Height);

            return new Vector2(xPos, yPos);
        }

        public void SetRender2D(bool render2D)
        {
            this.render2d = render2D;
        }
    }
}
