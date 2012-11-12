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


        public Position(params Vector2[] positions)
        {
            
        }

        Vector2 GetPosition(int dimension)
        {
            return new Vector2(0,0);
        }

        void SetPosition(int dimesion, Vector2 position)
        {

        }

        void Move(int dimension, Vector2 velocity)
        {

        }
    }
}
