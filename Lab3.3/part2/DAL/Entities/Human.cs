using System;

namespace DAL.Entities
{
    [Serializable]
    public abstract partial class Human
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        protected Human() { } // Для серіалізації

        protected Human(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}