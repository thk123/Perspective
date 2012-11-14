using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perspective
{
    class Enemy
    {
        Position pos;
        EnemyType type;

        public Enemy(Position pos, EnemyType type)
        {
            this.pos = pos;
            this.type = type;
        }

        public void Move(DimensionalManager dm)
        {
            switch (type)
            {
                case EnemyType.StraightLine:
                    {
                        for (int i = 0; i < dm.GetNumberOfActiveDimensions(); ++i)
                        {
                            pos.Move(i, 0.1f);
                        }
                        break;
                    }
                case EnemyType.Random:
                    {
                        for (int i = 0; i < dm.GetNumberOfActiveDimensions(); ++i)
                        {
                            pos.Move(i, (new Random()).Next(-1, 1));
                        }
                        break;
                    }
            }
        }

        public EnemyType GetEnemyType()
        {
            return type;
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
