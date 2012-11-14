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

        public void addEnemy(Enemy n)
        {
            enemies.Add(n);
        }

        public void removeEnemy(Enemy n)
        {
            enemies.Remove(n);
        }

        public void MoveEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                //enemy.Move();
            }
        }
    }
}
