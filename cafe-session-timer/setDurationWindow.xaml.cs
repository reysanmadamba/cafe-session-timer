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

namespace cafe_session_timer
{
    /// <summary>
    /// Interaction logic for setDurationWindow.xaml
    /// </summary>
    public partial class SetDurationWindow : Window
    {
        public int Minutes { get; private set; }
        public SetDurationWindow(int currentMinutes)
        {
            InitializeComponent();

            for (int i = 0; i < DurationCombo.Items.Count; i++)
            {
                if (DurationCombo.Items[i] is ComboBoxItem item 
                    && int.TryParse(item.Tag?.ToString(), out int mins)
                    && mins == currentMinutes)
                {
                    DurationCombo.SelectedIndex = i;
                    break;
                }
                    
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (DurationCombo.SelectedItem is ComboBoxItem selected
                && int.TryParse(selected.Tag.ToString(), out var mins))
            {
                Minutes = mins;
                DialogResult = true;
                Close();
            }

            else
            {
                MessageBox.Show("Please select a duration.", "Invalid Selection",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
