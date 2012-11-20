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
        List<int> forwards = new List<int>();
        DateTime deathTime;

        public Enemy(Position pos, EnemyType type, long lifeTime = 1000 * 10)
        {
            this.pos = pos;
            this.type = type;
            deathTime = System.DateTime.Now.AddMilliseconds(lifeTime);
        }

        public void Move(DimensionalManager dm)
        {
            while (forwards.Count <= dm.GetNumberOfActiveDimensions())
            {
                forwards.Add(1);
            }
            switch (type)
            {
                case EnemyType.StraightLine:
                    {
                        for (int i = 0; i < dm.GetNumberOfActiveDimensions(); ++i)
                        {
                            pos.Move(i, forwards[i] * 0.1f);
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
                case EnemyType.ZigZag0:
                    {
                        zigZag(1, dm.GetNumberOfActiveDimensions());
                        break;
                    }
                case EnemyType.ZigZag1 :
                    {
                        zigZag(0, dm.GetNumberOfActiveDimensions());
                        break;
                    }
            }
        }

        private void zigZag(int i, int numberOfDims)
        {
            for (; i <= numberOfDims; i += 2)
            {
                pos.Move(i, forwards[i] * 1f);
                if (Math.Abs(pos.GetPosition(i)) >= 3 * GetWidth() && System.DateTime.Now.CompareTo(deathTime) < 0)
                {
                    forwards[i] = -forwards[i];
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

        public float GetWidth()
        {
            return 8.0f; //return a standard width
        }
    }
}
