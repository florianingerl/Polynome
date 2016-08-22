using Polynoms;
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

namespace PolynomsApp
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

        private void On_ButtonCalculate_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var pec = new PolynomExpressionCalculator();
                Polynom result = pec.CalculatePolynomExpression(tbPolynomExpression.Text);
                tbResult.Text = result.ToString();
            }
            catch(Exception exception)
            {
                MessageBox.Show("Could not calculate!");
            }
        }
    }
}
