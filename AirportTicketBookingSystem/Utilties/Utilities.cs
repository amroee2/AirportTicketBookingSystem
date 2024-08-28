using AirportTicketBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilties
{
    public class Utilities
    {
        public static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome!\nYou Are?\n1-Manager\n2-Passenger");

            int op = Convert.ToInt32(Console.ReadLine());

            switch (op)
            {
                case 0: Console.WriteLine("Invalid Option"); PrintMenu();
                    break;
            }
        }
    }

}
