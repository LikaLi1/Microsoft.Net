using Proga;
using System;

namespace Proga
{
    abstract class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }
        public double Grades { get; set; }

        public Student(string name, int age, string course, double grades)
        {
            Name = name;
            Age = age;
            Course = course;
            Grades = grades;
        }
    }

    class Excellent : Student
    {
        public double Homework { get; set; }
        public double Additional_classes { get; set; }

        public Excellent(string name, int age, string course, double grades, double homework, double additional_classes)
            : base(name, age, course, grades)
        {
            Homework = homework;
            Additional_classes = additional_classes;
        }
    }

    class Double : Student
    {
        public double Football { get; set; }
        public double Sleep { get; set; }

        public Double(string name, int age, string course, double grades, double football, double sleep)
            : base(name, age, course, grades)
        {
            Football = football;
            Sleep = sleep;
        }
    }
}

class Prog
{
    static void Main()
    {
        Student[] students = new Student[2];
        students[0] = new Proga.Excellent("Ivan", 20, "Math", 4.5, 8.0, 2.0);
        students[1] = new Proga.Double("Anna", 22, "Physics", 4.8, 5.0, 7.0);

        foreach (Student student in students)
        {
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Course: {student.Course}, Grades: {student.Grades}");
            if (student is Proga.Excellent excellent)
            {
                Console.WriteLine($"Homework: {excellent.Homework}, Additional classes: {excellent.Additional_classes}");
            }
            else if (student is Proga.Double dobble)
            {
                Console.WriteLine($"Football: {dobble.Football}, Sleep: {dobble.Sleep}");
            }
        }
    }
}
