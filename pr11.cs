using Microsoft.VisualBasic;
using System;

namespace Fleet_management_system
{
    class Program
    {
        delegate void StringDelegate(string mes);

        static void Main()
        {
            StringDelegate del = (message) => Console.WriteLine("Message: " + message);
            del("Greate");
        }
    }
}
