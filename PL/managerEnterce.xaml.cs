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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for managerEnterce.xaml
    /// </summary>
    public partial class managerEnterce : Page
    {
        bool flag = false;
        public managerEnterce()
        {
            InitializeComponent();

        }
        private void ManagerLogin_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "Ayelet" && passwordTextBox.Password == "1234qwer")
            {
                MessageBox.Show("ברוכה הבאה איילת, הנך מוזמנת לבצע עדכונים במערכת");
                NameTextBox.Text = "";
                passwordTextBox.Password = "";
                NameTextBox.IsEnabled = false;
                passwordTextBox.IsEnabled = false;
                flag = true;
                ManagerLogin.Visibility = Visibility.Hidden;
                MainWindow.manager = true;

            }
            else if (NameTextBox.Text == "Tamar" && passwordTextBox.Password == "tamar55")
            {
                MessageBox.Show("ברוכה הבאה תמר, הנך מוזמנת לבצע עדכונים במערכת");
                NameTextBox.Text = "";
                passwordTextBox.Password = "";
                NameTextBox.IsEnabled = false;
                passwordTextBox.IsEnabled = false;
                flag = true;
                ManagerLogin.Visibility = Visibility.Hidden;
                MainWindow.manager = true;
            }
            else
            {
                MessageBox.Show("שם המשתמש או הסיסמא אינם רשומים במערכת");
                NameTextBox.Text = "";
                passwordTextBox.Password = "";
            }
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
