using System;
using System.Collections.Generic;
using RefactoringDemo.Steps.Step1;
using System.Runtime.Serialization.Json;


namespace RefactoringDemo
{
    class Program
    {
        static void Main()
        {
            var temp = new Customer("Sergey",new List<Rental>());
            temp.Rentals.Add(new Rental(new Movie("Noi",new NewReleasePrice()), 10));
            temp.Rentals.Add(new Rental(new Movie("Macho and Botan", new RegularPrice()), 1));
            temp.Rentals.Add(new Rental(new Movie("Unbelievable", new NewReleasePrice()), 5));
            
            
            Console.WriteLine(temp.Statement());
        }
    }
}
