namespace AspMvc.Models
{

    public class GuessingGame
    {
        public static bool GuessNumber(int guessedNumber, int correctNumber, ref int guesses, out string message)
        {
            if ((guessedNumber < 1) || (guessedNumber > 100))
            {
                message = "Invalid guess! Must be between 1 and 100.";
                return false;
            }

            guesses++;
            if (guessedNumber == correctNumber)
            {
                message = $"You have guessed the right number! [{guesses} guesses]";
                return true;
            }
            else if (guessedNumber < correctNumber)
                message = "Your guess is too low!";
            else
                message = "Your guess is too high!";
            return false;
        }
    }

}
