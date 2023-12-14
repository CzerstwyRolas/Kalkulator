using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;

namespace Kalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isPlayerX = true;
   

        public MainWindow()
        {
            InitializeComponent();


            Combo.Items.Add("+");
            Combo.Items.Add("-");
            Combo.Items.Add("*");
            Combo.Items.Add("/");
        }




        private void kolko_Click(object sender, RoutedEventArgs e)
        {

            Button00.Visibility = Visibility.Visible;
            Button01.Visibility = Visibility.Visible;
            Button02.Visibility = Visibility.Visible;
            Button10.Visibility = Visibility.Visible;
            Button11.Visibility = Visibility.Visible;
            Button12.Visibility = Visibility.Visible;
            Button20.Visibility = Visibility.Visible;
            Button21.Visibility = Visibility.Visible;
            Button22.Visibility = Visibility.Visible;

            stack.Visibility = Visibility.Hidden;

        }












        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            stack.Visibility = Visibility.Visible;
            Button00.Visibility = Visibility.Hidden;
            Button01.Visibility = Visibility.Hidden;
            Button02.Visibility = Visibility.Hidden;
            Button10.Visibility = Visibility.Hidden;
            Button11.Visibility = Visibility.Hidden;
            Button12.Visibility = Visibility.Hidden;
            Button20.Visibility = Visibility.Hidden;
            Button21.Visibility = Visibility.Hidden;
            Button22.Visibility = Visibility.Hidden;

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string binary1 = binary1TextBox.Text;
            string binary2 = binary2TextBox.Text;


            if (IsBinaryNumber(binary1) && IsBinaryNumber(binary2))
            {
                int sum = 0;
                int decimal1 = Convert.ToInt32(binary1, 2);
                int decimal2 = Convert.ToInt32(binary2, 2);
                switch (Combo.SelectedValue)
                {
                    case "+":


                        label.Text = BinaryAddition(binary1, binary2);
                        break;
                    case "-":
                        label.Text=BinarySubtraction(binary1, binary2);
                        

                        break;
                    case "*":

                        label.Text = BinaryMultiplication(binary1, binary2);
                        break;
                    case "/":


                        label.Text = BinaryDivision(binary1, binary2);
                        break;
                    default:

                        MessageBox.Show("Proszę wybrać działanie");
                        break;
                }

            }
            else
            {
                MessageBox.Show("Proszę wpisać prawidłowe dane (np. 101)");
            }
        }



        private void Buttona3_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Content == "")
            {
                button.Content = isPlayerX ? "X" : "O";
                isPlayerX = !isPlayerX;

                if (CheckForWinner())
                {
                    MessageBox.Show($"{(isPlayerX ? "O" : "X")} wygrywa!");
                    ResetGame();
                }
                else if (IsBoardFull())
                {
                    MessageBox.Show("Remis!");
                    ResetGame();
                }
            }
        }

        private bool CheckForWinner()
        {
            return CheckLine(Button00, Button01, Button02) ||
                   CheckLine(Button10, Button11, Button12) ||
                   CheckLine(Button20, Button21, Button22) ||
                   CheckLine(Button00, Button10, Button20) ||
                   CheckLine(Button01, Button11, Button21) ||
                   CheckLine(Button02, Button12, Button22) ||
                   CheckLine(Button00, Button11, Button22) ||
                   CheckLine(Button02, Button11, Button20);
        }

        private bool CheckLine(Button button1, Button button2, Button button3)
        {
            return button1.Content != "" && button1.Content == button2.Content && button2.Content == button3.Content;
        }

        private bool IsBoardFull()
        {
            return Button00.Content != "" && Button01.Content != "" && Button02.Content != "" &&
                   Button10.Content != "" && Button11.Content != "" && Button12.Content != "" &&
                   Button20.Content != "" && Button21.Content != "" && Button22.Content != "";
        }

        private void ResetGame()
        {

            Button00.Content = "";
            Button01.Content = "";
            Button02.Content = "";
            Button10.Content = "";
            Button11.Content = "";
            Button12.Content = "";
            Button20.Content = "";
            Button21.Content = "";
            Button22.Content = "";
        }

        private bool IsBinaryNumber(string input)
        {
            foreach (char c in input)
            {
                if (c != '0' && c != '1' && c != '-')
                {
                    return false;
                }
            }
            return true;
        }

        private string BinaryAddition(string binary1, string binary2)
        {
            // Uzupełnienie zerami z przodu do równych długości
            int maxLength = Math.Max(binary1.Length, binary2.Length);
            binary1 = binary1.PadLeft(maxLength, '0');
            binary2 = binary2.PadLeft(maxLength, '0');

            string result = "";
            int carry = 0;

            // Dodawanie bitowe
            for (int i = maxLength - 1; i >= 0; i--)
            {
                int bit1 = binary1[i] == '-' ? 0 : binary1[i] - '0';
                int bit2 = binary2[i] == '-' ? 0 : binary2[i] - '0';

                int sum = bit1 + bit2 + carry;

                // Jeżeli suma > 1, musimy przenieść nadmiar
                if (sum > 1)
                {
                    sum -= 2;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }

                // Dodawanie wyniku na początek
                result = sum + result;
            }

            // Dodanie przeniesienia, jeśli występuje
            if (carry == 1)
            {
                result = carry + result;
            }

            return result;
        }

        private string BinarySubtraction(string binary1, string binary2)
        {
            // Uzupełnienie zerami z przodu do równych długości
            int maxLength = Math.Max(binary1.Length, binary2.Length);
            binary1 = binary1.PadLeft(maxLength, '0');
            binary2 = binary2.PadLeft(maxLength, '0');

            string result = "";
            int borrow = 0;

            // Sprawdzenie, czy binary2 > binary1
            bool isNegative = false;
            for (int i = 0; i < maxLength; i++)
            {
                int bit1 = binary1[i] == '-' ? 0 : binary1[i] - '0';
                int bit2 = binary2[i] == '-' ? 0 : binary2[i] - '0';

                if (bit2 > bit1)
                {
                    isNegative = true;
                    break;
                }
                else if (bit1 > bit2)
                {
                    break;
                }
            }

            // Odejmowanie bitowe
            for (int i = maxLength - 1; i >= 0; i--)
            {
                int bit1 = binary1[i] == '-' ? 0 : binary1[i] - '0';
                int bit2 = binary2[i] == '-' ? 0 : binary2[i] - '0';

                int subtract = bit1 - bit2 - borrow;

                // Jeżeli bit1 < bit2, musimy pożyczyć od poprzedniego bitu
                if (subtract < 0)
                {
                    subtract += 2;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }

                // Dodawanie wyniku na początek
                result = subtract + result;
            }

            // Dodanie znaku minus, jeśli binary2 > binary1
            if (isNegative)
            {
                result = "-" + result;
            }

            return result;
        }

        private string BinaryMultiplication(string binary1, string binary2)
        {
            // Konwersja do liczb całkowitych
            int num1 = Convert.ToInt32(binary1, 2);
            int num2 = Convert.ToInt32(binary2, 2);

            // Mnożenie liczb całkowitych
            int product = num1 * num2;

            // Konwersja z powrotem do postaci binarnej
            return Convert.ToString(product, 2);
        }

        private string BinaryDivision(string binary1, string binary2)
        {
            // Konwersja do liczb całkowitych
            int num1 = Convert.ToInt32(binary1, 2);
            int num2 = Convert.ToInt32(binary2, 2);

            // Dzielenie liczb całkowitych
            if (num2 != 0)
            {
                int quotient = num1 / num2;
                int remainder = num1 % num2;

                // Konwersja z powrotem do postaci binarnej
                string Binary = Convert.ToString(quotient, 2);
                string remainderBinary = Convert.ToString(remainder, 2);

                return $"Część Całkowita: {Binary}, Reszta: {remainderBinary}";
            }
            else
            {
                return "Byczqu, nie dzieli się przez 0.";
            }
        }
    }
}





   


     