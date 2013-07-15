using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefactoringDemo.Steps.Step1
{
    public class Rental
    {
        public Movie Movie
        {
            get; 
            set;
        }

        public int DaysRented
        {
            get; 
            set;
        }
    }
}