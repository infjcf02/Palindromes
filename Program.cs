using System;
using System.Collections.Generic;
using System.Linq;

namespace Palindrome
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string numberFromConsole = Console.ReadLine();
            Program program = new Program();
            List<string> list = new List<string>();
            try
            {
                int number = 0;
                if(int.TryParse(numberFromConsole, out number))
                {
                    for (int i = 0; i < number; i++)
                    {
                        list.Add(Console.ReadLine());
                    }
                }

                string Result = string.Empty;
                foreach (string sentence in list)
                {
                    Dictionary<char, int> dicPalindrome = program.createDiccionaryFromString(sentence.Replace(" ", string.Empty));
                    string palindrome = program.getFirstLetterForThePalindrome(dicPalindrome);
                    palindrome = program.getPalindrome(ref dicPalindrome, palindrome);
                    Console.WriteLine("Palindrome: " + palindrome);
                    string leftOvers = program.getLestOvers(dicPalindrome);
                    Console.WriteLine("LestOvers: " + leftOvers);
                    char ascii = program.getASCIIChar(leftOvers);
                    Result += ascii;
                }
                Console.WriteLine("RESULT: " + Result);
                Console.ReadLine();

            }
            catch
            {
                throw;
            }

        }

        public Dictionary<char, int> createDiccionaryFromString(string sentence)
        {
            Dictionary<char, int> dicPalindrome = new Dictionary<char, int>();

            foreach (char letter in sentence)
            {
                if (!dicPalindrome.ContainsKey(letter))
                {
                    dicPalindrome.Add(letter, 1);
                }
                else
                {
                    dicPalindrome[letter] = dicPalindrome[letter] + 1;
                }
            }

            return dicPalindrome;
        }

        public string getFirstLetterForThePalindrome(Dictionary<char, int> dicPalindrome)
        {
            char firstLetter = dicPalindrome.FirstOrDefault(d => d.Value == 1).Key;
            dicPalindrome[firstLetter] = 0;
            return firstLetter.ToString();
        }

        public string getPalindrome(ref Dictionary<char, int> dicPalindrome, string palindrome)
        {
            foreach (KeyValuePair<char, int> pair in dicPalindrome.Where(d => d.Value > 1).ToList())
            {
                for (int i = 0; i < (pair.Value / 2); i++)
                {
                    dicPalindrome[pair.Key] = dicPalindrome[pair.Key] - 2;
                    palindrome = pair.Key + palindrome + pair.Key;
                }
            }
            return palindrome;
        }

        public string getLestOvers(Dictionary<char, int> dicPalindrome)
        {
            string leftOvers = string.Empty;
            foreach (KeyValuePair<char, int> pair in dicPalindrome)
            {
                if (pair.Value > 0)
                {
                    leftOvers = leftOvers + pair.Key;
                }
            }
            return leftOvers;
        }

        public char getASCIIChar(string leftOvers)
        {
            int asciiNumber = leftOvers.Length + 65;
            return Convert.ToChar(asciiNumber);
        }
    }
}
