using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefactoringDemo.Steps.Step3
{
    public class Movie
    {
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;

        public string Title
        {
            get; 
            set; 
        }

        public int PriceCode
        {
            get; 
            set;
        }
    }
}
