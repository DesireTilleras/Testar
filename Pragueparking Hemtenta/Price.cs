using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Pragueparking_Hemtenta
{
    class Price
 
    { // First 5 min are free. Mininum amount to debit is 40 for car and 20 for MC
        

    public int CalculatePrice(Vehicle vehicle)
    {
        
        TimeSpan span = DateTime.Now - vehicle.Arrival;
      

        int totalAmount;
        if (span.TotalMinutes < 5)
        {
            totalAmount = 0;
              
        }
        else if (span.TotalMinutes < 125)
        {
            if (vehicle.Type== Vehicle.VehicleType.MC)
            {
                totalAmount = 20;
                    
            }
            else
            {
                totalAmount = 40;
            }
        }
        else
        {
            int hours;
            if (span.TotalMinutes - 5 % 60 == 0) 
            {
                hours = (int)(span.TotalMinutes - 5) / 60; 
            }
            else
            {
                hours = (int)(span.TotalMinutes - 5) / 60; 
                                                          
                hours++;
            }
            if (vehicle.Type==Vehicle.VehicleType.MC)
            {
                totalAmount = hours * 10;
            }
            else
            {
                totalAmount = hours * 20;
            }                



            }
        return totalAmount;

    }
    
    }
}
