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
            /*Random random = new Random();
            for (int i = 0; i < dm.GetNumberOfActiveDimensions(); ++i)
            {
                pos.Move(i, random.Next());
            }*/
            pos.Move(1, 0.1f);
            pos.Move(0, 0.1f);
            //pos.Move(2, 0.1f);
        }

        public Position GetPosition()
        {
            return pos;
        }

        public float GetWidth(int dimension)
        {
            return 32.0f; //return a standard width
        }
    }
}
