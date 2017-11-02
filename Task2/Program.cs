using System;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Person john = new Person("Джон");
            Person bill = new Person("Билл");
            Person hugo = new Person("Хьюго");

            john.Came();
            bill.Came();
            hugo.Came();

            john.Leave();
            bill.Leave();
            hugo.Leave();

            Console.ReadKey();
        }
    }
}
