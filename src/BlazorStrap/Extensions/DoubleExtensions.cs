namespace BlazorStrap
{
    public static class DoubleExtensions
    {
        public static double RemoveNegative(this double value)
        {
            if (value < 0)
                return 0;
            return value;
        }
    }
}