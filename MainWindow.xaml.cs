/* Program: Temperature Converter (WPF Version)
 * Author:  Kyle Chapman
 * Created: September 10, 2022
 * Updated: September 12, 2022
 * 
 * This program is a demonstration of a WPF application, with accessibility features and such.
 * Based on work from a prior class the program allows entry of a temperature value and selection
 * of a unit to convert to. After this, click convert will run validation, then calculate and
 * nicely display the converted temperature. Functionality to clear and exit is also available.
 */

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

namespace TemperatureConverterWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor for the form.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Validates input and attempts to convert an entered temperature value and display it nicely.
        /// </summary>
        private void buttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Some variable to hold the input and output values.
            double entryValue;
            double convertedValue;
            // Constants just to avoid the arbitrary numerals.
            const double TemperatureRatio = 5.0 / 9.0;
            const double TemperatureDifference = 32;

            if (double.TryParse(textBoxInputTemperature.Text, out entryValue))
            {
                // Entry is valid; get the scale to run the calculation.
                // The "??" operator is a weird one, explained here: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
                if (radioToCelsius.IsChecked ?? true)
                {
                    // The scale is celsius.
                    convertedValue = (entryValue - TemperatureDifference) * TemperatureRatio;
                    labelOutput.Content = Math.Round(entryValue, 2) + "°F is equal to " + Math.Round(convertedValue, 2) + "°C.";
                }
                else
                {
                    // The scale is fahrenheit.
                    convertedValue = (entryValue / TemperatureRatio) + TemperatureDifference;
                    labelOutput.Content = Math.Round(entryValue, 2) + "°C is equal to " + Math.Round(convertedValue, 2) + "°F.";
                }

                buttonClear.Focus();
            }
            else
            {
                // Entry is invalid; report the error.
                MessageBox.Show("The temperature value must be a valid number.", "Entry Error");

                textBoxInputTemperature.SelectAll();
                textBoxInputTemperature.Focus();
            }
        }

        /// <summary>
        /// Clears all fields.
        /// </summary>
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            textBoxInputTemperature.Clear();
            labelOutput.Content = String.Empty;
            textBoxInputTemperature.Focus();
        }

        /// <summary>
        /// Me close form.
        /// </summary>
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void buttonCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(labelOutput.Content.ToString());
        }
    }
}
