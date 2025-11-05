using System;
using MemoryPack;

namespace DAL.Entities
{
    [MemoryPackable]
    public partial class Passport
    {
        public string Series { get; set; }
        public int Number { get; set; }
        public string FullPassport => $"{Series}{Number:D6}";

        [MemoryPackConstructor]
        public Passport() { }

        public Passport(string series, int number)
        {
            Series = series;
            Number = number;
        }

        public bool IsValidPassport()
        {
            return System.Text.RegularExpressions.Regex.IsMatch(FullPassport, @"^[A-Z]{2}\d{6}$");
        }
    }
}