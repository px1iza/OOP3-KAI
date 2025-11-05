using System;

namespace DAL.Entities
{
    public abstract class Human
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        protected Human() { } 

        protected Human(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}