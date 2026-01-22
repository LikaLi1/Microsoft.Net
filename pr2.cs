internal class Program
{
    static void Main()
    {
        var shop = new Shop();
        shop[0] = "Apple";
        shop[1] = "Banana";
        shop[2] = "Orange";

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Product {i}: {shop[i]}");
        }
    }
}

public class Shop
{
    private string[] _products = new string[100];

    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= _products.Length)
                throw new IndexOutOfRangeException();
            return _products[index];
        }
        set
        {
            if (index < 0 || index >= _products.Length)
                throw new IndexOutOfRangeException();
            _products[index] = value;


            if (index % 2 == 0)
            {
                Console.WriteLine("True");
            }
            else
            {
                Console.WriteLine("False");
            }

        }
    }

    public int ProductCount => _products.Length;
}
