using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Perspective
{
    class EnemyManager
    {
        ArrayList enemies;

        public EnemyManager()
        {
            enemies = new ArrayList();
        }

        public void removeEnemies(DimensionalManager dm)
        {
            foreach (Enemy en in enemies)
            {
                int count = 0;
                for (int i = 0; i < dm.GetNumberOfActiveDimensions(); ++i)
                {
                    if (en.GetPosition().GetPosition(i) + 20 > (i % 2 == 0 ? dm.GetScreenHeight() : dm.GetScreenWidth()))
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

        public void addEnemy(Enemy n)
        {
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
    }
}
