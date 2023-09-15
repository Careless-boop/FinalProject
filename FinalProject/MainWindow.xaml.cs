using System.Windows;
using FinalProject.Model;
using FinalProject.ViewModel;
using System;
using System.Linq;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        bool _expense;
        private TransactionViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new TransactionViewModel();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModel.DeserializeObjects(TranListBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception caught: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddExpButton_Click(object sender, RoutedEventArgs e)
        {
            _expense = true;
            ShowOptions();
            
        }
        private void AddIncButton_Click(object sender, RoutedEventArgs e)
        {
            _expense = false;
            ShowOptions();
            
        }
        private void ApplyTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            AddTransaction();
            HideOptions();
        }
        private void ShowStatsButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowStats(this);
        }
        private void HideOptions()
        {
            if (TransactionTypeTxtBlock.Visibility == Visibility.Visible ||
               TranTypeCmbBox.Visibility == Visibility.Visible ||
               ApplyTransactionButton.Visibility == Visibility.Visible)
            {
                TransactionTypeTxtBlock.Visibility = Visibility.Hidden;
                TranTypeCmbBox.Visibility = Visibility.Hidden;
                SumStackPanel.Visibility = Visibility.Hidden;
                DescStackPanel.Visibility = Visibility.Hidden;
                ApplyTransactionButton.Visibility = Visibility.Hidden;
                TransactionDescTxtBox.Text = string.Empty;
                TransactionSumTxtBox.Text = string.Empty;
            }
        }
        private void ShowOptions()
        {
            if (_expense)
            {
                TransactionTypeTxtBlock.Text = "Expense:";
            }
            else
            {
                TransactionTypeTxtBlock.Text = "Income:";
            }
            TransactionTypeTxtBlock.Visibility = Visibility.Visible;
            TranTypeCmbBox.Visibility = Visibility.Visible;
            if (_expense)
            {
                TranTypeCmbBox.ItemsSource = Enum.GetValues(typeof(ExpenseType)).Cast<ExpenseType>();
                TranTypeCmbBox.Text = "Expense type";
            }
            else
            {
                TranTypeCmbBox.ItemsSource = Enum.GetValues(typeof(IncomeType)).Cast<IncomeType>();
                TranTypeCmbBox.Text = "Income type";
            }
            
            SumStackPanel.Visibility = Visibility.Visible;
            DescStackPanel.Visibility = Visibility.Visible;
            ApplyTransactionButton.Visibility = Visibility.Visible;
            TranTypeCmbBox.SelectedItem = null;
            
        }
        private void AddTransaction()
        {
            try
            {
                TranListBox.Items.Add(_viewModel.AddTran(_expense,TransactionSumTxtBox.Text, TransactionDescTxtBox.Text, TranTypeCmbBox));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception caught: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RemoveTranButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModel.RemoveTran(TranListBox);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Exception caught: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
