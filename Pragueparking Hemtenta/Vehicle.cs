using System;
using System.Collections.Generic;
using System.Text;

namespace Pragueparking_Hemtenta
{
    class Vehicle
    {
        private readonly string regnr;
        private DateTime arrival;
        private VehicleType type; 

        public enum VehicleType
        {
            CAR, MC, EMPTY
        }

        public Vehicle(VehicleType type, string regnr, DateTime arrival)
        {
            this.type = type;
            this.regnr = regnr;
            this.arrival = arrival;
        }        

        public string Regnr
        {
            get
            {
                return regnr;
            }
        }
        public DateTime Arrival
        {
            get
            {
                return arrival;
            }
        }
        public VehicleType Type
        {
            get
            {
                return type;
            }
        }
        public override string ToString()
        {
            return arrival.ToString() + "_" + type + "_" + regnr;

        }
    }
}
