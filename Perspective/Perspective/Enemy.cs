using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perspective
{
    abstract class Enemy
    {
        protected Position pos;
        protected EnemyType type;
        protected int maxDimensionOfMovement;
        protected List<int> forwards = new List<int>();
        protected DateTime deathTime;

        protected static Random random = new Random();

        protected int damageAmount;

        protected Enemy(EnemyType type, Position pos, long lifeTime, int maxDimensionOfMovement, int damageAmount = 20)
        {
            this.pos = pos;
            this.type = type;
            this.maxDimensionOfMovement = maxDimensionOfMovement;
            deathTime = System.DateTime.Now.AddMilliseconds(lifeTime);
            this.damageAmount = damageAmount;
        }

        public virtual void Move(DimensionalManager dm)
        {
            while (forwards.Count <= dm.GetNumberOfActiveDimensions())
            {
                if (forwards.Count >= maxDimensionOfMovement) { break; }
                forwards.Add(1);
            }
        }

        public Position GetPosition()
        {
            return pos;
        }

        public EnemyType GetEnemyType()
        {
            return type;
        }

        public float GetWidth()
        {
            return 16.0f; //return a standard width
        }

        public int GetDamageAmount()
        {
            return damageAmount;
        }
    }

    public enum EnemyType
    {
        StraightLine,
        ZigZag0,
        ZigZag1
    }
}
