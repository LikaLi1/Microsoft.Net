using System;
using System.Collections.Generic;

interface IFeedable
{
    void Feed();
}

abstract class Animal : IFeedable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public float Weight { get; set; }

    public Animal(string name, int age, float weight)
    {
        Name = name;
        Age = age;
        Weight = weight;
    }

    public abstract void MakeSound();

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Weight: {Weight}kg");
    }

    public abstract void Feed();

    public static bool operator >(Animal a1, Animal a2)
    {
        return a1.Age > a2.Age;
    }

    public static bool operator <(Animal a1, Animal a2)
    {
        return a1.Age < a2.Age;
    }

    public override bool Equals(object obj)
    {
        if (obj is Animal other)
            return this.Age == other.Age;
        return false;
    }

    public override int GetHashCode()
    {
        return Age.GetHashCode();
    }
}

class Mammal : Animal
{
    public Mammal(string name, int age, float weight) : base(name, age, weight) { }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} (Mammal) says: *Mammal sound*");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Type: Mammal");
    }

    public override void Feed()
    {
        Console.WriteLine($"{Name} the mammal is being fed with suitable mammal food.");
    }
}

class Bird : Animal
{
    public Bird(string name, int age, float weight) : base(name, age, weight) { }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} (Bird) says: Chirp Chirp!");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Type: Bird");
    }

    public override void Feed()
    {
        Console.WriteLine($"{Name} the bird is being fed with seeds.");
    }
}

class Reptile : Animal
{
    public Reptile(string name, int age, float weight) : base(name, age, weight) { }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} (Reptile) says: *Hiss*");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Type: Reptile");
    }

    public override void Feed()
    {
        Console.WriteLine($"{Name} the reptile is being fed with insects.");
    }
}

class ZooKeeper
{
    public void CareForAnimal(Animal animal)
    {
        Console.WriteLine($"Caring for {animal.Name}");
        animal.Feed();
    }
}

static class ZooStatistics
{
    private static int totalAnimals = 0;

    public static void RegisterAnimal()
    {
        totalAnimals++;
    }

    public static void ShowStatistics()
    {
        Console.WriteLine($"Total animals in the zoo: {totalAnimals}");
    }
}

class Zoo
{
    private List<Animal> animals = new List<Animal>();

    public List<Animal> Animals => animals;

    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
        ZooStatistics.RegisterAnimal();
        Console.WriteLine($"{animal.Name} added to the zoo.");
    }

    public void RemoveAnimal(Animal animal)
    {
        if (animals.Remove(animal))
            Console.WriteLine($"{animal.Name} removed from the zoo.");
        else
            Console.WriteLine($"{animal.Name} not found in the zoo.");
    }

    public void ShowAllAnimals()
    {
        Console.WriteLine("Animals in the zoo:");
        foreach (var animal in animals)
        {
            animal.DisplayInfo();
            Console.WriteLine();
        }
    }

    public void MakeAllSounds()
    {
        foreach (var animal in animals)
        {
            animal.MakeSound();
        }
    }

    public void ShowAllAnimalsInfo()
    {
        foreach (var animal in animals)
        {
            animal.DisplayInfo();
            Console.WriteLine();
        }
    }

    public void FeedAllAnimals()
    {
        foreach (var animal in animals)
        {
            animal.Feed();
        }
    }
}

class Program
{
    static void Main()
    {
        Zoo zoo = new Zoo();

        Mammal lion = new Mammal("Lion", 5, 190.5f);
        Bird parrot = new Bird("Parrot", 2, 0.9f);
        Reptile snake = new Reptile("Snake", 3, 4.5f);

        zoo.AddAnimal(lion);
        zoo.AddAnimal(parrot);
        zoo.AddAnimal(snake);

        zoo.ShowAllAnimals();
        zoo.MakeAllSounds();
        zoo.FeedAllAnimals();

        Console.WriteLine($"Is {lion.Name} older than {parrot.Name}? {lion > parrot}");
        Console.WriteLine($"Is {snake.Name} older than {lion.Name}? {snake > lion}");

        ZooKeeper keeper = new ZooKeeper();
        keeper.CareForAnimal(lion);

        ZooStatistics.ShowStatistics();
    }
}
