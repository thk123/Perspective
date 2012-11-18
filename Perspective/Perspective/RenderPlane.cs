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

        //Color bg;

        public RenderPlane(Rectangle screenPosition, int xDimension, int yDimension, DimensionalManager dimensionalManager)
        {
            this.xDimension = xDimension;
            this.yDimension = yDimension;

            planePosition = new Position(yDimension);
            this.dimensionalManager = dimensionalManager;

            render2d = false;
            renderTarget = screenPosition;

            //this.bg = backgroundColour;
        }

        public void UpdatePosition(Position newPosition)
        {
            planePosition = newPosition;
        }

        public void Resize(Rectangle newRenderPlane)
        {
            renderTarget = newRenderPlane;
        }

        public void Render(EnemyManager enemyManager, Player player, SpriteBatch spriteBatch)
        {
            //Draw background square
          //  spriteBatch.Draw(Game1.square, renderTarget, bg);

            Position playerPos = player.getPosition();
            Vector2 pos = GetScaledPosition(getPlanarPosition(playerPos));

            renderPosition(spriteBatch, pos, Color.Aquamarine, 32);

            foreach (Enemy enemy in enemyManager.getEnemies())
            {

                Position enemyPosition = enemy.GetPosition();
                Vector2 planarPosition = getPlanarPosition(enemyPosition);

                //if we are rendering in 1D, the GetSize takes in to account the distance in the y axis
                float size = GetSize(enemy);

                if (IsInRenderFrame(planarPosition, size))
                {
                    Vector2 drawPosition = GetScaledPosition(planarPosition);

                    switch (enemy.GetEnemyType())
                    {
                        case EnemyType.StraightLine:
                            {
                                renderPosition(spriteBatch, drawPosition, Color.Red, size);
                                break;
                            }
                        case EnemyType.Random:
                            {
                                renderPosition(spriteBatch, drawPosition, Color.Black, size);
                                break;
                            }
                    }
                }
            }
        }

        private void renderPosition(SpriteBatch spriteBatch, Vector2 pos, Color colour, float size)
        {
            if (render2d)
            {
                Rectangle drawRectangle = new Rectangle((int)(pos.X - size / 2), (int)(pos.Y - size / 2), (int)size, (int) size);
                spriteBatch.Draw(Game1.circle, drawRectangle, colour);
            }
            else
            {
                Rectangle drawRectangle = new Rectangle((int)(pos.X - size / 2), (int)(pos.Y - renderTarget.Height / 2), (int)size, renderTarget.Height);
                spriteBatch.Draw(Game1.square, drawRectangle, colour);
            }
        }

        private Vector2 getPlanarPosition(Position enemyPosition)
        {
            Vector2 planarPosition;
            if (render2d)
            {
                planarPosition = new Vector2(enemyPosition.GetPosition(xDimension), enemyPosition.GetPosition(yDimension));
            }
            else
            {
                planarPosition = new Vector2(enemyPosition.GetPosition(xDimension), 0);
            }
            return planarPosition;
        }

        private float GetSize(Enemy enemy)
        {
            float totalDistance = 0;
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
                float dimOffset = Math.Abs(planePosition.GetPosition(i) - enemy.GetPosition().GetPosition(i));
                if (dimOffset >= enemy.GetWidth(i))
                {
                    return 0.0f;
                }
                else
                {
                    totalDistance += (dimOffset * dimOffset);
                }
            }

            //Otherwise will be some deg 2 polynomial based on how far away it is in each dimension

            float radius = /*(float)Math.Pow(2.0,  dimensionalManager.GetNumberOfActiveDimensions() -1)  */ (float)Math.Sqrt(enemy.GetWidth(xDimension) * enemy.GetWidth(xDimension) - totalDistance);
            return radius;
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
