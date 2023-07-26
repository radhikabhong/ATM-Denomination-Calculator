using System;
using System.Collections.Generic;

namespace ATMCombinations
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] denominations = { 10, 50, 100 }; // Available denominations in the ATM
            int[] payouts = { 30, 50, 60, 80, 140, 230, 370, 610, 980 }; // Payout amounts to be calculated

            foreach (int payout in payouts)
            {
                if (payout <= 0)
                {
                    Console.WriteLine($"Error: Invalid payout amount {payout} EUR. Payout amount must be positive.");
                    continue;
                }

                Console.WriteLine($"For {payout} EUR, the available payout denominations would be:");
                List<List<int>> combinations = CalculateCombinations(payout, denominations, denominations.Length - 1, new List<int>());

                if (combinations.Count == 0)
                {
                    Console.WriteLine("No valid combinations from available denominations.");
                }
                else
                {
                    foreach (List<int> combination in combinations)
                    {
                        Console.WriteLine(string.Join(" + ", combination));
                    }
                }
                Console.WriteLine();
            }
        }

        // Function to calculate all possible combinations to make the given 'amount' using 'denominations'
        static List<List<int>> CalculateCombinations(int amount, int[] denominations, int currentIndex, List<int> currentCombination)
        {
            List<List<int>> result = new List<List<int>>();

            if (amount == 0)
            {
                result.Add(new List<int>(currentCombination));
                return result;
            }

            if (amount < 0 || currentIndex < 0)
                return result;

            // Case 1: Exclude the current denomination
            List<List<int>> combinationsExcludingCurrent = CalculateCombinations(amount, denominations, currentIndex - 1, currentCombination);

            // Case 2: Include the current denomination
            List<int> updatedCombination = new List<int>(currentCombination);
            updatedCombination.Add(denominations[currentIndex]);
            List<List<int>> combinationsIncludingCurrent = CalculateCombinations(amount - denominations[currentIndex], denominations, currentIndex, updatedCombination);

            result.AddRange(combinationsExcludingCurrent);
            result.AddRange(combinationsIncludingCurrent);

            return result;
        }
    }
}
