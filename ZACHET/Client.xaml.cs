using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    
    public partial class Client : Window
    {
        public Client()
        { 
            InitializeComponent();
        }

        private void EnterPA_Click(object sender, RoutedEventArgs e)
        {
            PersonalAcc personalAcc = new PersonalAcc();
            this.Hide();
            personalAcc.Show();
            string log = Hello.Text;
            String[] words = log.Split(' ');
            personalAcc.login11.Text += words[1];
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Video video = new Video();
            video.Show();
        }
    }
}
