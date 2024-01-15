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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ZACHET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        
        private void timerTick(object sender, EventArgs e)
        {
            Log.BorderBrush = new SolidColorBrush(Colors.Black);
            Pass.BorderBrush = new SolidColorBrush(Colors.Black);
            Pass2.BorderBrush = new SolidColorBrush(Colors.Black);
            Error.Text = "";
        }

        private void timerTick1(object sender, EventArgs e)
        {
            ErrorBT.Visibility = Visibility.Hidden;
        }
        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();
            registration.Show();

        }
        int count = 0;
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            
            var mail = Log.Text;
            var login = Log.Text;
            Pass.Text = Pass2.Password.ToString();
            var password = Pass.Text;
            var context = new AppDbContext();

            var LogEnter = context.Users.SingleOrDefault(x => x.Login == login && x.Password == password);
            var MailEnter = context.Users.SingleOrDefault(x => x.Email == mail && x.Password == password);
            

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();

            if (count == 2)
            {
                System.Windows.Threading.DispatcherTimer timer1 = new System.Windows.Threading.DispatcherTimer();
                timer1.Tick += new EventHandler(timerTick1);
                timer1.Interval = new TimeSpan(0, 0, 15);
                timer1.Start();
                ErrorBT.Visibility = Visibility.Visible;
                MessageBox.Show("Слишком много попыток авторизации, подождите 15 секунд");
                count = 0;

                
            }

            if (login != "Введите логин/почту" && password != "")
            {
                if (LogEnter != null || MailEnter != null)
                {
                    this.Hide();
                    Client client = new Client();
                    client.Show();
                    var log1 = context.Users.SingleOrDefault(x => (x.Email == mail && x.Password == password) || (x.Login == login && x.Password == password));
                    client.Hello.Text += log1.Login;
                    
                    
                }
                else
                {
                    count++;
                    Log.BorderBrush = new SolidColorBrush(Colors.Red);
                    Pass2.BorderBrush = new SolidColorBrush(Colors.Red);
                    Pass.BorderBrush = new SolidColorBrush(Colors.Red);
                    Error.Text = "Неправильно введен логин или пароль";
                    Error.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
        }

        private void Log_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Log.Text == "Введите логин/почту")
            {
                Log.Text = "";
            }
        }

        private void Log_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Log.Text))
            {
                Log.Text = "Введите логин/почту";
            }
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            if (Pass2.Password.ToString().Length > 0)
            {
                Pass.Text = Pass2.Password.ToString();
                Pass2.Password = null;
                Pass2.Visibility = Visibility.Hidden;
                Pass.Visibility = Visibility.Visible;
            }
            else 
            {
                Pass2.Password = Pass.Text;
                Pass.Text = null;
                Pass.Visibility = Visibility.Hidden;
                Pass2.Visibility = Visibility.Visible;
            }
            
        }

        private void ErrorBT_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Подождите 15 секунд");
        }
    }
}