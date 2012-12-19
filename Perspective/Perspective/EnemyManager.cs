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

        public void reset()
        {
            enemies.Clear();
            timeSinceLastDimensionChange = 0;
            timeSinceLastSpawn = 0;
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
                    Console.WriteLine("Removing enemy: " + en.ToString());
                    removeEnemy(en);
                }
            }
        }

        public void addEnemy(EnemyType type, Position pos, int lifetime = 10 * 1000, int maxDimensionOfMovement = int.MaxValue)
        {
            Enemy n;
            switch (type)
            {
                case EnemyType.ZigZag0:
                    {
                        n = new ZigZag(type, pos, 0, lifetime, maxDimensionOfMovement);
                        break;
                    }
                case EnemyType.ZigZag1:
                    {
                        n = new ZigZag(type, pos, 1, lifetime, maxDimensionOfMovement);
                        break;
                    }
                case EnemyType.StraightLine:
                    {
                        n = new StraightLineEnemy(type, pos, lifetime, maxDimensionOfMovement);
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
            enemies.Add(n);
        }

        public void removeEnemy(Enemy n)
        {
            enemies.Remove(n);
        }

        public void Update(GameTime gameTime, DimensionalManager dm, Player player)
        {
            timeSinceLastSpawn += gameTime.ElapsedGameTime.Milliseconds;
            timeSinceLastDimensionChange += gameTime.ElapsedGameTime.Milliseconds;
            
            {
                int spawn = Random.Next(-randomPrecision, randomPrecision);
                if (timeSinceLastSpawn + spawn > averageTimeBetweenSpawn)
                {
                    SpawnEnemy(dm, player);
                    timeSinceLastSpawn = 0;
                }
            }

            {
                int spawn = Random.Next(-randomPrecision, randomPrecision);
                if (timeSinceLastDimensionChange + spawn > averageTimeBetweenDimChange)
                {
                    AddDimension();
                    timeSinceLastDimensionChange = 0;
                }
            }

            removeEnemies(dm);
            MoveEnemies(dm);
        }

        private void SpawnEnemy(DimensionalManager dm, Player player)
        {
            int enemyType = Random.Next(Enum.GetNames(typeof(EnemyType)).Length);
            Position playerPos = player.getPosition();

            float[] position = new float[dm.GetNumberOfActiveDimensions()];
            double offSet;
            Random random = new Random();
            for(int i = 0; i< position.Length; ++i)
            {
                do
                {
                    offSet = random.NextDouble() - 0.5;
                    position[i] =
                        i % 2 == 0 ?
                        (float)(playerPos.GetPosition(i) + dm.GetScreenWidth() * offSet) :
                        (float)(playerPos.GetPosition(i) + dm.GetScreenHeight() * offSet);
                }
                while (Math.Abs(playerPos.GetPosition(i) - position[i]) < player.getRadius());
            }
   
            addEnemy(
                (EnemyType)enemyType,
                new Position(position)
                );
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
