using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Adventure_Game
{
       
    internal class Planet
    {
        public string Name { get; set; }
        public List<Cargo> AvailableCargo { get; private set; }
        public bool RefuelingStation { get; set; }

        //Constructor
        public Planet(string name, bool refuelingStation)
        {
            Name = name;
            RefuelingStation = refuelingStation;
            AvailableCargo = new List<Cargo>();
        }

        public void AddCargo(Cargo item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Cargo item cannot be null");
            }
            else
            {
                AvailableCargo.Add(item);
            }
        }

        public void RemoveCargo(Cargo item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Cargo item cannot be null");
            }
            if (!AvailableCargo.Contains(item))
            {
                throw new InvalidOperationException("Item Doenst Exist");
            }
            else
            {
                AvailableCargo.Remove(item);
            }
        }

        public void RefuelSpacehip(SpaceShip spaceShip, double amount)
        {
            if (spaceShip == null)
            {
                throw new ArgumentNullException(nameof(spaceShip), "Spaceship cannot be null");
            }
            if (!RefuelingStation)
            {
                throw new InvalidOperationException($"{Name} is not a refueling station");
            }
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Fuel amount must be greater than 0");
            }
            else
            {
                spaceShip.Fuel += amount;
            }

        }
    }
}
