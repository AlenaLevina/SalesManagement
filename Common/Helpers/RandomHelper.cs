using System;

namespace Common.Helpers
{
    public class RandomHelper
    {
        public static int GenerateNumber(int length)
        {
            var random = new Random();
            var minValue = Math.Pow(10, length - 1);
            var maxValue = Math.Pow(10, length) - 1;
            if (maxValue <= Int32.MaxValue) return (new Random()).Next(Convert.ToInt32(minValue), Convert.ToInt32(maxValue));
            return -1;
        }
    }
}
