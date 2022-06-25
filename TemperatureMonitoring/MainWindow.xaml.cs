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
using System.IO;

namespace TemperatureMonitoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FishConditionData? fishConditionData;
        public MainWindow()
        {
            InitializeComponent();
            tbMaxTempSummary.Visibility = Visibility.Hidden;
            tbMinTempSummary.Visibility = Visibility.Hidden;
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
                int? minTemp;
                int? minTempDuration;

                if (tbMinTemp.Text == "")
                {
                    minTemp = null;
                    minTempDuration = null;
                }
                else
                {
                    minTemp = int.Parse(tbMinTemp.Text);
                    minTempDuration = int.Parse(tbMinTempDuration.Text);
                }

                Fish fish = new Fish(int.Parse(tbMaxTemp.Text), int.Parse(tbMaxTempDuration.Text),
                minTemp, minTempDuration, tbFishName.Text,
                tbTemperatureRecords.Text, ' ', DateTime.Parse(tbDate.Text));
                FishConditionData condition = fish.CheckFishCondition();

                //DataGrid settings
                ViolationsGrid.ItemsSource = condition.Violations;
                
                foreach (var column in ViolationsGrid.Columns)
                {
                    column.IsReadOnly = true;
                }
          
                //save condition to use when report will be saved.
                fishConditionData = condition;

                // summary block
                tbMaxTempSummary.Visibility = Visibility.Visible;
                tbMinTempSummary.Visibility = Visibility.Visible;

                tbMaxTempSummary.Text = "Порог максимальной температуры не превышен";
                tbMinTempSummary.Text = "Порог минимальной температуры не превышен";

                if (condition.MaxTempViolationsDuration > condition.Fish.MaxTempDuration)
                {
                    int duration = condition.MaxTempViolationsDuration;
                    tbMaxTempSummary.Text = $"Порог максимальной температуры превышен на {duration / 60} часов, {duration % 60} минут";
                }
                if (condition.MinTempViolationsDuration > condition.Fish.MinTempDuration)
                {
                    int duration = condition.MinTempViolationsDuration;
                    tbMinTempSummary.Text = $"Порог минимальной температуры превышен на {duration / 60} часов, {duration % 60} минут";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Проверьте корректность данных");
            }
            
        }

        private void SaveReport(object sender, RoutedEventArgs e)
        {
            if (fishConditionData == null)
            {
                MessageBox.Show("Сначала требуется сформировать отчет");
            }
            else
            {
                var condition = (FishConditionData)fishConditionData;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    {
                        sw.WriteLine($"Вид рыбы: {tbFishName.Text}");
                        sw.WriteLine($"Дата начала транспортировки: {tbDate.Text}");
                        sw.WriteLine($"{tbMaxTempSummary.Text} {tbMinTempSummary.Text}");

                        foreach (var record in condition.Violations)
                        {
                            sw.WriteLine(record);
                        }
                    }
                }
            }
            
        }
    }
}
