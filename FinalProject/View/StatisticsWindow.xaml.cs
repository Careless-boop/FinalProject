using FinalProject.Model;
using FinalProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FinalProject.View
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow
    {
        private StatisticsViewModel _viewModel;
        public StatisticsWindow(List<Transaction> transactions)
        {
            InitializeComponent();
            _viewModel = new StatisticsViewModel(transactions);
        }

        private void YearSumButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.YearlySummary(TimeCmbBox);
        }

        private void MonthSumButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.MontlySummary(TimeCmbBox);
        }

        private void DaySumButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DailySummary(TimeCmbBox);
        }

        private void TimeCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.ChangeStats(TimeCmbBox, TotalIncomeTxtBlock, TotalExpenseTxtBlock, ResultTxtBlock, TranListBox);
        }

        private void AllTimeButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AllTimeSummary(TimeCmbBox, TotalIncomeTxtBlock, TotalExpenseTxtBlock, ResultTxtBlock, TranListBox);
        }

        private void ExpenseShowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModel.ShowExpense(TranTypesTabControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception caught: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void IncomeShowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModel.ShowIncome(TranTypesTabControl);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Exception caught: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ToFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModel.TransactionsToFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception caught: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
