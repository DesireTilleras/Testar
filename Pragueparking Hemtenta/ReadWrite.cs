using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Pragueparking_Hemtenta
{
    class ReadWrite
    {
                
        public Vehicle[] Read()
        {
            Vehicle[] parking = new Vehicle[200];

            if (!File.Exists("pragueparking.txt")) 
            {
                throw new FileNotFoundException("The file could not be found");
            }
            

            StreamReader sr = new StreamReader("pragueparking.txt");
            
            using (sr)
            {
                string line = "";

                for (int i = 0; i < parking.Length; i++)
                {
                    line = sr.ReadLine();
                    parking[i] = DesignDocument(line);
                }


            }
            return parking;


        }
        public void Write (Vehicle[] parking)
        {
            if (!File.Exists("pragueparking.txt")) 
            {
                Console.WriteLine("The file 'pragueparking.txt' does not exist. Creating a file");
            }
            StreamWriter sw = new StreamWriter("pragueparking.txt"); // En ny fil skapas.
            using (sw)
            {
                foreach (Vehicle vehicle in parking) 
                {
                    sw.WriteLine(vehicle.ToString());
                }
                sw.Flush();
                
            }
        }
        public Vehicle DesignDocument (string input) // How I want the txt document to be designed. 
        {
            string [] arrayPosition = new string[3];
            arrayPosition = input.Split('_'); 
            DateTime tempDate = DateTime.Parse(arrayPosition[0]);
            Vehicle.VehicleType type;

            if (arrayPosition[1] == "CAR") 
            {
                type = Vehicle.VehicleType.CAR;
            }
            else if (arrayPosition[1] == "MC") 
            {
                type = Vehicle.VehicleType.MC;
            }
            else
            {
                type = Vehicle.VehicleType.EMPTY; 
            }
            Vehicle tempVehicle = new Vehicle(type, arrayPosition[2], tempDate); 
                                                                           
            return tempVehicle;
        }
    }
}



       