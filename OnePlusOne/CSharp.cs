namespace OnePlusOne
{
    public static class CSharp
    {
        public static void Swap<T>(ref T i, ref T j)
        {
            T o = i;
            i = j;
            j = o;
        }
    }
}
