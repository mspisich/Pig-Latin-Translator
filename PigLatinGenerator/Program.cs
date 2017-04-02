using System;

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
                string input = Console.ReadLine();

                //Translate to Pig Latin and print to screen
                string pigLatin = Translate(input);
                Console.WriteLine(pigLatin);

                //Continue program?
                Console.WriteLine("\nTranslate another line? (y/n):");
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

            foreach (string englishWord in englishSentence)
            {
                wordNum++;

                //If word contains numbers or special symbols, don't translate.
                bool hasNonLetters = CheckIfNumbersOrSymbols(englishWord);
                if (hasNonLetters == true)
                {
                    translatedSentence += englishWord;
                }
                else
                {
                    string currentWord = englishWord.ToLower();
                    string translatedWord = "";
                    bool isAllCaps = CheckIfAllCaps(englishWord);
                    bool firstLetterIsCaps = false;

                    //Check if first letter in word is capitalized (only if word is not all caps!)
                    if (isAllCaps == false && englishWord[0].Equals(englishWord.ToUpper()[0]))
                    {
                        firstLetterIsCaps = true;
                    }

                    //If first character is a vowel, add "way" onto ending
                    if (currentWord[0].Equals('a') || currentWord[0].Equals('e') || currentWord[0].Equals('i') || currentWord[0].Equals('o') || currentWord[0].Equals('u'))
                    {
                        translatedWord += (currentWord + "way");

                        if (isAllCaps == true)
                        {
                            translatedWord = translatedWord.ToUpper();
                        }
                        else if (firstLetterIsCaps == true)
                        {
                            translatedWord = translatedWord.Substring(0, 1).ToUpper() + translatedWord.Substring(1);
                        }

                        translatedSentence += translatedWord;
                    }
                    else
                    {
                        //If first character is a consonant, move all consonants before the first vowel to the end of the word, then add "ay" to the end of the word

                        string consonantsBeforeFirstVowel = "";
                        bool reachedVowel = false;

                        for (int i = 0; i < currentWord.Length; i++)
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

                        //Add consonants before first vowel to the end of the word, add "ay", then add the final translated word to the translated sentence.
                        translatedWord += (consonantsBeforeFirstVowel + "ay");

                        //Recapitalize word if original word was all caps
                        if (isAllCaps == true)
                        {
                            translatedWord = translatedWord.ToUpper();
                        }
                        //Capitalize first letter if original word's first letter was capitalized
                        else if (firstLetterIsCaps == true)
                        {
                            translatedWord = translatedWord.Substring(0, 1).ToUpper() + translatedWord.Substring(1);
                        }

                        translatedSentence += translatedWord;
                    }
                }

                //Add a space between translated words
                if (englishSentence.Length > wordNum)
                {
                    translatedSentence += " ";
                }
            }

            return translatedSentence;
        }

        //Is the whole word capitalized?
        public static bool CheckIfAllCaps(string word)
        {
            //Count number of capital letters
            int capitalLetters = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i].Equals(word.ToUpper()[i]))
                {
                    capitalLetters++;
                }
            }

            //If number of capital letters equals total letters in word, return true
            if (capitalLetters == word.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Check if the word contains numbers or special symbols
        public static bool CheckIfNumbersOrSymbols(string word)
        {
            string nonLetters = "0123456789@#$%^&*()-=_+[]{}/|<>~`;:" + "\"";
            char[] nonLettersArray = nonLetters.ToCharArray();

            //Return false if no numbers or special characters are in word. IndexOfAny will return "-1" in this case.
            if (word.IndexOfAny(nonLettersArray) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Continue program?
        public static bool Continue()
        {
            string input = Console.ReadLine().ToLower();
            bool run = false;

            if (input.Equals("n"))
            {
                Console.WriteLine("Bye!");
                run = false;
            }
            else if (input.Equals("y"))
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