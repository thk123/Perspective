using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perspective
{
    class Enemy
    {
        Position pos;
        public Enemy(Position pos)
        {
            this.pos = pos;
        }

        public void Move(DimensionalManager dm)
        {
            Random random = new Random();
            for (int i = 0; i < dm.GetNumberOfActiveDimensions(); ++i)
            {
                pos.Move(i, random.Next());
            }
        }

        public Position GetPosition()
        {
            return pos;
        }

        public float GetWidth(int dimension)
        {
            return 1.0f; //return a standard width
        }
    }
}
