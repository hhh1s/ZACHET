using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Logging;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ZACHET
{
    /// <summary>
    /// Логика взаимодействия для PersonalAcc.xaml
    /// </summary>
    public partial class PersonalAcc : Window
    {
        public PersonalAcc()
        {
            InitializeComponent();
        }
        public string loginCH = "";
        private void Mail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Mail.Text == "Введите новую почту")
            {
                Mail.Text = "";
            }
        }

        private void Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Mail.Text))
            {
                Mail.Text = "Введите новую почту";
            }
        }

        private void Pass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Pass.Text == "Введите новый пароль")
            {
                Pass.Text = "";
            }
        }

        private void Pass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Pass.Text))
            {
                Pass.Text = "Введите новый пароль";
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            
            Mail.BorderBrush = new SolidColorBrush(Colors.Black);
            Pass.BorderBrush = new SolidColorBrush(Colors.Black);
            Error.Text = "";
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            var mail = Mail.Text;
            var password = Pass.Text;
            bool check = false;
            bool checkNum = false;
            bool checkEmail = false;
            bool M = false;
            bool P = false;
            string log = login11.Text;
            string[] word = log.Split(' ');
            var context = new AppDbContext();
           

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();

            if (Mail.Text != "Введите новую почтe")
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
                        Mail.BorderBrush = new SolidColorBrush(Colors.Red);
                        Error.Text += " Почта содержит только буквы Английского алфовита и цыфрвы ";

                    }
                    if (words[1].Contains("."))
                    {
                        string[] latters = words[1].Split(".");
                        if (latters[1].Length >= 2)
                        {
                            if (checkEmail == true)
                            {
                                M = true;
                            }
                        }
                        else
                        {
                            Mail.BorderBrush = new SolidColorBrush(Colors.Red);
                            Error.Text += "\n Почта не содерижит .com, .ru и тд";

                        }
                    }
                    else
                    {
                        Mail.BorderBrush = new SolidColorBrush(Colors.Red);
                        Error.Text += "\n Почта не содерижит .com, .ru и тд";

                    }
                }
                else
                {
                    Mail.BorderBrush = new SolidColorBrush(Colors.Red);
                    Error.Text += "\n Почта не содерижит '@' ";

                }
            }

            if (M == true)
            {
                var q = context.Users.Where(x => x.Login == word[2]).AsEnumerable().Select(x => { x.Email = Mail.Text; return x; });
                foreach (var item in q)
                {
                    context.Entry(item).State = EntityState.Modified;
                }
                context.SaveChanges();
            }



            if (Pass.Text != "Введите новый пароль")
            {
                foreach (var symbols in password)
                {
                    if (char.IsDigit(symbols))
                    {
                        checkNum = true;
                        break;
                    }
                    if (password.Length >= 6)
                    {

                        if (checkNum == true)
                        {
                            if (password.Contains("!") || password.Contains("@") || password.Contains("#") || password.Contains("$") || password.Contains("*") || password.Contains("&") || password.Contains("%"))
                            {
                                P = true;
                            }
                            else
                            {
                                Pass.BorderBrush = new SolidColorBrush(Colors.Red);

                                Error.Text += "\n Пароль должен содержать специальные символы !,@,#,$,*,&,%";

                            }

                        }
                        else
                        {
                            Pass.BorderBrush = new SolidColorBrush(Colors.Red);

                            Error.Text += "\n Пароль должен содержать цирфы";

                        }
                    }
                    else
                    {
                        Pass.BorderBrush = new SolidColorBrush(Colors.Red);

                        Error.Text += "\n Пароль должен содержать 6 или более символов";

                    }
                }

            }
            if (P == true)
            {
                var q = context.Users.Where(x => x.Login == word[2]).AsEnumerable().Select(x => { x.Password = Pass.Text; return x; });
                foreach (var item in q)
                {
                    context.Entry(item).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }
    }
}




