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
            positions = new List<float>(startingPositions.Length);
        }

        public float GetPosition(int dimension)
        {
            return defaultPosition;
        }

        public void SetPosition(int dimesion, float position)
        {

        }

        public void Move(int dimension, float velocity)
        {

        }
    }
}
