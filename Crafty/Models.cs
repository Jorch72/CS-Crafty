using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Crafty
{
    public static class Models
    {
        public static float[] CubeVerticies = {
                -1,  1,  1,
                 1,  1,  1,
                 1, -1,  1,
                -1, -1,  1,
                -1, -1, -1,
                -1,  1, -1,
                 1,  1, -1,
                 1, -1, -1
            };

        public static byte[] CubeIndicies = {
                1, 0, 2, // Front
                2, 0, 3,
                3, 5, 4, // Left
                3, 0, 5,
                4, 5, 7, // Back
                5, 6, 7,
                6, 2, 7, // Right
                1, 2, 6,
                0, 1, 6, // Top
                0, 6, 5,
                3, 4, 7, // Bottom
                3, 7, 2
            };
    }
}
