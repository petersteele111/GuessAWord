using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessAWord
{
    class Program
    {
        /// <summary>
        /// Main Method and Entry Point for the Program
        /// Instantiates an Array of random words
        /// Grabs a random word from the randomWords Array
        /// Sets userWon boolean to false by default
        /// Instantiates two Lists<char> for Asterisks and Characters (individual chars of hiddenWord)
        /// Do-While loop to display the hidden word as asterisks (*) and request a character guess from the user
        /// Passes userResponse, the Lists<char>, and hiddenWord to various methods to validate and check if a correct guess has been made
        /// Finally checks to see if the user has won, and if so exits the program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] randomWords = {"play", "hello", "foxes", "marco", "ibiza", "general", "calisthenics", "goal" };
            string hiddenWord = getWord(randomWords);
            bool userWon;
            List<char> asterisks = convertToAsterisk(hiddenWord);
            List<char> characters = explodeHiddenWord(hiddenWord);
            do
            {
                char userResponse = userGuess();
                var values = updateLists(checkUserGuess(hiddenWord, userResponse), userResponse, characters, asterisks);
                updateAsteriskWord(values.asterisks);
                userWon = checkIfUserWon(values.asterisks);
            } while (!userWon);
        }

        /// <summary>
        /// Gets a random word from the Array
        /// </summary>
        /// <param name="randomWords">Name of Array containing random words</param>
        /// <returns></returns>
        private static string getWord(string[] randomWords)
        {
            Random ranNumberGenerator = new Random();
            int randomNumber;
            randomNumber = ranNumberGenerator.Next(1, 8);

            string hiddenWord = randomWords[randomNumber];
            return hiddenWord;
        }

        /// <summary>
        /// Converts the randomly selected word to all asterisks
        /// </summary>
        /// <param name="hiddenWord">Name of String of random word selected</param>
        /// <returns></returns>
        private static List<char> convertToAsterisk(string hiddenWord)
        {

            List<char> asterisks = new List<char>();
            Console.Write("Word: ");
            for (int i = 0; i < hiddenWord.Length; i++)
            {
                asterisks.Add('*');
                Console.Write(asterisks[i]);
            }
            return asterisks;
        }

        /// <summary>
        /// Explode hiddenWord into individual characters and add them to a List<char>
        /// </summary>
        /// <param name="hiddenWord">Name of String of random hidden word selected</param>
        /// <returns></returns>
        private static List<char> explodeHiddenWord(string hiddenWord)
        {
            List<char> hiddenWordChars = new List<char>();
            hiddenWordChars.AddRange(hiddenWord);
            return hiddenWordChars;
        }

        /// <summary>
        /// Promt the user to guess a letter in the word
        /// </summary>
        /// <returns>Returns char of user response</returns>
        private static char userGuess()
        {
            Console.WriteLine();
            Console.Write("Guess a letter: ");
            char.TryParse(Console.ReadLine().ToLower().Trim(), out char userResponse);
            return userResponse;
        }

        /// <summary>
        /// Checks the User Guess against the hidden word to see 
        /// if the user selected character matches any occurences in the hidden word
        /// </summary>
        /// <param name="hiddenWord">Name of String of random hidden word selected</param>
        /// <param name="userResponse">Name of Char of user response</param>
        /// <returns>Returns boolean if user response matched any occurence in the hidden word</returns>
        private static bool checkUserGuess(string hiddenWord, char userResponse)
        {
            if (hiddenWord.Contains(userResponse))
            {
                Console.WriteLine();
                Console.WriteLine($"Yes! {userResponse} is in the word");
                Console.WriteLine();
                return true;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"Sorry. {userResponse} is not in the word");
                Console.WriteLine();
                return false;
            }
        }

        /// <summary>
        /// Updates the Characters and Asterisks List's (List<char>)
        /// Loops through to each occurence of the user guess and matches it to the characters list.
        /// Takes the index of said character and deletes an asterisk inside of the Asterisk List at that index location
        /// Add's the user response character to the same index location in the Asterisk List
        /// Takes the index of the user response character in the Characters List and deletes it from the List
        /// Add's an asterisk (*) in that same index position inside the Character List to allow the next occurance of the character to be "first"
        /// Rinse and Repeat until all asterisks inside of the Asterisk List are updated with the user response character, and all characters inside of the Character List are updated with asterisks (*) in that characters position
        /// This allows each occurence of the user repsonse char to be find inside of the Lists
        /// </summary>
        /// <param name="userGuessChecked">Name of Boolean that validates the user response char was found inside of the character List</param>
        /// <param name="userResponse">Name of Char of user response</param>
        /// <param name="characters">Name of Characters List<char> that holds each character of the hidden word</char></param>
        /// <param name="asterisks">Name of the Asterisks List<char> that holds an asterisk for each character of the hidden word</char></param>
        /// <returns>Returns a tuple of two lists. Asterisks List<char> and Characters List<char></returns>
        private static (List<char> asterisks, List<char> characters) updateLists(bool userGuessChecked, char userResponse, List<char> characters, List<char> asterisks)
        {
            if (userGuessChecked)
            {
                while (characters.Contains(userResponse))
                {
                    int index = characters.IndexOf(userResponse);
                    asterisks.RemoveAt(index);
                    asterisks.Insert(index, userResponse);
                    characters.Remove(userResponse);
                    characters.Insert(index, '*');
                }
            }
            return (asterisks, characters);
        }

        /// <summary>
        /// Updates the screen with the user response char now added at the proper index(es) while retaining asterisks (*) in spots the user response char did not match
        /// </summary>
        /// <param name="asterisks">Name of the Asterisks List<char> that holds an asterisk for each character of the hidden word<</param>
        private static void updateAsteriskWord(List<char> asterisks)
        {
            Console.Write("Word: ");
            for (int i = 0; i < asterisks.Count; i++)
            {
                Console.Write(asterisks[i]);
            }
        }

        /// <summary>
        /// Checks if the user has won the game
        /// Checks to see if the Asterisks List<char> contains any asterisks (*)
        /// If the Asterisks List<char> does not contain any asterisks (*), then the user has guessed all characters and thus guessed the word and won the game
        /// </summary>
        /// <param name="asterisks">Name of the Asterisks List<char> that holds an asterisk for each character of the hidden word<</param>
        /// <returns>Returns a Boolean to see if the user has won</returns>
        private static bool checkIfUserWon(List<char> asterisks)
        {
            if (!asterisks.Contains('*'))
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Congratulations! You have guessed the word! Thank you for playing.");
                continuePrompt();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Add's a continue prompt to the screen
        /// </summary>
        private static void continuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to Continue");
            Console.ReadKey(); 
        }
    }
}
