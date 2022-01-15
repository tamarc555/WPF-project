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
    /// Interaction logic for delieveryPage.xaml
    /// </summary>
    public partial class delieveryPage : Page
    {
        private BlApi.IBL bl = new BL.BL();

        public delieveryPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.getCustomer(int.Parse(IDtextBox.Text));
                new customerWindow(bl, int.Parse(IDtextBox.Text)).ShowDialog();
                
                //customerWindow(bl, int.Parse(IDtextBox.Text));

                //new listOfParcel(bl, MainWindow.manager).ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("הלקוח לא שמור במערכת");
            }
        }
    }
}
