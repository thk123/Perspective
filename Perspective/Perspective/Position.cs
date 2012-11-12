using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Perspective
{
    /// <summary>
    /// A class to represent the position of something in n dimensions
    /// </summary>
    class Position
    {
        private const float defaultPosition = 0;

        List<float> positions;

        public Position(params float[] startingPositions)
        {
            positions = new List<float>(startingPositions);
        }

        public Position(int dimensions)
        {
            positions = new List<float>(dimensions);

            for (int i = 0; i < dimensions; ++i)
            {
                positions[i] = defaultPosition;
            }
        }

        float GetPosition(int dimension)
        {
            if (dimension > positions.Count)
            {
                return defaultPosition;
            }
            else
            {
                return positions[dimension];
            }

        }

        void SetPosition(int dimesion, float position)
        {
            if (dimesion > positions.Count)
            {
                while (dimesion > positions.Count - 1)
                {
                    positions.Add(defaultPosition);
                }

                positions.Add(position);
            }
            else
            {
                positions[dimesion] = position;
            }
        }

        void Move(int dimension, float velocity)
        {
            SetPosition(dimension, GetPosition(dimension) + velocity);
        }
    }
}
