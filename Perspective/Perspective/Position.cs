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

        public Position(params float[] positions)
        {
            positions = new List<float>(positions.Length);
        }

        float GetPosition(int dimension)
        {
            return defaultPosition;
        }

        void SetPosition(int dimesion, float position)
        {

        }

        void Move(int dimension, float velocity)
        {

        }
    }
}
