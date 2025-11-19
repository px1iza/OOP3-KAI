using System;
using DAL.Interfaces;

namespace DAL.Entities
{
    public class SoftwareDeveloper : Human, ISkill
    {
        private int _rideCount = 0;
        public int RideCount => _rideCount;

        public SoftwareDeveloper(string firstName, string lastName)
            : base(firstName, lastName) { }

        public void RideBike()
        {
            _rideCount++;
        }
    }
}