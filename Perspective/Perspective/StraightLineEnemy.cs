using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perspective
{
    class StraightLineEnemy : Enemy
    {
        List<float> speed;
        public StraightLineEnemy(EnemyType type, Position pos, long lifeTime, int maxDimensionOfMovement) : base(type, pos, lifeTime, maxDimensionOfMovement)
        {
            speed = new List<float>();
        }

        public override void Move(DimensionalManager dm)
        {
            while (speed.Count <= dm.GetNumberOfActiveDimensions())
            {
                if (speed.Count >= maxDimensionOfMovement) { break; }
                speed.Add((float)random.NextDouble());
            }
            base.Move(dm);
            for (int i = 0; i < dm.GetNumberOfActiveDimensions(); ++i)
            {
                pos.Move(i, forwards[i] * speed[i]);
                if (random.Next(50) == 0 && i > 1)
                {
                    {
                        float temp = speed[i - 1];
                        speed[i - 1] = speed[i];
                        speed[i] = temp;
                    }
                    {
                        int temp = forwards[i - 1];
                        forwards[i - 1] = forwards[i];
                        forwards[i] = temp;
                    }
                }
            }
        }
        
    }
}
