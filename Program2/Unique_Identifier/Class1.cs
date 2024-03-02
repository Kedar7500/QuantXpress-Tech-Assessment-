using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unique_Identifier
{
    internal class Class1
    {
  
        static void Main(string[] args)
        {
            // decalaration of variable

            string symbolName = "NIFTY";
            DateTime expiry = new DateTime(2024, 1, 31);
            int startStikePrice = 18500;
            int strikeGap = 50;


            // Part 1: Generate series of contract names
            GenerateContractSeries(symbolName, expiry, startStikePrice, strikeGap);

            // Part 2: Find closest strike instrument
            string closestContract = FindClosestStrikeInstrument(18526);// passing accordingly
            Console.WriteLine("Closest contract: " + closestContract);
        }

        // method for generating the contractSeries.
        static void GenerateContractSeries(string symbolName, DateTime expiry, int startStrikePrice, int strikeGap)
        {
            // for loop for interating upto 100
            for (int i = 0; i < 100; i++)
            {
                string optionType = "CE"; // Assuming it's always CE for this example
                int strikePrice = startStrikePrice + (i * strikeGap);
                string expiryFormatted = expiry.ToString("ddMMMyyyy").ToUpper(); // ddMMMyyyy specifying the format for date conversion
                string contractName = $"{symbolName}{expiryFormatted}{strikePrice}{optionType}";
               
                Console.WriteLine(contractName);
            }
        }

        // method to find ClosetstStrike
        static string FindClosestStrikeInstrument(int targetStrikePrice)
        {
            int startStrikePrice = 18500; // Assuming startStrikePrice is known
            int strikeGap = 50; // Assuming strikeGap is known
            int closestStrikePrice = startStrikePrice;
            int minDifference = Math.Abs(targetStrikePrice - startStrikePrice);

            for (int i = 1; i < 100; i++)
            {
                int currentStrikePrice = startStrikePrice + (i * strikeGap);
                int difference = Math.Abs(targetStrikePrice - currentStrikePrice);

                if (difference < minDifference)
                {
                    minDifference = difference;
                    closestStrikePrice = currentStrikePrice;
                }
                else
                {
                    // Break if the difference starts increasing
                    break;
                }
            }

            // decalaring loacal varibale again that method accpets only number we want to find
            string symbolName = "NIFTY"; // Assuming symbolName is known
            DateTime expiry = new DateTime(2024, 1, 31); // Assuming expiry is known
            string expiryFormatted = expiry.ToString("ddMMMyyyy").ToUpper();
            string optionType = "CE"; // Assuming it's always CE for this example
            
            return $"{symbolName}{expiryFormatted}{closestStrikePrice}{optionType}";
        }
    }

}

