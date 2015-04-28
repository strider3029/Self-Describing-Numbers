using System;
using System.IO;
using System.Collections.Generic;

namespace SelfDescribingNumbers
{
    class SelfDescribingNumbers
    {
        static int Main(string[] args)
        {
            // Check the argument and file exists
            if (args.Length == 0 || !File.Exists(args[0]))
            {
                Console.Write("You failed to specify the file to read, or entered a non existant file path.\nPlease try again with the file name as the first argument.");
                return 0;
            }

            using (StreamReader reader = File.OpenText(args[0]))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (null == line) continue;

                    if ("" == line) continue;

                    PrintSelfDescribingNumbers(line);
                }
            }

            return 0;
        }

        /// <summary>
        /// A number is a self-describing number when (assuming digit positions are labeled 0 to N-1),
        /// the digit in each position is equal to the number of times that that digit appears in the number. 
        /// 
        /// For the curious, here's how 2020 is a self-describing number: Position '0' has value 2 and there is two 0 in the number.
        /// Position '1' has value 0 because there are not 1's in the number. Position '2' has value 2 and there is two 2.
        /// And the position '3' has value 0 and there are zero 3's. 
        /// </summary>
        /// <param name="sumOfParts"></param>
        static void PrintSelfDescribingNumbers(string splitNum)
        {
            bool isSelfDescribingNumber = true;

            int[] individualNumbers = new int[splitNum.Length];
            Dictionary<int, int> countOfNumbers = new Dictionary<int, int>(splitNum.Length);

            //
            // Count the number of times that a number occurs in the string
            //
            for (int index = 0; index < splitNum.Length; ++index)
            {
                individualNumbers[index] = Convert.ToInt32(splitNum.Substring(index, 1));

                if (countOfNumbers.ContainsKey(individualNumbers[index]))
                {
                    ++countOfNumbers[individualNumbers[index]];
                }
                else
                {
                    countOfNumbers.Add(individualNumbers[index], 1);
                }
            }

            //
            // Check whether the amount of occurances of a number, matches the value of the original input at the index of the number (see the summary comment for an example)
            //
            foreach (KeyValuePair<int, int> kvp in countOfNumbers)
            {
                if((individualNumbers.Length < kvp.Key) || (kvp.Key < individualNumbers.Length && individualNumbers[kvp.Key] != kvp.Value))

                if (kvp.Key <= individualNumbers.Length && individualNumbers[kvp.Key] != kvp.Value)
                {
                    isSelfDescribingNumber = false;
                    break;
                }
            }

            Console.WriteLine((isSelfDescribingNumber ? 1 : 0));
        }
    }
}
