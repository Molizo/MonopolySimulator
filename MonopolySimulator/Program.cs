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
        private static List<Property> boardProperties = new List<Property>();
        private static List<string> communityCards = new List<string>();
        private static List<string> chanceCards = new List<string>();
        private static ulong requiredTurns = 0, requiredGames = 0;

        private static void Main(string[] args)
        {
            showTitle();
            readInput();
            readBoardConfig();
            while (true)
            {
                playGame(requiredTurns);
            }

            Console.WriteLine("\n\nProgram terminated...");
            Console.Read();
        }

        private static void playGame(ulong turns)
        {
            Random dice = new Random();
            int position = 1;
            for (ulong i = 0; i < turns; i++)
            {
                int dice1Results = dice.Next(1, 6);
                int dice2Results = (dice.Next(7, 12) - dice1Results) * dice.Next(400, 1300) / dice.Next(400, 1300) % dice.Next(1, 6);
                Console.WriteLine("Rolled {0} and {1}", dice1Results, dice2Results);
                if (dice1Results != dice2Results)
                {
                    position += dice1Results + dice2Results;
                    position = position % (boardProperties.Count + 1);
                    Console.WriteLine("Advanced to {0}", position);
                }
            }
        }

        private static void readBoardConfig()
        {
            Console.WriteLine();
            Console.WriteLine("Reading configuration file...");
            try
            {
                FileInfo fi = new FileInfo(@".\boardConfig.xlsx");
                using (ExcelPackage workbook = new ExcelPackage(fi))
                {
                    ExcelWorksheet worksheet = workbook.Workbook.Worksheets["Config"];

                    Console.WriteLine("\n\nProperties: ");
                    int currentRow = 2;
                    while (true)
                    {
                        Property inputProperty = new Property
                        {
                            id = worksheet.Cells[currentRow, 1].Text
                        };
                        if (inputProperty.id == "END")
                        {
                            break;
                        }

                        inputProperty.name = worksheet.Cells[currentRow, 2].Text;
                        inputProperty.type = worksheet.Cells[currentRow, 3].Text;
                        inputProperty.payout = worksheet.Cells[currentRow, 4].Text;
                        boardProperties.Add(inputProperty);
                        Console.WriteLine(inputProperty.id + " " + inputProperty.name + " " + inputProperty.type + " " + inputProperty.payout);
                        currentRow++;
                    }

                    Console.WriteLine("\n\nCommunity cards: ");
                    currentRow = 2;
                    while (true)
                    {
                        if (worksheet.Cells[currentRow, 7].Text == "END")
                        {
                            break;
                        }

                        communityCards.Add(worksheet.Cells[currentRow, 7].Text);
                        Console.WriteLine(communityCards[currentRow - 2]);
                        currentRow++;
                    }

                    Console.WriteLine("\n\nChance cards: ");
                    currentRow = 2;
                    while (true)
                    {
                        if (worksheet.Cells[currentRow, 8].Text == "END")
                        {
                            break;
                        }

                        chanceCards.Add(worksheet.Cells[currentRow, 8].Text);
                        Console.WriteLine(chanceCards[currentRow - 2]);
                        currentRow++;
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
                    requiredTurns = Convert.ToUInt64(Console.ReadLine());
                    Console.Write("Please enter the number of games: ");
                    requiredGames = Convert.ToUInt64(Console.ReadLine());
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
                           "|  player in order to find the most visited property    |" + System.Environment.NewLine +
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