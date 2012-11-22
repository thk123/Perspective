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

        public const float defaultPosition = 0;

        List<float> positions;

        public Position(params float[] startingPositions)
        {
            positions = new List<float>(startingPositions);
        }

        public Position(int dimensions)
        {
            positions = new List<float>(dimensions);

            reset(dimensions);
        }

        public int GetNumberOfDimensions()
        {
            return positions.Count;
        }

        public float GetPosition(int dimension)
        {
            if (dimension >= positions.Count)
            {
                return defaultPosition;
            }
            else
            {
                return positions[dimension];
            }

        }

        public void SetPosition(int dimension, float position)
        {
            if (dimension >= positions.Count)
            {
                while (dimension > positions.Count - 1)
                {
                    positions.Add(defaultPosition);
                }

                positions.Add(position);
            }
            else
            {
                positions[dimension] = position;
            }
        }

        public void Move(int dimension, float velocity)
        {
            SetPosition(dimension, GetPosition(dimension) + velocity);
        }

        public override string ToString()
        {
            StringBuilder vector = new StringBuilder();
            vector.Append("{");
            for (int i = 0; i < positions.Count; ++i)
            {
                vector.Append(positions[i].ToString());
                vector.Append(", ");
            }

            vector.Append("...}");

            return vector.ToString();
        }

        public static Position operator +(Position position1, Position position2)
        {
            Position largerPos = position1.GetNumberOfDimensions() > position2.GetNumberOfDimensions() ? position1 : position2;
            Position smallerPos = largerPos.Equals(position1) ? position2 : position1;
            for(int current = 0; current < smallerPos.GetNumberOfDimensions(); ++current)
            {
                largerPos.SetPosition(current, position1.GetPosition(current) + position2.GetPosition(current));
            }
            return largerPos;
        }

        public void reset(int dimensions)
        {
            positions.Clear();

            for (int i = 0; i < dimensions; ++i)
            {
                positions.Add(defaultPosition);
            }
        }
    }
}
