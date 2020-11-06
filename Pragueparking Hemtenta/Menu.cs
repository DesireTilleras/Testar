using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace Pragueparking_Hemtenta
{
    class Menu
    {
        private static Vehicle[] parking = new Vehicle[200]; // 200 spots. You should be able to park 200 mc or 100 cars.

        public void MainMenu()
        {
            MethodForReadFile();
            Console.Clear();


            int userInput = 0;

            do
            {
                userInput = DisplayMenu(); // Calling the function for the different choices in the menu. 
                Console.Clear();
                switch (userInput)
                {
                    case 1:
                        Console.SetCursorPosition(40, 8);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        checkIn();
                        break;
                    case 2:
                        Console.SetCursorPosition(40, 9);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        checkOut();
                        break;
                    case 3:
                        Console.SetCursorPosition(40, 10);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        SwitchParking();
                        break;
                    case 4:
                        Console.SetCursorPosition(40, 11);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        FindVehicle();
                        break;
                    case 5:
                        ListAllSpots();
                        break;
                    case 6:
                        OptimizeParking();
                        break;
                    case 7:
                        OptimizeMCParking();
                        break;
                    case 8:
                        Visualize();
                        break;
                    case 9:
                        Console.SetCursorPosition(26, 10);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Thank you for using | OLD TIMES SQUARE PARKING GARAGE system|");
                        Console.SetCursorPosition(34, 12);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("We hope you had a nice day at work!");
                        Console.SetCursorPosition(40, 20);
                        Console.Write("Press");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" \"ENTER\" ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(" to exit ");
                        Console.ReadLine();
                        break;
                    default:
                        Console.SetCursorPosition(40, 15);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Invalid input");
                        break;
                }

            } while (userInput != 9); //The menu will keep showing if the user makes a choice that is not between numbers 1-9.
        }
        /// <summary>
        /// Main menu that contains all the functions below and switch case.
        /// </summary>
        /// <returns></returns>
        static int DisplayMenu()
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(32, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" OLD TIMES SQUARE PARKING GARAGE SYSTEM ");
            Console.SetCursorPosition(30, 6);
            Console.WriteLine("----------------------------------------------");
            Console.SetCursorPosition(55, 25);
            Console.WriteLine("System administrator:");
            Console.SetCursorPosition(55, 26);
            Console.WriteLine("Desiré Tillerås");
            Console.SetCursorPosition(55, 27);
            Console.WriteLine("Tel: 070 228 69 84");
            Console.SetCursorPosition(30, 25);
            Console.WriteLine("Address:");
            Console.SetCursorPosition(30, 26);
            Console.WriteLine("Staroměstské nám.");
            Console.SetCursorPosition(30, 27);
            Console.WriteLine("110 00 Staré Město");
            Console.SetCursorPosition(40, 9);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. New Parking.");
            Console.SetCursorPosition(40, 10);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("2. End Parking.");
            Console.SetCursorPosition(40, 11);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("3. Switch place for vehicle.");
            Console.SetCursorPosition(40, 12);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("4. Find vehicle.");
            Console.SetCursorPosition(40, 13);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("5. List all parking spots.");
            Console.SetCursorPosition(40, 14);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("6. Optimize Parking");
            Console.SetCursorPosition(40, 15);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("7. Optimize Motorcycle Parking");
            Console.SetCursorPosition(40, 16);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("8. Visualize Parking");
            Console.SetCursorPosition(40, 17);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("9. Exit");
            Console.SetCursorPosition(60, 20);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|");
            Console.SetCursorPosition(40, 20);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|   Enter choice: ");

            Console.ForegroundColor = ConsoleColor.White;

            var result = ReadInt();
            return result;
        }
        /// <summary>
        /// Function for displaying the menu
        /// </summary>
        /// <returns></returns>
        public void checkIn()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(40, 6);
            Console.WriteLine("CHECK IN NEW VEHICLE");
            Console.SetCursorPosition(36, 8);
            Console.WriteLine("-----------------------------");
            Vehicle.VehicleType vehicleType = UseVehicle();
            Console.SetCursorPosition(36, 11);
            Console.Write("Enter license plate and press \"Enter\": ");
            string regnr = Console.ReadLine().ToUpper().Trim();
            regnr = StringWash(regnr);
            DateTime startTime = DateTime.Now;
            int index = SearchForSpot(regnr);
            try
            {
                if (index < 0)
                {
                    if (vehicleType == Vehicle.VehicleType.CAR)
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (parking[i].Type == Vehicle.VehicleType.EMPTY)
                            {
                                parking[i] = new Vehicle(vehicleType, regnr, startTime);
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.SetCursorPosition(34, 10);
                                Console.Write("The car");
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(" |{0}| ", regnr);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("is placed on spot: {0}", i + 1);
                                Console.SetCursorPosition(36, 13);
                                Console.WriteLine("Time for arrival: " + startTime);

                                break;
                            }
                        }
                    }
                    if (vehicleType == Vehicle.VehicleType.MC)
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (parking[i].Type == Vehicle.VehicleType.EMPTY)
                            {
                                parking[i] = new Vehicle(vehicleType, regnr, startTime);
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.SetCursorPosition(34, 10);
                                Console.Write("The car");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write(" |{0}| ", regnr);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("is placed on spot: {0}", i + 1);
                                Console.SetCursorPosition(36, 13);
                                Console.WriteLine("Time for arrival: " + startTime);
                                break;
                            }
                            if (parking[i].Type == Vehicle.VehicleType.MC)
                            {
                                if (parking[i + 100].Type == Vehicle.VehicleType.EMPTY)
                                {
                                    Console.Clear();
                                    parking[i + 100] = new Vehicle(vehicleType, regnr, startTime);
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.SetCursorPosition(36, 10);
                                    Console.Write("The motorcycle");
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.Write(" |{0}| ", parking[i + 100].Regnr);
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.WriteLine("is placed on spot: {0}", i + 1);
                                    Console.SetCursorPosition(36, 13);
                                    Console.WriteLine("Time for arrival: " + startTime);
                                    break;
                                }
                            }
                        }
                    }

                }
                else
                {

                    Console.Clear();
                    Console.SetCursorPosition(10, 10);
                    Console.WriteLine("Sorry, there's already a vehicle parked here with this license plate! (What are the odds?)");
                    Console.SetCursorPosition(28, 12);
                    Console.WriteLine("You'll have tell the customer to park somewhere else in Prague!");
                }

                MethodForWriteFile();
                Console.WriteLine();
                Console.SetCursorPosition(32, 16);
                Console.WriteLine("-------------------------------------------------");
                Console.SetCursorPosition(40, 18);
                Console.Write("Press");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" \"ENTER\" ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("for main menu");
                Console.ReadLine();
                Console.Clear();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("The parking garage doesn't have room for this vehicle, there's a parking garage across the street.");
                Console.SetCursorPosition(40, 18);
                Console.Write("Press");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" \"ENTER\" ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("for main menu");
                Console.ReadLine();
                Console.Clear();
            }


        }
        /// <summary>
        /// Function for checking in new vehicle.
        /// </summary>
        /// <returns></returns>
        public void checkOut()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            int index;
            string regnr;
            Console.SetCursorPosition(42, 6);
            Console.WriteLine("|CHECK OUT VEHICLE|");
            Console.SetCursorPosition(34, 11);
            Console.Write("Enter license plate and press ");
            Console.Write("\"Enter\": ");
            regnr = Console.ReadLine().ToUpper().Trim();
            regnr = StringWash(regnr);
            index = SearchForSpot(regnr);
            if (index < 100 && index >= 0)
            {
                if (parking[index].Type == Vehicle.VehicleType.CAR)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(32, 10);
                    Console.Write("The vehicle");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" |{0}| ", parking[index].Regnr);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    Console.Write(" is now checked out of spot: {0}", index + 1);
                    Price price = new Price();
                    int totalPrice = price.CalculatePrice(parking[index]);

                    var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                    nfi.NumberGroupSeparator = " ";
                    string formatted = totalPrice.ToString("#,0.00", nfi);

                    TimeSpan span = DateTime.Now - parking[index].Arrival;
                    Console.SetCursorPosition(4, 13);

                    Console.WriteLine($"The vehicle has been parked for {span.Days} days ,{span.Hours} hours and {span.Minutes} minutes, which is a total parking fee of CZK {formatted} ");
                    parking[index] = new Vehicle(Vehicle.VehicleType.EMPTY, "000000", DateTime.MinValue);

                }
                if (parking[index].Type == Vehicle.VehicleType.MC)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(32, 10);
                    Console.Write("The vehicle");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" |{0}| ", parking[index].Regnr);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("is now checked out of spot: {0}", index + 1);
                    Price price = new Price();
                    int totalPrice = price.CalculatePrice(parking[index]);

                    var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                    nfi.NumberGroupSeparator = " ";
                    string formatted = totalPrice.ToString("#,0.00", nfi);

                    TimeSpan span = DateTime.Now - parking[index].Arrival;
                    Console.SetCursorPosition(4, 13);
                    Console.WriteLine($"The vehicle has been parked for {span.Days} days ,{span.Hours} hours and {span.Minutes} minutes, which is a total parking fee of CZK {formatted} ");
                    int temp = 0;
                    parking[index] = parking[temp];
                    parking[temp] = parking[index + 100];
                    parking[index + 100] = new Vehicle(Vehicle.VehicleType.EMPTY, "000000", DateTime.MinValue);

                }

            }
            else if (index >= 100)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(32, 10);
                Console.Write("The vehicle");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" |{0}| ", parking[index].Regnr);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("is checked out of spot: {0}", index - 99);
                Price price = new Price();
                int totalPrice = price.CalculatePrice(parking[index]);

                var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                nfi.NumberGroupSeparator = " ";
                string formatted = totalPrice.ToString("#,0.00", nfi);

                TimeSpan span = DateTime.Now - parking[index].Arrival;
                Console.SetCursorPosition(4, 13);
                Console.WriteLine($"The vehicle has been parked for {span.Days} days ,{span.Hours} hours and {span.Minutes} minutes, which is a total parking fee of CZK {formatted} ");
                int temp = 0;
                parking[index] = parking[temp];
                parking[index] = new Vehicle(Vehicle.VehicleType.EMPTY, "000000", DateTime.MinValue);
                parking[temp] = parking[index - 100];
            }
            else
            {
                Console.Clear();
                Console.SetCursorPosition(36, 12);
                Console.WriteLine($"The vehicle {regnr} is not parked here!");

            }
            MethodForWriteFile();
            Console.SetCursorPosition(37, 15);
            Console.WriteLine("--------------------------------");
            Console.SetCursorPosition(38, 17);
            Console.Write("Press");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" \"ENTER\" ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("for main menu");
            Console.ReadLine();
            Console.Clear();

        }
        /// <summary>
        /// Function for checking out vehicle.
        /// </summary>
        /// <returns></returns>
        public void SwitchParking()
        {
            Console.Clear();
            Console.SetCursorPosition(45, 4);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("|Switch spot|");
            Console.SetCursorPosition(65, 50);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Here are all the vehicles in the garage");
            Console.SetCursorPosition(65, 51);
            Console.WriteLine("Press enter to continue with the switch");
            Console.SetCursorPosition(67, 10);
            ListAllSpots();


            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(44, 6);
            Console.WriteLine("|Switch spot|");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(25, 9);
            Console.Write("A list of all spots will be shown again after entering license plate");
            Console.SetCursorPosition(30, 10);
            Console.Write("so you can see which ones are empty");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(40, 12);
            Console.Write("Enter license plate: ");
            Console.ResetColor();
            string regNr = Console.ReadLine();
            regNr = StringWash(regNr);
            Console.Clear();
            int index = SearchForSpot(regNr);


            if (index >= 0)
            {
                Console.SetCursorPosition(60, 50);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Here you can see which spots are empty");
                Console.SetCursorPosition(60, 51);
                Console.WriteLine("Press enter to continue with the switch");
                Console.SetCursorPosition(67, 10);
                ListAllSpots();
                Console.SetCursorPosition(32, 11);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Enter a new empty spot between 1 - 100: ");
                var spot = ReadInt();                          

                if (spot >= 0 && spot <= 100)
                {
                    if (parking[spot - 1].Type == Vehicle.VehicleType.EMPTY)
                    {
                        if (index > 100)
                        {
                            Console.Clear();

                            Vehicle temp = parking[index];
                            parking[index] = parking[spot - 1];
                            parking[spot - 1] = temp;

                            Console.SetCursorPosition(44, 6);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("|Switch spot|");
                            Console.SetCursorPosition(44, 9);
                            Console.Write("Good job!");
                            Console.SetCursorPosition(36, 11);
                            Console.Write("The vehicle");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(" |{0}| ", regNr);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("is now on spot: {0}", spot);
                            Console.SetCursorPosition(36, 15);
                            Console.Write("Press");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(" \"ENTER\" ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("for main menu");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();

                            Vehicle temp = parking[index];
                            parking[index] = parking[index + 100];
                            Vehicle temp2 = parking[spot - 1];
                            parking[spot - 1] = temp;
                            parking[index + 100] = new Vehicle(Vehicle.VehicleType.EMPTY, "000000", DateTime.MinValue);

                            Console.SetCursorPosition(44, 6);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("|Switch spot|");
                            Console.SetCursorPosition(44, 9);
                            Console.Write("Good job!");
                            Console.SetCursorPosition(36, 11);
                            Console.Write("The vehicle");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(" |{0}| ", regNr);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("is now on spot: {0}", spot);
                            Console.SetCursorPosition(36, 15);
                            Console.Write("Press");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(" \"ENTER\" ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("for main menu");
                            Console.ReadLine();
                            Console.Clear();

                        }
                    }
                    else if (parking[spot - 1].Type == Vehicle.VehicleType.MC && parking[spot + 99].Type == Vehicle.VehicleType.EMPTY)
                    {
                        if (parking[index].Type == Vehicle.VehicleType.MC)
                        {

                            if (index > 100)
                            {

                                Vehicle temp = parking[index];
                                parking[index] = parking[spot + 99];
                                parking[spot + 99] = temp;
                                temp = new Vehicle(Vehicle.VehicleType.EMPTY, "000000", DateTime.MinValue);

                                Console.SetCursorPosition(44, 6);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("|Switch spot|");
                                Console.SetCursorPosition(44, 9);
                                Console.Write("Good job!");
                                Console.SetCursorPosition(36, 11);
                                Console.Write("The vehicle");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write(" |{0}| ", regNr);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("is now on spot: {0}", spot);
                                Console.SetCursorPosition(36, 15);
                                Console.Write("Press");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write(" \"ENTER\" ");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("for main menu");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            else
                            {
                                Vehicle temp = parking[index];
                                parking[index] = parking[index + 100];
                                parking[spot + 99] = temp;
                                parking[index + 100] = new Vehicle(Vehicle.VehicleType.EMPTY, "000000", DateTime.MinValue);

                                Console.SetCursorPosition(44, 6);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("|Switch spot|");
                                Console.SetCursorPosition(44, 9);
                                Console.Write("Good job!");
                                Console.SetCursorPosition(36, 11);
                                Console.Write("The vehicle");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write(" |{0}| ", regNr);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("is now on spot: {0}", spot);
                                Console.SetCursorPosition(36, 15);
                                Console.Write("Press");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write(" \"ENTER\" ");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("for main menu");
                                Console.ReadLine();
                                Console.Clear();
                            }

                            Console.Clear();
                            Console.SetCursorPosition(44, 6);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("|Switch spot|");
                            Console.SetCursorPosition(44, 9);
                            Console.Write("Good job!");
                            Console.SetCursorPosition(36, 11);
                            Console.Write("The vehicle");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(" |{0}| ", regNr);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("is now on spot: {0}", spot);
                            Console.SetCursorPosition(36, 15);
                            Console.Write("Press");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(" \"ENTER\" ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("for main menu");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.SetCursorPosition(45, 4);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("|Switch spot|");
                            Console.SetCursorPosition(60, 7);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("As you can see to the left, the parking spot you");
                            Console.SetCursorPosition(65, 8);
                            Console.WriteLine("chose is already occupied by a motorcycle");
                            Console.SetCursorPosition(67, 10);
                            Console.WriteLine("Press enter to start over");
                            ListAllSpots();
                            SwitchParking();

                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.SetCursorPosition(44, 4);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("|Switch spot|");
                        Console.SetCursorPosition(57, 7);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("As you can see to the left,");
                        Console.SetCursorPosition(57, 8);
                        Console.WriteLine("the parking spot you chose is already occupied");
                        Console.SetCursorPosition(57, 10);
                        Console.WriteLine("Press enter to try again");
                        ListAllSpots();
                        SwitchParking();

                    }
                }
                else
                {
                    Console.Clear();
                    Console.SetCursorPosition(22, 10);
                    Console.Write("We only have spots 0-100 here in OLD TIMES SQUARE PARKING GARAGE system!");
                    Console.SetCursorPosition(38, 12);
                    Console.Write("Press enter to try again");
                    Console.ReadLine();
                    Console.Clear();
                    SwitchParking();

                }
                

            }
            else
            {

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(35, 10);
                Console.WriteLine("The vehicle is not parked here");
                Console.SetCursorPosition(28, 12);
                Console.Write("Are you sure you printed it correctly? Press enter to try again");
                Console.ReadLine();
                SwitchParking();

            }


            ReadWrite file = new ReadWrite();
            file.Write(parking);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(36, 14);
            Console.WriteLine();
            Console.SetCursorPosition(36, 15);
            Console.Write("Press");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" \"ENTER\" ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("for main menu");
            Console.Clear();
            


        }
        /// <summary>
        /// Function for switch parking spots for vehicles.
        /// </summary>
        /// <returns></returns>
        public static void ListAllSpots()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(35, 3);
            Console.WriteLine("|Listing all cars and parking spots|");
            Console.ResetColor();

            int count = 0;
            int x = 0;
            int y = 7;

            string spot1;
            string spot2;
            string spot3;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (parking[count].Type == Vehicle.VehicleType.CAR)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        spot1 = parking[count].Regnr + " " + parking[count].Type;
                        spot2 = "";
                        spot3 = "";
                        Console.WriteLine($"{count + 1} :  {spot1}  {spot2} {spot3}");
                        Console.ResetColor();
                        y++;
                    }
                    else if (parking[count].Type == Vehicle.VehicleType.MC && parking[count + 100].Type == Vehicle.VehicleType.MC)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        spot1 = parking[count].Regnr + " " + parking[count].Type;
                        spot2 = "";
                        spot3 = parking[count + 100].Regnr + " " + parking[count + 100].Type;
                        Console.WriteLine($"{count + 1} :  {spot1}  {spot2} {spot3}");
                        Console.ResetColor();
                        y++;
                    }
                    else if (parking[count].Type == Vehicle.VehicleType.MC && parking[count + 100].Type == Vehicle.VehicleType.EMPTY)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        spot1 = parking[count].Regnr + " " + parking[count].Type;
                        spot2 = "";
                        spot3 = "";
                        Console.WriteLine($"{count + 1} :  {spot1}  {spot2} {spot3}");
                        Console.ResetColor();
                        y++;

                    }
                    else
                    {
                        Console.SetCursorPosition(x, y);
                        spot1 = parking[count].Regnr + " " + parking[count].Type;
                        spot2 = "";
                        spot3 = "";
                        Console.WriteLine($"{count + 1} :  {spot1}  {spot2} {spot3}");
                        Console.ResetColor();
                        y++;
                    }
                    count++;
                }
                x = x + 30;
                y = 7;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.Write("Press");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" \"ENTER\" ");
            Console.ReadLine();
            Console.Clear();

        }
        /// <summary>
        /// Function for listing all spots.
        /// </summary>
        /// <returns></returns>
        static void FindVehicle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(46, 6);
            Console.WriteLine("|FIND VEHICLE|");
            Console.SetCursorPosition(36, 11);
            Console.Write("Enter license plate: "); // Användaren skriver in reg.nr.
            string regnr = Console.ReadLine(); // Läser av inskrivna reg.nr.
            Console.WriteLine();
            regnr = StringWash(regnr); //Tvättar strängen.
            int index = SearchForSpot(regnr);
            bool findVehicle = false;
            for (int i = 0; i < parking.Length; i++)//Loop så den går igenom alla inmatade reg.nr
            {
                if (index < 100)
                {

                    if (parking[i].Regnr == regnr)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(32, 10);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("|{0}| ", regnr);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("is located on parking spot: {0}", i + 1);
                        Console.WriteLine();
                        Console.SetCursorPosition(36, 12);
                        Price price = new Price();
                        int totalPrice = price.CalculatePrice(parking[i]);

                        var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                        nfi.NumberGroupSeparator = " ";
                        string formatted = totalPrice.ToString("#,0.00", nfi);

                        TimeSpan span = DateTime.Now - parking[i].Arrival;
                        Console.SetCursorPosition(20, 13);
                        Console.WriteLine($"The vehicle has been parked for {span.Hours} hours and {span.Minutes} minutes");
                        Console.SetCursorPosition(20, 14);
                        Console.WriteLine($"If you check out the vehicle now, the total price would be : CZK {formatted}");
                        Console.SetCursorPosition(20, 18);
                        Console.WriteLine("--------------------------------------------------------------------");
                        Console.WriteLine();
                        Console.SetCursorPosition(38, 20);
                        Console.Write("Press");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" \"ENTER\" ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("for main menu");
                        Console.ReadLine();
                        Console.Clear();

                        findVehicle = true;
                        break;
                    }
                }
                else
                {
                    if (parking[i].Regnr == regnr)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(32, 10);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("|{0}| ", regnr);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("is located on parking spot: {0}", i - 99);
                        Console.WriteLine();
                        Console.SetCursorPosition(36, 12);
                        Price price = new Price();
                        int totalPrice = price.CalculatePrice(parking[i]);

                        var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone(); // This will seperate the number so that it is easier to read.
                        nfi.NumberGroupSeparator = " ";
                        string formatted = totalPrice.ToString("#,0.00", nfi);

                        TimeSpan span = DateTime.Now - parking[i].Arrival;
                        Console.SetCursorPosition(20, 13);
                        Console.WriteLine($"The vehicle has been parked for {span.Hours} hours and {span.Minutes} minutes");
                        Console.SetCursorPosition(20, 14);
                        Console.WriteLine($"If you check out the vehicle now, the total price would be : CZK {formatted}");
                        Console.SetCursorPosition(20, 18);
                        Console.WriteLine("--------------------------------------------------------------------");
                        Console.WriteLine();
                        Console.SetCursorPosition(38, 20);
                        Console.Write("Press");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" \"ENTER\" ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("for main menu");
                        Console.ReadLine();
                        Console.Clear();

                        findVehicle = true;
                        break;
                    }
                }
            }
            if (findVehicle == false)
            {
                Console.SetCursorPosition(36, 11);
                Console.Write("The vehicle");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" |{0}| ", regnr);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("is not parked here");
                Console.SetCursorPosition(36, 17);
                Console.WriteLine("-----------------------------------");
                Console.WriteLine();
                Console.SetCursorPosition(38, 20);
                Console.Write("Press");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" \"ENTER\" ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("to try again");
                Console.ReadLine();
                FindVehicle();

            }

        }
        /// <summary>
        /// Function for finding a certain vehicle
        /// </summary>
        /// <returns></returns>
        static string StringWash(string regnr)
        {

            Regex washIt = new Regex(@"^[\p{L}\p{M}0-9\s]{1,10}$");// p{L}any kind of letter from any language.p{m} = a character intended to be combined with another character (e.g. accents, umlauts, enclosing boxes, etc.).
            regnr = Regex.Replace(regnr, "\\s+", string.Empty).Trim();
            while (!washIt.IsMatch(regnr))
            {
                Console.Clear();
                Console.SetCursorPosition(36, 10);
                Console.WriteLine("Invalid Input. Forbidden characters used.");
                Console.SetCursorPosition(40, 12);
                Console.WriteLine("Please try again");
                Console.SetCursorPosition(40, 14);
                Console.Write("License Plate: ");
                regnr = Console.ReadLine();
                regnr = Regex.Replace(regnr, "\\s+", string.Empty).Trim();
                Console.Clear();
            }
            string washed = regnr.ToUpper();
            return washed;
        }
        /// <summary>
        /// This function will make sure that the result of input regarding license plate is correct. You should be able to use other languages too. 
        /// </summary>
        /// <returns></returns>
        static Vehicle.VehicleType UseVehicle()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(36, 9);
                Console.Write("You can choose between");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" car ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" or ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(" motorcycle ");
                Console.ResetColor();
                Console.SetCursorPosition(36, 13);
                Console.Write("Answer ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\"C\"");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" for Car and ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("\"M\"");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" for Motorcycle: ");
                ConsoleKey input = Console.ReadKey().Key;
                Console.WriteLine();
                Console.Clear();
                if (input == ConsoleKey.C)
                {
                    return Vehicle.VehicleType.CAR;

                }
                else if (input == ConsoleKey.M)
                {
                    return Vehicle.VehicleType.MC;
                }
                else
                {
                    Console.WriteLine("Please type in only C or M, no other letters. Try again.");
                }

            }
        }
        /// <summary>
        /// This function asks the user if it's a MC or Car.
        /// </summary>
        /// <returns></returns>
        static int SearchForSpot(string regnr)
        {
            for (int i = 0; i < 200; i++)
            {
                if (parking[i].Regnr == regnr)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// The user can ask for a specific vehicle. It will show what time the car is parked and the price.
        /// </summary>
        /// <returns></returns>
        public void MethodForReadFile()
        {
            ReadWrite rw = new ReadWrite();

            try
            {
                parking = rw.Read();
            }
            catch (FileNotFoundException)
            {
                FileStream tempStream = File.Create("pragueparking.txt");
                tempStream.Close();

                for (int i = 0; i < 200; i++)
                {
                    parking[i] = (new Vehicle(Vehicle.VehicleType.EMPTY, "000000", DateTime.MinValue));
                }
                parking[0] = new Vehicle(Vehicle.VehicleType.CAR, "AAA111", new DateTime(2019, 10, 07));
                parking[1] = new Vehicle(Vehicle.VehicleType.CAR, "AAA222", new DateTime(2020, 10, 09));
                parking[22] = new Vehicle(Vehicle.VehicleType.CAR, "AAA333", new DateTime(2020, 10, 08));
                parking[3] = new Vehicle(Vehicle.VehicleType.MC, "AAA444", new DateTime(2020, 10, 06));
                parking[103] = new Vehicle(Vehicle.VehicleType.MC, "AAA555", new DateTime(2020, 10, 09));
                parking[4] = new Vehicle(Vehicle.VehicleType.CAR, "AAA666", new DateTime(2020, 10, 07));
                parking[5] = new Vehicle(Vehicle.VehicleType.CAR, "AAA777", new DateTime(2020, 10, 05));
                parking[6] = new Vehicle(Vehicle.VehicleType.CAR, "BBB111", new DateTime(2019, 10, 07));
                parking[37] = new Vehicle(Vehicle.VehicleType.CAR, "รถยนต์", new DateTime(2020, 10, 09)); // Other language (thai)
                parking[8] = new Vehicle(Vehicle.VehicleType.MC, "BBB333", new DateTime(2020, 10, 08));
                parking[108] = new Vehicle(Vehicle.VehicleType.MC, "BBB444", new DateTime(2020, 10, 06));
                parking[62] = new Vehicle(Vehicle.VehicleType.MC, "BBB555", new DateTime(2020, 10, 09));
                parking[10] = new Vehicle(Vehicle.VehicleType.CAR, "BBB666", new DateTime(2020, 10, 07));
                parking[9] = new Vehicle(Vehicle.VehicleType.CAR, "BBB777", new DateTime(2020, 10, 05));
                parking[88] = new Vehicle(Vehicle.VehicleType.MC, "CCC333", new DateTime(2020, 10, 08));
                parking[65] = new Vehicle(Vehicle.VehicleType.MC, "CCC888", new DateTime(2020, 10, 06));


                rw.Write(parking);


            }

        }
        /// <summary>
        /// This function read and writes to and from the saved document. And fills the parking with a few vehicles.
        /// </summary>
        /// <returns></returns>
        public void MethodForWriteFile()
        {
            ReadWrite write = new ReadWrite();
            try
            {
                write.Write(parking);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No file");
            }

        }
        /// <summary>
        ///  This function read and writes to and from the saved document. 
        /// </summary>
        /// <returns></returns>

        public void OptimizeParking()
        {
            int x = 27;
            int y = 3;

            for (int i = parking.Length - 1; i >= 0; i--)
            {
                if (parking[i].Type == Vehicle.VehicleType.CAR)
                {
                    for (int j = 0; j < parking.Length; j++)
                    {
                        if (i > j)
                        {

                            if (parking[j].Type == Vehicle.VehicleType.EMPTY)
                            {

                                Vehicle temp1 = parking[j];
                                parking[j] = parking[i];
                                parking[i] = temp1;

                                Console.SetCursorPosition(x, y);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine($"Vehicle {parking[j].Regnr} {parking[j].Type} is moved from spot {i + 1} to spot {j + 1}");
                                y++;
                                break;
                            }
                        }


                    }
                }
                if (parking[i].Type == Vehicle.VehicleType.MC)
                {
                    for (int j = 0; j < parking.Length; j++)
                    {
                        if (i > j)
                        {
                            if (parking[j].Type == Vehicle.VehicleType.EMPTY)
                            {
                                Vehicle temp1 = parking[i];
                                parking[i] = parking[j];
                                parking[j] = temp1;
                                if (j < 100 && j > 0)
                                {
                                    if (i < 100)
                                    {
                                        Console.SetCursorPosition(x, y);
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.WriteLine($"Vehicle {parking[j].Regnr} {parking[j].Type} is moved from spot {i + 1} to spot {j + 1}");
                                        y++;
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(x, y);
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.WriteLine($"Vehicle {parking[j].Regnr} {parking[j].Type} is moved from spot {i - 99} to spot {j + 1}");
                                        y++;
                                    }
                                }
                                if (j > 100)
                                {
                                    Console.SetCursorPosition(x, y);
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.WriteLine($"Vehicle {parking[j].Regnr} {parking[j].Type} is moved from spot {i + 1} to spot {j - 99}");
                                    y++;
                                }

                                break;
                            }

                            if (parking[j].Type == Vehicle.VehicleType.MC && parking[j + 100].Type == Vehicle.VehicleType.EMPTY)
                            {

                                try
                                {

                                    if (i < 100 && i < j + 100)
                                    {
                                        Vehicle temp2 = parking[i];
                                        parking[i] = parking[j + 100];
                                        parking[j + 100] = temp2;

                                        if (i > 100)
                                        {
                                            Console.SetCursorPosition(x, y);
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            Console.WriteLine($"Vehicle {parking[j].Regnr} {parking[j].Type} is moved from spot {i - 99} to spot {j + 1}");
                                            y++;
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(x, y);
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            Console.WriteLine($"Vehicle {parking[j].Regnr} {parking[j].Type} is moved from spot {i + 1} to spot {j + 1}");
                                            y++;
                                        }

                                    }
                                }
                                catch (ArgumentException)
                                {
                                    Console.WriteLine("The parking garage is already optimized!");
                                }

                                break;
                            }
                        }
                    }
                }
            }


            ReadWrite file = new ReadWrite();
            file.Write(parking);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(34, 25);
            Console.Write("The parking garage is optimized!");
            Console.SetCursorPosition(36, 27);
            Console.Write("Press");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" \"ENTER\" ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("for main menu");
            Console.ReadLine();
            Console.Clear();


        }
        /// <summary>
        /// This function will organize the garage so the vehicles will be closer to the exit.
        /// </summary>
        /// <returns></returns>

        public void OptimizeMCParking()
        {
            int x = 27;
            int y = 3;
            for (int i = parking.Length - 1; i > 0; i--)
            {

                if (parking[i].Type == Vehicle.VehicleType.MC)
                {
                    for (int j = 0; j < parking.Length; j++)
                    {
                        if (i > j)
                        {
                            if (parking[j].Type == Vehicle.VehicleType.EMPTY)
                            {
                                Vehicle temp1 = parking[j];
                                parking[j] = parking[i];
                                parking[i] = temp1;
                                if (j < 100 && j > 0)
                                {
                                    if (i < 100)
                                    {
                                        Console.SetCursorPosition(x, y);
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.WriteLine($"Vehicle {parking[j].Regnr} {parking[j].Type} is moved from spot {i + 1} to spot {j + 1}");
                                        y++;
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(x, y);
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.WriteLine($"Vehicle {parking[j].Regnr} {parking[j].Type} is moved from spot {i - 99} to spot {j + 1}");
                                        y++;
                                    }
                                }
                                if (j > 100)
                                {
                                    Console.SetCursorPosition(x, y);
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.WriteLine($"Vehicle {parking[j].Regnr} {parking[j].Type} is moved from spot {i + 1} to spot {j - 99}");
                                    y++;
                                }

                                break;
                            }

                            if (parking[j].Type == Vehicle.VehicleType.MC && parking[j + 100].Type == Vehicle.VehicleType.EMPTY)
                            {

                                try
                                {

                                    if (i < 100 && i < j + 100)
                                    {
                                        Vehicle temp2 = parking[i];
                                        parking[i] = parking[j + 100];
                                        parking[j + 100] = temp2;

                                        if (i > 100)
                                        {
                                            Console.SetCursorPosition(x, y);
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            Console.WriteLine($"Vehicle {parking[j].Regnr} {parking[j].Type} is moved from spot {i - 99} to spot {j + 1}");
                                            y++;
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(x, y);
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                            Console.WriteLine($"Vehicle {parking[j].Regnr} {parking[j].Type} is moved from spot {i + 1} to spot {j + 1}");
                                            y++;
                                        }

                                    }
                                }
                                catch (ArgumentException)
                                {
                                    Console.WriteLine("The parking garage is already optimized!");
                                }

                                break;
                            }
                        }

                    }

                }
            }

            ReadWrite file = new ReadWrite();
            file.Write(parking);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(34, 25);
            Console.Write("The parking garage is optimized!");
            Console.SetCursorPosition(36, 27);
            Console.Write("Press");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" \"ENTER\" ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("for main menu");
            Console.ReadLine();
            Console.Clear();

        }
        /// <summary>
        /// This function will organize the garage so the motorcycles will be closer to the exit and set together, if they're spread out.
        /// </summary>
        /// <returns></returns>

        static int ReadInt()
        {
            int integer;
            while (!int.TryParse(Console.ReadLine(), out integer))
            {
                Console.SetCursorPosition(25, 22);
                Console.Write("Invalid input. You have to use a number. Try again: ");
            }
            return integer;
        }
        /// <summary>
        /// Runs a TryParse-loop for integers. Prompts user to retry while it's not a number.
        /// </summary>
        /// <returns></returns>
        public void Visualize()
        {
            Console.SetCursorPosition(35, 4);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" OLD TIMES SQUARE PARKING GARAGE ");
            Console.SetCursorPosition(30, 5);
            Console.WriteLine("----------------------------------------------");


            int count = 0;
            int x = 7;
            int y = 7;

            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (parking[count].Type == Vehicle.VehicleType.CAR)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(x, y);
                        Console.Write($"{count + 1}");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" ||");
                        Console.ResetColor();
                        y++;

                    }
                    if (parking[count].Type == Vehicle.VehicleType.MC && parking[count + 100].Type == Vehicle.VehicleType.MC)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(x, y);
                        Console.Write($"{count + 1}");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" ||  || ");
                        Console.ResetColor();
                        Console.ResetColor();

                        y++;

                    }
                    if (parking[count].Type == Vehicle.VehicleType.MC && parking[count + 100].Type == Vehicle.VehicleType.EMPTY)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(x, y);
                        Console.Write($"{count + 1}");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" ||");
                        Console.ResetColor();
                        Console.ResetColor();
                        y++;

                    }
                    if (parking[count].Type == Vehicle.VehicleType.EMPTY)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(x, y);
                        Console.WriteLine($"{count + 1} - ");
                        Console.ResetColor();
                        y++;

                    }
                    count++;

                }

                x = x + 10;
                y = 7;
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(40, 20);
            Console.Write("|| = MC");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(40, 21);
            Console.Write("|| = CAR");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(40, 22);
            Console.Write("- = EMPTY SPOT");

            ReadWrite file = new ReadWrite();
            file.Write(parking);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(36, 27);
            Console.Write("Press");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" \"ENTER\" ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("for main menu");
            Console.ReadLine();
            Console.Clear();
        }
        /// <summary>
        /// Function for an overview of the garage.
        /// </summary>
        /// <returns></returns>

    }
}






