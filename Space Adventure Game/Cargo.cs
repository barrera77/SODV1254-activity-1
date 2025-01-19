using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Adventure_Game
{
    public class Cargo
    {
        public string Name { get; set; }
        public double Weight { get; set; }  

        //Constructor
        public Cargo(string name, double weight) 
        {
            Name = name;
            Weight = weight;
        }

    }
}
