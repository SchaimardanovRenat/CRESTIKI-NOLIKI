using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Wpf_Tic_Tac_Toe_
{
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        private Button[] buttons;
        private bool isCross;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[9] { _1, _2, _3, _4, _5, _6, _7, _8, _9 };
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
            isCross = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;

            button.Content = isCross ? "X" : "O";
            button.IsEnabled = false;

            if (CheckForWinner("X"))
            {
                EndGame("Победили крестики!");
                return;
            }

            int index = random.Next(0, 9);
            while (!buttons[index].IsEnabled)
            {
                index = random.Next(0, 9);
            }


            Button computerButton = buttons[index];
            computerButton.Content = isCross ? "O" : "X";
            computerButton.IsEnabled = false;

            if (CheckForWinner("O"))
            {
                EndGame("Победили нолики!");
                return;
            }

            if (CheckForDraw())
            {
                EndGame("Ничья!");
                return;
            }
        }

        private bool CheckForWinner(string player)
        {
            for (int row = 0; row < 3; row++)
            {
                if (buttons[row * 3].Content.ToString() == player &&
                    buttons[row * 3 + 1].Content.ToString() == player &&
                    buttons[row * 3 + 2].Content.ToString() == player)
                {
                    return true;
                }
            }

            for (int col = 0; col < 3; col++)
            {
                if (buttons[col].Content.ToString() == player &&
                    buttons[col + 3].Content.ToString() == player &&
                    buttons[col + 6].Content.ToString() == player)
                {
                    return true;
                }
            }

            if ((buttons[0].Content.ToString() == player &&
                 buttons[4].Content.ToString() == player &&
                 buttons[8].Content.ToString() == player) ||
                (buttons[2].Content.ToString() == player &&
                 buttons[4].Content.ToString() == player &&
                 buttons[6].Content.ToString() == player))
            {
                return true;
            }
            return false;
        }

        private void EndGame(string message)
        {
            ResultMessage.Text = message;

            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }

            RestartButton.IsEnabled = true;


        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            ResultMessage.Text = "";
            if (isCross == true)
            {
                isCross = false;
            }
            else
            {
                isCross = true;
            }

            foreach (Button button in buttons)
            {
                button.Content = "";
                button.IsEnabled = true;
            }

            RestartButton.IsEnabled = false;

            Random random = new Random();
            if (isCross == false)
            {
                int randomIndex = random.Next(buttons.Length);
                Button randomButton = buttons[randomIndex];
                randomButton.Content = "X";
            }

        }



        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool CheckForDraw()
        {
            int filledButtonCount = 0;

            foreach (Button button in buttons)
            {
                if (!button.IsEnabled)
                {
                    filledButtonCount++;
                }
            }

            if (filledButtonCount == 8)
            {
                Console.WriteLine("It's a draw!");
                
                return true;
            }

            return false;
        }
    }
}

