using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perspective
{
    class CollisionManager
    {
        public static bool CheckCollision(Player p, Enemy e, DimensionalManager dm)
        {
            int activeDimensions = dm.GetNumberOfActiveDimensions();
            
            Position playerPostion = p.getPosition();
            Position enemyPosition = e.GetPosition();

            float playerWidth = p.getRadius();
            float enemyRadius = e.GetWidth();

            float totalDistance = 0.0f;

            for (int i = 0; i < activeDimensions; ++i)
            {
                float distance = Math.Abs(playerPostion.GetPosition(i) - enemyPosition.GetPosition(i));
                totalDistance += (distance * distance);
                
            }

            return totalDistance < ((playerWidth + enemyRadius) * (playerWidth + enemyRadius));
        }
    }
}
