using System;
using MemoryPack;

namespace DAL.Entities
{
    [Serializable]
    [MemoryPackable]
    public partial class Student : Human, ISkill
    {
        public int Height { get; set; }
        public int Weight { get; set; }
        public string StudentID { get; set; }
        public Passport Passport { get; set; }

        private int _rideCount = 0;
        public int RideCount => _rideCount;

        public Student() { } // Для серіалізації

        [MemoryPackConstructor]
        public Student(string firstName, string lastName, int height, int weight, string studentID, Passport passport)
                    : base(firstName, lastName)
        {
            Height = height;
            Weight = weight;
            StudentID = studentID;
            Passport = passport;
        }

        public void RideBike()
        {
            _rideCount++;
        }

        public bool IsValidStudentID()
        {
            return System.Text.RegularExpressions.Regex.IsMatch(StudentID, @"^[A-Z]{2}\d{6}$");
        }

        // --- Додаємо ToString для CustomProvider ---
        public override string ToString()
        {
            return $"Student {FirstName}{LastName}\n{{ " +
                   $"\"FirstName\": \"{FirstName}\", " +
                   $"\"LastName\": \"{LastName}\", " +
                   $"\"Height\": {Height}, " +
                   $"\"Weight\": {Weight}, " +
                   $"\"StudentID\": \"{StudentID}\", " +
                   $"\"PassportSeries\": \"{Passport?.Series}\", " +
                   $"\"PassportNumber\": \"{Passport?.Number}\" }};";
        }
    }
}