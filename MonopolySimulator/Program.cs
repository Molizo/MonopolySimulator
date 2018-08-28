using System;

namespace MonopolySimulator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ulong turns = 0, games = 0;
            string title = "+-------------------------------------------------------+" + System.Environment.NewLine +
                           "|  Monopoly Turn Simulator by Visoiu Mihnea Theodor     |" + System.Environment.NewLine +
                           "|                                                       |" + System.Environment.NewLine +
                           "|  This program simulates the turns made by a Monopoly  |" + System.Environment.NewLine +
                           "|  player in order to find the most visited location    |" + System.Environment.NewLine +
                           "|                                                       |" + System.Environment.NewLine +
                           "|  The developer is not associated with Hasbro Ent.     |" + System.Environment.NewLine +
                           "+-------------------------------------------------------+";
            Console.WriteLine(title);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("The board configuration file is located in the same folder");
            Console.WriteLine("as the executable. It is named boardConfig.xlsx");
            Console.WriteLine("DO NOT ALTER THE HEADERS IN boardConfig.xlsx");
            Console.WriteLine("The config file may be opened with a spreadsheet program.");
            Console.WriteLine();
            Console.WriteLine();

            bool validInput = false;
            while (validInput == false)
            {
                try
                {
                    Console.Write("Please enter the number of player turns per game: ");
                    turns = Convert.ToUInt64(Console.ReadLine());
                    Console.Write("Please enter the number of games: ");
                    games = Convert.ToUInt64(Console.ReadLine());
                    validInput = true;
                }
                catch
                {
                    Console.WriteLine("Invalid input detected. Please try again");
                    validInput = false;
                }
            }

            Console.WriteLine("Program terminated...");
            Console.Read();
        }
    }
}