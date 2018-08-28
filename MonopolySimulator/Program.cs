using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace MonopolySimulator
{
    public class Property
    {
        public string id { set; get; }
        public string name { set; get; }
        public string type { set; get; }
        public string payout { set; get; }
    }

    internal class Program
    {
        private static readonly List<Property> boardPropertys = new List<Property>();
        private static ulong turns = 0, games = 0;

        private static void Main(string[] args)
        {
            showTitle();
            readInput();
            readBoardConfig();

            Console.WriteLine("Program terminated...");
            Console.Read();
        }

        private static void readBoardConfig()
        {
            Console.WriteLine();
            Console.WriteLine("Reading configuration file: ");
            Console.WriteLine("Properties:");
            try
            {
                FileInfo fi = new FileInfo(@".\boardConfig.xlsx");
                using (ExcelPackage workbook = new ExcelPackage(fi))
                {
                    ExcelWorksheet worksheet = workbook.Workbook.Worksheets["Properties"];
                    int currentProperty = 2;
                    while (true)
                    {
                        Property inputProperty = new Property
                        {
                            id = worksheet.Cells[currentProperty, 1].Text
                        };
                        if (inputProperty.id == "END")
                        {
                            break;
                        }

                        inputProperty.name = worksheet.Cells[currentProperty, 2].Text;
                        inputProperty.type = worksheet.Cells[currentProperty, 3].Text;
                        inputProperty.payout = worksheet.Cells[currentProperty, 4].Text;
                        Console.WriteLine(inputProperty.id + " " + inputProperty.name + " " + inputProperty.type + " " + inputProperty.payout);
                        currentProperty++;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error reading configuration file!");
            }
        }

        private static void readInput()
        {
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
        }

        private static void showTitle()
        {
            string title = "+-------------------------------------------------------+" + System.Environment.NewLine +
                           "|  Monopoly Turn Simulator by Visoiu Mihnea Theodor     |" + System.Environment.NewLine +
                           "|                                                       |" + System.Environment.NewLine +
                           "|  This program simulates the turns made by a Monopoly  |" + System.Environment.NewLine +
                           "|  player in order to find the most visited Property    |" + System.Environment.NewLine +
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
        }
    }
}