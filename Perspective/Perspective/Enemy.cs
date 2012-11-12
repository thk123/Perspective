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

        public void Move()
        {
            Random random = new Random();
            for (int i = 0; i < DimensionalManager.GetNumberOfActiveDimensions(); ++i)
            {
                pos.Move(i, random.Next());
            }
        }
    }
}
