using System.Globalization;
using System.Numerics;

namespace matrixMxN
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Menu m = new();
            m.Run();

        }
    }
}