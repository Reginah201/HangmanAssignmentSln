using System;
using Microsoft.Maui.Controls;

namespace HangmanAssignment
{
    public partial class HangmanGamePage : ContentPage
    {
         
        private string targetWord; // Example word
        private string currentGuess; // Hidden representation of the word
        private string currentHint; //Hint for the word you should guess
         
         
        private int wrongGuesses = 0;
        private string[] hangmanImages = {
                "hang1.png", "hang2.png", "hang3.png",
                "hang4.png", "hang5.png", "hang6.png",
                "hang7.png", "hang8.png"
            };

        public HangmanGamePage()
        {
            InitializeComponent();
            currentWordLabel.Text = FormatWord(currentGuess);
            hangmanImage.Source = ImageSource.FromFile(hangmanImages[0]);
        }

         
        private void OnGuessClicked(object sender, EventArgs e)
        {
            string guess = guessEntry.Text?.ToUpper();

            if (string.IsNullOrEmpty(guess) || guess.Length != 1)
            {
                feedbackLabel.Text = "Please enter a single letter.";
                return;
            }

            if (targetWord.Contains(guess))
            {
                UpdateWordDisplay(guess);
                feedbackLabel.Text = $"Correct! You guessed: {guess}";
            }
            else
            {
                wrongGuesses++;
                feedbackLabel.Text = $"Incorrect! Wrong guesses: {wrongGuesses}";
                UpdateHangmanImage();
            }

            guessEntry.Text = ""; // Clear the input field
            CheckGameStatus();
        }

        private void UpdateWordDisplay(string guess)
        {
            char[] guessArray = currentGuess.ToCharArray();
            char[] targetArray = targetWord.ToCharArray();

            for (int i = 0; i < targetArray.Length; i++)
            {
                if (targetArray[i] == guess[0])
                {
                    guessArray[i] = guess[0];
                }
            }

            currentGuess = new string(guessArray);
            currentWordLabel.Text = FormatWord(currentGuess);
        }

        private void UpdateHangmanImage()
        {
            if (wrongGuesses < hangmanImages.Length)
            {
                hangmanImage.Source = ImageSource.FromFile(hangmanImages[wrongGuesses]);
            }
        }

        private void CheckGameStatus()
        {
            if (!currentGuess.Contains("_"))
            {
                feedbackLabel.Text = $"Congratulations! You've guessed the word: {targetWord}!";
                EndGame();
            }
            else if (wrongGuesses >= hangmanImages.Length)
            {
                feedbackLabel.Text = $"Game over! You died. The word was: {targetWord}.";
                EndGame();
            }
        }

        private void EndGame()
        {
            guessEntry.IsEnabled = false;
        }

        private string FormatWord(string word)
        {
            return string.Join(" ", word.ToCharArray());
        }
        
    }
}
