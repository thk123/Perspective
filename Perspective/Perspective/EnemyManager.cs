using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Perspective
{
    class EnemyManager
    {
        List<Enemy> enemies;

        public EnemyManager()
        {
            enemies = new List<Enemy>();
        }

        public void removeEnemies(DimensionalManager dm)
        {
            for (int j = 0; j<enemies.Count; ++j)
            {
                Enemy en = enemies.ElementAt<Enemy>(j);
                int count = 0;
                for (int i = 0; i < dm.GetNumberOfActiveDimensions(); ++i)
                {
                    if (Math.Abs(en.GetPosition().GetPosition(i) + 20) > (i % 2 == 0 ? dm.GetScreenHeight() : dm.GetScreenWidth()))
                    {
                        ++count;
                    }
                }
                if (count == dm.GetNumberOfActiveDimensions())
                {
                    removeEnemy(en);
                }
            }
        }

        public void addEnemy(EnemyType type, Position pos)
        {
            Enemy n = new Enemy(pos, type);
            enemies.Add(n);
        }

        public void removeEnemy(Enemy n)
        {
            enemies.Remove(n);
        }

        public void MoveEnemies(DimensionalManager dm)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Move(dm);
            }
        }

        public List<Enemy> getEnemies()
        {
            return enemies;
        }
    }

    public enum EnemyType
    {
        Random,
        StraightLine
    }
}
