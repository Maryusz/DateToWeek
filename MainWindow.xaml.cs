using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DateToWeek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Variables
        int week = 0;
        int year = 0;

        // regulates the function of automatic parsing.
        bool isAutomaticParseChecked = true;

        DateTime date = DateTime.MinValue;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region WeekTextbox checks
        private void WeekTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                week = Int32.Parse(this.WeekTextBox.Text);
            }
            catch (FormatException)
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
            DateTextbox.Text = date.Date.ToString("dd.MM.yyyy");
        }
        #endregion

        #region YearTextbox checks
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

            // Cannot accept negative, empty values or higher than 9999
            if (year <= 0 || year > 9999)
            {
                YearTextbox.BorderBrush = Brushes.Red;
                year = 1;
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

            DateTextbox.Text = date.Date.ToString("dd.MM.yyyy");
        }
        #endregion

        #region SerialTextBox method
        private void SerialTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Retrives the text from the textbox
            string text = ((TextBox)sender).Text;

            // initialize the array of splitted results
            string[] splitted = { };

            // Checks if the text contains a slash
            if (text.Contains("/") && text.Length >= 8)
            {
                // Splits it on the slash (/) character
                splitted = text.Split("/");
                SerialResultTextbox.Text = DateOrderReverse(splitted[1]);
            }

            if (text.Contains("/"))
            {
                // Formats the data in an alternative way.
                splitted = text.Split("/");
                try
                {
                    AlternativeSerialTextbox.Text = $"C-{splitted[1]}";
                }
                catch (IndexOutOfRangeException)
                {
                    // This exception can be ignored, because it'll be cause everytime.
                }
            }



        }
        #endregion

        #region Utility methods
        /// <summary>
        /// Receive a textbox as param and copy the text to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        private void copyToClipboard(object sender)
        {
            Clipboard.SetText(((TextBox)sender).Text);
        }

        /// <summary>
        /// Receives a text within a date inside, for example 190212 and turnes it to 120219
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        private string DateOrderReverse(string rawData)
        {
            var possibleDate = rawData.Substring(0, 6);
            string[] date = { "", "", "" };

            date[0] = possibleDate.Substring(4, 2);
            date[1] = possibleDate.Substring(2, 2);
            date[2] = possibleDate.Substring(0, 2);

            return string.Join(".", date);

        }

        #endregion

        #region Button behavior
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            copyToClipboard(DateTextbox);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            copyToClipboard(SerialResultTextbox);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            copyToClipboard(AlternativeSerialTextbox);
        }
        #endregion

        #region Automatic parsing logic
        // If automaticPaste is enabled, this textbox will get automatically the data from the clipboard
        private void SerialTextbox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isAutomaticParseChecked)
            {
                SerialTextbox.Text = Clipboard.GetText();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isAutomaticParseChecked = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            isAutomaticParseChecked = false;
        }

        #endregion
    }
}
