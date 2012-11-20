using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perspective
{
    class ZigZag : Enemy
    {
        int dimToMoveIn;
        public ZigZag(EnemyType type, Position pos, int dimToMoveIn, long lifeTime, int maxDimensionOfMovement) 
            : base(type, pos, lifeTime, maxDimensionOfMovement)
        {
            this.dimToMoveIn = dimToMoveIn;
        }

        public void Move(DimensionalManager dm)
        {
            base.Move(dm);
            for (int i = dimToMoveIn; i <= dm.GetNumberOfActiveDimensions(); i += 2)
                {
                    pos.Move(i, forwards[i] * 1f);
                    if (Math.Abs(pos.GetPosition(i)) >= 3 * GetWidth(i) && System.DateTime.Now.CompareTo(deathTime) < 0)
                    {
                        forwards[i] = -forwards[i];
                    }
                }
        }
    }
}
