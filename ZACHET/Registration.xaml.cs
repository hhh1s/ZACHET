using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZACHET
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            
    }
        private void timerTick(object sender, EventArgs e)
        {
            Log.BorderBrush = new SolidColorBrush(Colors.Black);
            Email.BorderBrush = new SolidColorBrush(Colors.Black);
            Pass.BorderBrush= new SolidColorBrush(Colors.Black);
            Pass2.BorderBrush = new SolidColorBrush(Colors.Black);
            Error.Text = "";
        }
        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            var mail = Email.Text;
            var login = Log.Text;
            var password = Pass.Text;
            var password2 = Pass2.Text;
            var context = new AppDbContext();
            bool check = false;
            bool checkNum = false;
            bool checkEmail = false;

            
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
            

            Error.Text = "";



            if (mail != "Введите почту")
            {
                if (mail.Contains("@"))
                {
                    string[] words = mail.Split("@");

                    if (Regex.IsMatch(words[0], "^[a-zA-Z0-9]*$"))
                    {
                        checkEmail = true;
                    }
                    else 
                    {
                        Email.BorderBrush = new SolidColorBrush(Colors.Red);
                        Error.Text += " Почта содержит только буквы Английского алфовита и цыфрвы ";
                       
                    }
                    if (words[1].Contains("."))
                    {
                        string[] latters = words[1].Split(".");
                        if (latters[1].Length >= 2 )
                        {
                            if (checkEmail == true)
                            {
                                var userAdd1 = context.Users.SingleOrDefault(x => x.Email == Email.Text);
                                if (userAdd1 == null)
                                {
                                    check = true;
                                }
                                else
                                {
                                    Email.BorderBrush = new SolidColorBrush(Colors.Red);
                                    Error.Text += "\n Почта уже зарегестрированна";
                                }
                            }
                        }
                        else
                        {
                            Email.BorderBrush = new SolidColorBrush(Colors.Red);
                            Error.Text += "\n Почта не содерижит .com, .ru и тд";
                            
                        }
                    }
                    else
                    {
                        Email.BorderBrush = new SolidColorBrush(Colors.Red);
                        Error.Text += "\n Почта не содерижит .com, .ru и тд";
                        
                    }
                }
                else
                {
                    Email.BorderBrush = new SolidColorBrush(Colors.Red);
                    Error.Text += "\n Почта не содерижит '@' ";
                  
                }
            }
             
            foreach (var symbols in password)
            {
                if (char.IsDigit(symbols))
                {
                    checkNum = true;
                    break;
                }
            }

            if (password != "Введите пароль" && password2 != "Повторите пароль")
            {
                if (password == password2)
                {
                    if (password.Length >= 6)
                    {
                        if (checkNum == true)
                        {
                            if (password.Contains("!") || password.Contains("@") || password.Contains("#") || password.Contains("$") || password.Contains("*") || password.Contains("&") || password.Contains("%"))
                            {
                                var userAdd = context.Users.SingleOrDefault(x => x.Login == Log.Text);
                                if (userAdd is null && check == true)
                                {
                                    var user = new User { Login = login, Password = password, Email = mail };
                                    context.Users.Add(user);
                                    context.SaveChanges();
                                    MessageBox.Show("Добро пожаловать писят два, москва москва");
                                    this.Hide();
                                    MainWindow mainWindow2 = new MainWindow();
                                    mainWindow2.Show();
                                }
                                else
                                {
                                    Log.BorderBrush = new SolidColorBrush(Colors.Red);
                                    Error.Text += "\n Такой пользователь уже зарегиcтрирован";
                                    
                                }
                            }
                            else
                            {
                                Pass.BorderBrush = new SolidColorBrush(Colors.Red);
                                Pass2.BorderBrush = new SolidColorBrush(Colors.Red);
                                Error.Text += "\n Пароль должен содержать специальные символы !,@,#,$,*,&,%";
                                
                            }
                        }
                        else
                        {
                            Pass.BorderBrush = new SolidColorBrush(Colors.Red);
                            Pass2.BorderBrush = new SolidColorBrush(Colors.Red);
                            Error.Text += "\n Пароль должен содержать цирфы";
                            
                        }
                    }
                    else
                    {
                        Pass.BorderBrush = new SolidColorBrush(Colors.Red);
                        Pass2.BorderBrush = new SolidColorBrush(Colors.Red);
                        Error.Text += "\n Пароль должен содержать 6 или более символов";
                        
                    }
                }
                else
                {
                    Pass.BorderBrush = new SolidColorBrush(Colors.Red);
                    Pass2.BorderBrush = new SolidColorBrush(Colors.Red);
                    Error.Text += "\n Пароли не одинаковые";
                  

                }
                
            }
        }

        private void Pass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Pass.Text == "Введите пароль")
            {
                Pass.Text = "";
            }

        }

        private void Pass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Pass.Text))
            {
                Pass.Text = "Введите пароль";
            }
        }
        private void Log_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Log.Text == "Введите логин")
            {
                Log.Text = "";
            }

        }

        private void Log_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Log.Text))
            {
                Log.Text = "Введите логин";
            }
           
        }

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Email.Text == "Введите почту")
            {
                Email.Text = "";
            }
            
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Email.Text))
            {
                Email.Text = "Введите почту";
            }
            
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Pass2.Text == "Повторите пароль")
            {
                Pass2.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(Pass2.Text))
            {
                Pass2.Text = "Повторите пароль";
            }
        }
    }
}

