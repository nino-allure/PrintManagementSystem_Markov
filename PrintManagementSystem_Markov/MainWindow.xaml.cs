using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PrintManagementSystem_Markov.Classes;
using PrintManagementSystem_Markov.Interface;

namespace PrintManagementSystem_Markov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TypeOperation> typeOperationList = TypeOperation.AllTypeOperation();
        public List<Format> formatsList = Format.AllFormats();

        // Статическая коллекция для хранения истории операций
        public static ObservableCollection<OperationRecord> OperationsHistory { get; } = new ObservableCollection<OperationRecord>();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            foreach (TypeOperation items in typeOperationList)
                typeOperation.Items.Add(items.name);
            foreach (Format item in formatsList)
                formats.Items.Add(item.format);
        }

        public void CostCalculations()
        {
            float price = 0;

            if (typeOperation.SelectedIndex != -1)
            {
                if (typeOperation.SelectedItem as String == "Сканирование") price = 10;
                else if (typeOperation.SelectedItem as String == "Печать" || typeOperation.SelectedItem as String == "Копия")
                {
                    if (formats.SelectedItem as String == "A4")
                    {
                        if (TwoSides.IsChecked == false)
                        {
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 4;
                                else price = 3;
                            }
                            else price = 20;
                        }
                        else
                        {
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 6;
                                else price = 4;
                            }
                            else price = 35;
                        }
                    }
                    else if (formats.SelectedItem as String == "A3")
                    {
                        if (TwoSides.IsChecked == false)
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 8;
                            else price = 6;
                        }
                        else
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 12;
                            else price = 10;
                        }
                    }
                    else if (formats.SelectedItem as String == "A2")
                    {
                        if (Colors.IsChecked == false)
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 35;
                            else price = 50;
                        }
                        else
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 120;
                            else price = 170;
                        }
                    }
                    else if (formats.SelectedItem as String == "A1")
                    {
                        if (Colors.IsChecked == false)
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 75;
                            else price = 120;
                        }
                        else
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 140;
                            else if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 180;
                            else if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 200;
                            else if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 240;
                        }
                    }
                }
            }

            if (textBoxCount.Text.Length > 0)
                textBoxPrice.Text = (float.Parse(textBoxCount.Text) * price).ToString();
        }

        private void SelectedType(object sender, SelectionChangedEventArgs e)
        {
            if (typeOperation.SelectedIndex != -1)
            {
                if (typeOperation.SelectedItem as String == "Сканирование")
                {
                    formats.SelectedIndex = -1;
                    TwoSides.IsChecked = false;
                    Colors.IsChecked = false;
                    LotOfColor.IsChecked = false;

                    formats.IsEnabled = false;
                    TwoSides.IsEnabled = false;
                    Colors.IsEnabled = false;
                    LotOfColor.IsEnabled = false;
                }
                else if (typeOperation.SelectedItem as String == "Печать" || typeOperation.SelectedItem as String == "Копия")
                {
                    formats.IsEnabled = true;
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;
                    LotOfColor.IsEnabled = true;
                }
                if (formats.SelectedItem as String == "A4")
                {
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;
                    LotOfColor.IsEnabled = true;
                }
                else if (formats.SelectedItem as String == "A3")
                {
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;
                    LotOfColor.IsEnabled = true;
                }
                else if (formats.SelectedItem as String == "A2" || formats.SelectedItem as String == "A1")
                {
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;
                    LotOfColor.IsEnabled = true;
                }
                else if (typeOperation.SelectedItem as String == "Ризограф")
                {
                    formats.SelectedIndex = 0;
                    formats.IsEnabled = false;
                    Colors.IsEnabled = false;
                    LotOfColor.IsEnabled = false;
                }

                if (textBoxCount.Text.Length == 0)
                    textBoxCount.Text = "1";

                CostCalculations();
            }
        }

        private void SelectedFormat(object sender, SelectionChangedEventArgs e)
        {
            if (formats.SelectedItem as String == "A4")
            {
                TwoSides.IsEnabled = true;
                Colors.IsEnabled = true;
                LotOfColor.IsEnabled = false;
            }
            else if (formats.SelectedItem as String == "A3")
            {
                TwoSides.IsEnabled = true;
                Colors.IsEnabled = false;
                LotOfColor.IsEnabled = false;
            }
            else
            {
                TwoSides.IsEnabled = false;
                Colors.IsEnabled = true;
                LotOfColor.IsEnabled = true;
            }

            if (textBoxCount.Text.Length == 0)
                textBoxCount.Text = "1";

            CostCalculations();
        }

        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e) => CostCalculations();
        private void ColorsChange(object sender, RoutedEventArgs e) => CostCalculations();

        private void AddOperation(object sender, RoutedEventArgs e)
        {
            try
            {
                TypeOperationsWindow newTOW = new TypeOperationsWindow();
                newTOW.typeOperationText = typeOperation.SelectedItem as String;
                newTOW.typeOperation = typeOperationList.Find(x => x.name == newTOW.typeOperationText).id;

                if (formats.SelectedIndex != -1)
                {
                    newTOW.formatText = formats.SelectedItem as String;
                    newTOW.format = formatsList.Find(x => x.format == newTOW.formatText).id;
                }

                if (TwoSides.IsEnabled == true)
                {
                    if (TwoSides.IsChecked == false)
                        newTOW.side = 1;
                    else
                        newTOW.side = 2;
                }

                if (Colors.IsChecked == false)
                {
                    newTOW.colorText = "Ч/Б";
                    newTOW.color = false;
                }
                else
                {
                    newTOW.colorText = "ЦВ";
                    newTOW.color = true;
                }

                if (LotOfColor.IsChecked == true)
                {
                    newTOW.colorText += "(> 50%)";
                    newTOW.occupancy = true;
                }

                newTOW.count = int.Parse(textBoxCount.Text);
                newTOW.price = float.Parse(textBoxPrice.Text);
                addOperationButton.Content = "Добавить";
                Operations.Items.Add(newTOW);
                CalculationsAllPrice();

                // Сохраняем в историю
                string userName = usersName.Text;
                if (string.IsNullOrEmpty(userName))
                    userName = "Неизвестный пользователь";

                var operationRecord = new OperationRecord(newTOW, userName);
                OperationsHistory.Add(operationRecord);

                // Очищаем поля после добавления
                ClearOperationFields();

                MessageBox.Show("Операция добавлена в историю!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении операции: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearOperationFields()
        {
            typeOperation.SelectedIndex = -1;
            formats.SelectedIndex = -1;
            TwoSides.IsChecked = false;
            Colors.IsChecked = false;
            LotOfColor.IsChecked = false;
            textBoxCount.Text = "";
            textBoxPrice.Text = "";
            usersName.Text = "";
        }

        private void EditOperation(object sender, RoutedEventArgs e)
        {
            if (Operations.SelectedIndex != -1)
            {
                TypeOperationsWindow newTOW = Operations.Items[Operations.SelectedIndex] as TypeOperationsWindow;

                typeOperation.SelectedItem = typeOperationList.Find(x => x.id == newTOW.typeOperation).name;
                formats.SelectedItem = formatsList.Find(x => x.id == newTOW.format).format;

                if (newTOW.side == 1) TwoSides.IsChecked = false;
                else if (newTOW.side == 2) TwoSides.IsChecked = true;

                Colors.IsChecked = newTOW.color;

                string[] resultColor = newTOW.colorText.Split(' ');
                if (resultColor.Length == 1) LotOfColor.IsChecked = false;
                else if (resultColor.Length == 2) LotOfColor.IsChecked = true;

                textBoxCount.Text = newTOW.count.ToString();
                textBoxPrice.Text = newTOW.price.ToString();

                addOperationButton.Content = "Изменить";

                Operations.Items.Remove(Operations.Items[Operations.SelectedIndex]);
                CalculationsAllPrice();
            }
            else MessageBox.Show("Пожалуйста, выберите операцию для редактирования.");
        }

        private void DeleteOperation(object sender, RoutedEventArgs e)
        {
            if (Operations.SelectedIndex != -1)
            {
                Operations.Items.Remove(Operations.Items[Operations.SelectedIndex]);
                CalculationsAllPrice();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите операцию для удаления.");
            }
        }

        public void CalculationsAllPrice()
        {
            float allPrice = 0;

            for (int i = 0; i < Operations.Items.Count; i++)
            {
                TypeOperationsWindow newTOW = Operations.Items[i] as TypeOperationsWindow;
                allPrice += newTOW.price;
            }

            labelAllPrice.Content = "Общая сумма: " + allPrice;
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new OperationsHistoryWindow();
            historyWindow.Show();
            this.Close();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем текущие операции при возврате
            Operations.Items.Clear();
            CalculationsAllPrice();
            ClearOperationFields();
        }
    }
}