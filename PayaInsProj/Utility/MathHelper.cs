using System;

namespace PayaInsProj.Utility
{
    public static class MathHelper
    {
        public static int Clamp(int value, int min, int max)
        {
            if (min > max) throw new ArgumentException("min must be <= max");
            return value < min ? min : value > max ? max : value;
        }

        public static double Clamp(double value, double min, double max)
        {
            if (min > max) throw new ArgumentException("min must be <= max");
            return value < min ? min : value > max ? max : value;
        }
    }
}