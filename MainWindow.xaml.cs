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
        private Canvas plotCanvas;
        private Point startPoint;
        int x = 0;
        String password = "";
        bool isPlayerX = true;
        private List<TextBox> guessBoxes;
        private double scale = 30.0;
        private double offsetX = 0.0;
        private string secretNumber;

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


                    if (IsValidBinary(binary1) && IsValidBinary(binary2))
                    {
                        int sum = 0;
                        int decimal1 = Convert.ToInt32(binary1, 2);
                        int decimal2 = Convert.ToInt32(binary2, 2);
                        switch (Combo.SelectedValue)
                        {
                            case "+":
                                sum = decimal1 + decimal2;
                                break;
                            case "-":
                                sum = decimal1 - decimal2;
                                break;
                            case "*":
                                sum = decimal1 * decimal2;
                                break;
                            case "/":
                                sum = decimal1 / decimal2;
                                break;
                            default:

                                MessageBox.Show("Proszę wybrać działanie");
                                break;
                        }

                        label.Text = Convert.ToString(sum, 2);
                    }
                    else
                    {
                        MessageBox.Show("Proszę wpisać prawidłowe dane (np. 101)");
                    }
                }

                private bool IsValidBinary(string binary)
                {
                    foreach (char digit in binary)
                    {
                        if (digit != '0' && digit != '1')
                        {
                            return false;
                        }
                    }
                    return true;
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
                            MessageBox.Show($"{(isPlayerX ? "O" : "X")} wins!");
                            ResetGame();
                        }
                        else if (IsBoardFull())
                        {
                            MessageBox.Show("It's a draw!");
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

           
            




            }
        }
    


     