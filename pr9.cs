using System;

namespace Fleet_management_system
{
    class Program
    {
        delegate void StringDelegate(string message);

        static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        static void Main()
        {
            StringDelegate del = new StringDelegate(PrintMessage);
            del("Hi!");
        }
    }
}
