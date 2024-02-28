namespace Unique_Identifier
{
    internal class Program
    {

        public static string[] ContractNames(string symbolName, DateTime expiry, int startStrikePrice, int strikeGap, int count)
        {
            string[] contractNames = new string[count];

            for (int i = 0; i < count; i++)
            {
                int strikePrice = startStrikePrice + i * strikeGap;
                string optionType = "CE";
                string expiryDate = expiry.ToString("ddMMMyyyy").ToUpper();
                string contractName = $"{symbolName}{expiryDate}{strikePrice}{optionType}";

                contractNames[i] = contractName;
            }

            return contractNames;
        }

        static string FindClosestStrikeInstrument(string[] contractNames, int targetStrikePrice)
        {
            string closestInstrument = null;
            int minDifference = int.MaxValue;

            foreach (string contractName in contractNames)
            {
                string strikePriceString = contractName.Substring(contractName.Length - 8, 6);
                int strikePrice = int.Parse(strikePriceString);

                int difference = Math.Abs(strikePrice - targetStrikePrice);

                if (difference < minDifference)
                {
                    minDifference = difference;
                    closestInstrument = contractName;
                }
            }

            return closestInstrument;
        }

        static void Main1(string[] args)
        {
            string symbolName = "NIFTY";
            DateTime expiry = new DateTime(2024, 1, 31);
            int startStrikePrice = 18500;
            int strikeGap = 50;
            int count = 100;

            string[] contractNames = ContractNames(symbolName, expiry, startStrikePrice, strikeGap, count);

            foreach (string s in contractNames)
            {
                Console.WriteLine(s);
            }

            string result=FindClosestStrikeInstrument(contractNames, 18501);
            Console.WriteLine(result);
            Console.WriteLine(FindClosestStrikeInstrument(contractNames, 18526));
        }
    }

}