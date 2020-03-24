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

namespace DateToWeek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int week = 0;
        int year = 0;

        DateTime date = DateTime.MinValue;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WeekTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                week = Int32.Parse(this.WeekTextBox.Text);
            } catch (FormatException)
            {
                WeekTextBox.Text = "1";
            }

            if (week < 1 || week > 54)
            {
                WeekTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                WeekTextBox.BorderBrush = Brushes.Gray;
            }


            if (DateTextbox == null)
            {
                return;
            }
            date = new DateTime(year, 1, 1);
            date = date.AddDays((7 * week) - 7);
            DateTextbox.Text = date.Date.ToString();
        }

        private void YearTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                year = Int32.Parse(this.YearTextbox.Text);
            }
            catch (FormatException)
            {
                YearTextbox.Text = "2000";
            }

            if (year < 1950 || year > 2100)
            {
                YearTextbox.BorderBrush = Brushes.Red;
            }
            else
            {
                YearTextbox.BorderBrush = Brushes.Gray;
            }

            date = new DateTime(year, 1, 1);
            date = date.AddDays((7 * week) - 7);

            if (DateTextbox == null)
            {
                return;
            }

            DateTextbox.Text = date.Date.ToString();
        }

 
    }
}
