using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perspective
{
    class StraightLineEnemy : Enemy
    {
        public StraightLineEnemy(EnemyType type, Position pos, long lifeTime, int maxDimensionOfMovement) : base(type, pos, lifeTime, maxDimensionOfMovement) {}

        public override void Move(DimensionalManager dm)
        {
            base.Move(dm);
            for (int i = 0; i < dm.GetNumberOfActiveDimensions(); ++i)
            {
                pos.Move(i, forwards[i] * random.Next(1));
            }
        }
        
    }
}
