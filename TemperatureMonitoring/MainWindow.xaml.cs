using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TemperatureMonitoring.Core;

namespace TemperatureMonitoring
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

        private void OpenFileBtn_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var transportingData = FileParser.ParseFishData(openFileDialog.FileName);
                tbDate.Text = transportingData.date.ToString();
                tbTemperatureRecords.Text = String.Join(' ', transportingData.temps);
            }
        }

        private void FormReport(object sender, RoutedEventArgs e)
        {
            try
            {
                Fish fish = new Fish(int.Parse(tbMaxTemp.Text), int.Parse(tbMaxTempDuration.Text),
                int.Parse(tbMinTemp.Text), int.Parse(tbMinTempDuration.Text), tbFishName.Text,
                tbTemperatureRecords.Text, ' ', DateTime.Parse(tbDate.Text));
                FishConditionData condition = fish.CheckFishCondition();
                ViolationsGrid.ItemsSource = condition.Violations;
            }
            catch
            {
                MessageBox.Show("Проверьте корректность данных");
            }
            
        }
    }
}
