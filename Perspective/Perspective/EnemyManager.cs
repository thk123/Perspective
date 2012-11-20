using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;

namespace Perspective
{
    class EnemyManager
    {
        List<Enemy> enemies;

        int timeSinceLastSpawn;
        int timeSinceLastDimensionChange;

        const int averageTimeBetweenSpawn = 1 * 1000;
        const int averageTimeBetweenDimChange = 10 * 1000;
        const int randomPrecision = 1 * 1000;

        private static Random Random;

        public EnemyManager()
        {
            enemies = new List<Enemy>();

            timeSinceLastDimensionChange = 0;
            timeSinceLastSpawn = 0;

            Random = new Random();
        }

        public void StartNewGame()
        {
            timeSinceLastDimensionChange = 0;
            timeSinceLastSpawn = 0;
        }

        public void removeEnemies(DimensionalManager dm)
        {
            for (int j = 0; j<enemies.Count; ++j)
            {
                Enemy en = enemies.ElementAt<Enemy>(j);
                int count = 0;
                for (int i = 0; i < dm.GetNumberOfActiveDimensions(); ++i)
                {
                    if (Math.Abs(en.GetPosition().GetPosition(i)) > (i % 2 == 0 ? dm.GetScreenHeight() : dm.GetScreenWidth()))
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

        public void addEnemy(EnemyType type, Position pos, int lifeTime)
        {
            Enemy n = new Enemy(pos, type, lifeTime);
            enemies.Add(n);
        }

        public void removeEnemy(Enemy n)
        {
            enemies.Remove(n);
        }

        public void Update(GameTime gameTime, DimensionalManager dm)
        {
            timeSinceLastSpawn += gameTime.ElapsedGameTime.Milliseconds;
            timeSinceLastDimensionChange += gameTime.ElapsedGameTime.Milliseconds;
            
            {
                int distance = Math.Abs(timeSinceLastSpawn - averageTimeBetweenSpawn);
                int spawn = distance + Random.Next(-randomPrecision, randomPrecision);
                SpawnEnemy(dm.GetNumberOfActiveDimensions());
                timeSinceLastSpawn = 0;
            }

            {
                int distance = Math.Abs(timeSinceLastDimensionChange - averageTimeBetweenDimChange);
                int spawn = distance + Random.Next(-randomPrecision, randomPrecision);
                AddDimension();
                timeSinceLastDimensionChange = 0;
            }

            removeEnemies(dm);
            MoveEnemies(dm);
        }

        private void SpawnEnemy(int dimensions)
        {
            int choice = Random.Next(Enum.GetNames(typeof(EnemyType)).Length);
            addEnemy((EnemyType)choice, new Position(0.0f, 0.0f, 0.0f) + new Position(Random.Next(-350, 350), Random.Next(-350, 350)));
            //TODO: Write some codez
        }

        private void AddDimension()
        {
           //TODO: Write some codez
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
}
