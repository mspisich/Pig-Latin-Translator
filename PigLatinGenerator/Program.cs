using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigLatinGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Pig Latin Translator!");

            //Run program until user wants to stop
            bool run = true;
            while (run)
            {
                //User input
                Console.WriteLine("\nEnter a line to be translated:");
                string input = Console.ReadLine().ToLower();

                //Translate to Pig Latin and print to screen
                string pigLatin = Translate(input);
                Console.WriteLine(pigLatin);

                //Continue program?
                run = Continue();
            }
        }

        //Translate English to Pig Latin
        public static string Translate(string input)
        {
            string translatedSentence = "";

            //Split sentence into separate words
            string[] englishSentence = input.Split(' ');

            //Track current word being worked on
            int wordNum = 0;

            foreach (string currentWord in englishSentence)
            {
                wordNum++;

                //If first character is a vowel, add "way" onto ending
                if (currentWord[0].Equals('a') || currentWord[0].Equals('e') || currentWord[0].Equals('i') || currentWord[0].Equals('o') || currentWord[0].Equals('u'))
                {
                    translatedSentence += (currentWord + "way");
                }
                else
                {
                    //If first character is a consonant, move all consonants before the first vowel to the end of the word, then add "ay" to the end of the word
                    string translatedWord = "";
                    string consonantsBeforeFirstVowel = "";
                    bool reachedVowel = false;

                    for (int i = 0; i <= (currentWord.Length - 1); i++)
                    {
                        //First vowel reached
                        if (currentWord[i].Equals('a') || currentWord[i].Equals('e') || currentWord[i].Equals('i') || currentWord[i].Equals('o') || currentWord[i].Equals('u'))
                        {
                            reachedVowel = true;
                        }

                        //Get consonants before first vowel in word
                        if (reachedVowel == false)
                        {
                            consonantsBeforeFirstVowel += currentWord[i];
                        }
                        //Get characters from first vowel onwards
                        else
                        {
                            translatedWord += currentWord[i];
                        }
                    }

                    translatedWord += consonantsBeforeFirstVowel;
                    translatedWord += "ay";
                    translatedSentence += translatedWord;
                }

                //Add a space between translated words
                if (englishSentence.Length > wordNum)
                {
                    translatedSentence += " ";
                }
            }

            return translatedSentence;
        }

        //Continue program?
        public static bool Continue()
        {
            Console.WriteLine("Translate another line? (y/n):");
            string input = Console.ReadLine().ToLower();
            bool run = false;

            if (input == "n")
            {
                Console.WriteLine("Bye!");
                run = false;
            }
            else if (input == "y")
            {
                run = true;
            }
            else
            {
                Console.WriteLine("Invalid input! Please type y/n:");
                run = Continue();
            }

            return run;
        }
    }
}