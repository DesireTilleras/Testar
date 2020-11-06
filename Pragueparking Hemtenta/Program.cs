using Pragueparking_Hemtenta;
using System;
using System.Text;
using System.Text.RegularExpressions;
namespace HuvudMeny
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode; // So you can read and write other languages to the txt document. Name of document : pragueparking.txt
            Console.InputEncoding = Encoding.Unicode;
            Menu menu = new Menu();
            menu.MainMenu();
            Console.WriteLine("Hej");
        }
    }
}
        





    