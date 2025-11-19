using System;
using DAL.Interfaces;

namespace DAL.Entities
{
    public partial class Student : Human, ISkill
    {
        // Подія для нотифікації
        public event EventHandler<string> IdealWeightReached;

        public int Height { get; set; }

        public int Weight { get; private set; }

        public string StudentID { get; set; }
        public Passport Passport { get; set; }

        private int _rideCount = 0;
        public int RideCount => _rideCount;

        public Student() { }

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

        // Метод для зміни ваги (набір/зменшення)
        public void ChangeWeight(int amount)
        {
            Weight += amount;
            CheckIdealWeight();
        }

        private void CheckIdealWeight()
        {
            if (Height - 110 == Weight)
            {
                // Викликаємо подію, якщо вага ідеальна
                IdealWeightReached?.Invoke(this, $"Student {FirstName} has reached ideal weight: {Weight}!");
            }
        }

        public bool IsValidStudentID()
        {
            return System.Text.RegularExpressions.Regex.IsMatch(StudentID, @"^[A-Z]{2}\d{6}$");
        }

        public override string ToString()
        {
            return $"Student {FirstName} {LastName}, Height: {Height}, Weight: {Weight}";
        }
        public string StudyOnline(IInternetService internetService)
        {
            if (internetService.IsConnected)
            {
                return $"Student {FirstName} is studying online. Knowledge increased!";
            }
            else
            {
                return $"Student {FirstName} cannot study. No internet connection.";
            }
        }
    }
}