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
using PrintManagementSystem_Markov.Classes;

namespace PrintManagementSystem_Markov.Interface
{
    public partial class OperationsHistoryWindow : Window
    {
        private List<OperationRecord> allOperations = new List<OperationRecord>();
        private DateTime currentStartDate = DateTime.MinValue;
        private DateTime currentEndDate = DateTime.MaxValue;

        public OperationsHistoryWindow()
        {
            InitializeComponent();
            LoadOperations();
            UpdateOperationsList();
        }

        private void LoadOperations()
        {
            // Загружаем операции из статической коллекции MainWindow
            allOperations.Clear();

            foreach (var operation in MainWindow.OperationsHistory)
            {
                allOperations.Add(operation);
            }

            // Если нет операций, создаем тестовые данные для демонстрации
            if (allOperations.Count == 0)
            {
                CreateDemoOperations();
            }
        }

        private void CreateDemoOperations()
        {
            Random rnd = new Random();

            // Операции за сегодня
            for (int i = 0; i < 3; i++)
            {
                var operation = CreateRandomOperation("Иванов И.И.", DateTime.Now.AddHours(-rnd.Next(0, 24)));
                allOperations.Add(operation);
            }

            // Операции за вчера
            for (int i = 0; i < 2; i++)
            {
                var operation = CreateRandomOperation("Петров П.П.", DateTime.Now.AddDays(-1).AddHours(rnd.Next(0, 24)));
                allOperations.Add(operation);
            }

            // Операции за неделю
            for (int i = 0; i < 4; i++)
            {
                var operation = CreateRandomOperation("Сидоров С.С.", DateTime.Now.AddDays(-rnd.Next(2, 7)));
                allOperations.Add(operation);
            }
        }

        private OperationRecord CreateRandomOperation(string userName, DateTime date)
        {
            var typeOperations = TypeOperation.AllTypeOperation();
            var formats = Format.AllFormats();
            Random rnd = new Random();

            var operation = new TypeOperationsWindow
            {
                typeOperation = rnd.Next(1, 5),
                format = rnd.Next(1, 5),
                side = rnd.Next(1, 3),
                color = rnd.Next(0, 2) == 1,
                occupancy = rnd.Next(0, 2) == 1,
                count = rnd.Next(1, 50),
                price = rnd.Next(10, 1000)
            };

            operation.typeOperationText = typeOperations.Find(t => t.id == operation.typeOperation)?.name ?? "Неизвестно";
            operation.formatText = formats.Find(f => f.id == operation.format)?.format ?? "Неизвестно";
            operation.colorText = operation.color ?
                (operation.occupancy ? "ЦВ(> 50%)" : "ЦВ") :
                "Ч/Б";

            return new OperationRecord(operation, userName)
            {
                OperationDate = date
            };
        }

        private void UpdateOperationsList()
        {
            var filteredOperations = allOperations
                .Where(op => op.OperationDate >= currentStartDate && op.OperationDate <= currentEndDate)
                .OrderByDescending(op => op.OperationDate)
                .ToList();

            OperationsListView.ItemsSource = filteredOperations;
        }

        // Методы фильтрации по периодам
        private void TodayButton_Click(object sender, RoutedEventArgs e)
        {
            currentStartDate = DateTime.Today;
            currentEndDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            UpdateOperationsList();
        }

        private void YesterdayButton_Click(object sender, RoutedEventArgs e)
        {
            currentStartDate = DateTime.Today.AddDays(-1);
            currentEndDate = DateTime.Today.AddSeconds(-1);
            UpdateOperationsList();
        }

        private void WeekButton_Click(object sender, RoutedEventArgs e)
        {
            currentStartDate = DateTime.Today.AddDays(-7);
            currentEndDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            UpdateOperationsList();
        }

        private void MonthButton_Click(object sender, RoutedEventArgs e)
        {
            currentStartDate = DateTime.Today.AddDays(-30);
            currentEndDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            UpdateOperationsList();
        }

        private void QuarterButton_Click(object sender, RoutedEventArgs e)
        {
            currentStartDate = DateTime.Today.AddDays(-90);
            currentEndDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            UpdateOperationsList();
        }

        private void YearButton_Click(object sender, RoutedEventArgs e)
        {
            currentStartDate = DateTime.Today.AddDays(-365);
            currentEndDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            UpdateOperationsList();
        }

        private void AddOperationButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем основное окно для добавления операции
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void EditOperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (OperationsListView.SelectedItem is OperationRecord selectedOperation)
            {
                MessageBox.Show($"Редактирование операции от {selectedOperation.OperationDate:dd.MM.yyyy}\n" +
                              $"Пользователь: {selectedOperation.UserName}\n" +
                              $"Вид работы: {selectedOperation.typeOperationText}\n" +
                              $"Сумма: {selectedOperation.price} руб.",
                              "Редактирование операции");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите операцию для редактирования",
                              "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteOperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (OperationsListView.SelectedItem is OperationRecord selectedOperation)
            {
                var result = MessageBox.Show($"Вы действительно хотите удалить операцию от {selectedOperation.OperationDate:dd.MM.yyyy}?\n" +
                                           $"Пользователь: {selectedOperation.UserName}\n" +
                                           $"Сумма: {selectedOperation.price} руб.",
                                           "Подтверждение удаления",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Удаляем из обеих коллекций
                    allOperations.Remove(selectedOperation);
                    MainWindow.OperationsHistory.Remove(selectedOperation);
                    UpdateOperationsList();

                    MessageBox.Show("Операция удалена успешно", "Удаление",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите операцию для удаления",
                              "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void OperationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Можно добавить логику при выборе операции
        }
    }
}