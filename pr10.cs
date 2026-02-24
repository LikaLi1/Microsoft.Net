using System;

namespace Fleet_management_system
{
    class Program
    {
        delegate int IntDelegate(int a, int b);

        static int PrintAdd(int a, int b)
        {
            return a + b;
        }

        static int PrintMul(int a, int b)
        {
            return a* b;
        }

        static void Main()
        {
            int num1 = 8;
            int num2 = 2;
            IntDelegate del1 = PrintAdd;
            Console.WriteLine("Add: " + del1(num1, num2));

            IntDelegate del2 = PrintMul;
            Console.WriteLine("Mul: " + del2(num1, num2));
        }
    }
}
