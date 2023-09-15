using FinalProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FinalProject.ViewModel
{
    public class StatisticsViewModel
    {
        private const string savedTransactions = "summary.txt";
        private List<Transaction> _transactions;
        private List<Transaction> _currentTransactions;
        public StatisticsViewModel(List<Transaction> transactions)
        {
            _transactions = transactions.ToList();
        }
        public void YearlySummary(ComboBox time)
        {
            List<int> years = _transactions.Select(transaction => transaction.Date.Year)
                                           .Distinct()
                                           .ToList();
                
            time.Items.Clear();
            time.Text = "Select time";
            foreach (int year in years)
            {
                time.Items.Add(year);
            }
        }
        public void MontlySummary(ComboBox time)
        {
            var months = _transactions.Select(transaction => new { Month = transaction.Date.Month, Year = transaction.Date.Year })
                                                      .Distinct()
                                                      .ToList();
            time.Items.Clear();
            time.Text = "Select time";
            foreach(var item in months)
            {
                time.Items.Add($"{item.Month}.{item.Year}");
            }
        }
        public void DailySummary(ComboBox time)
        {
            var days = _transactions.Select(transaction => new { Day=transaction.Date.Day, Month = transaction.Date.Month, Year = transaction.Date.Year })
                                                      .Distinct()
                                                      .ToList();
            time.Items.Clear();
            time.Text = "Select time";
            foreach (var item in days)
            {
                time.Items.Add($"{item.Day}.{item.Month}.{item.Year}");
            }
        }
        public void ChangeStats(ComboBox time,TextBlock income,TextBlock expense,TextBlock result,ListBox trans)
        {
            trans.Items.Clear();
            if(time.SelectedItem != null)
            {
                int type = time.SelectedItem.ToString().Count(c => c == '.');
                if (type == 0)
                {
                    int year = int.Parse(time.SelectedItem.ToString());
                    _currentTransactions = _transactions.Where(t => t.Date.Year == year).ToList();
                }
                else if (type == 1)
                {
                    string[] monthAndYear = time.SelectedItem.ToString().Split('.');
                    int month = int.Parse(monthAndYear[0]);
                    int year = int.Parse(monthAndYear[1]);
                    _currentTransactions = _transactions.Where(t => t.Date.Year == year&&t.Date.Month==month).ToList();
                }
                else
                {
                    string[] dayAndMonthAndYear = time.SelectedItem.ToString().Split('.');
                    int day = int.Parse(dayAndMonthAndYear[0]);
                    int month = int.Parse(dayAndMonthAndYear[1]);
                    int year = int.Parse(dayAndMonthAndYear[2]);
                    _currentTransactions = _transactions.Where(t => t.Date.Year == year && t.Date.Month == month && t.Date.Day == day).ToList();
                }
                var incomes = _currentTransactions.Where(t => t is Income);
                var expenses = _currentTransactions.Where(t=> t is Expense);
                decimal results = _currentTransactions.Sum(t => t.Amount);
                result.Text = results.ToString();
                income.Text = incomes.Sum(t => t.Amount).ToString();
                expense.Text = expenses.Sum(t => t.Amount).ToString();
                foreach(Transaction t in _currentTransactions)
                {
                    trans.Items.Add(t is Income ? $"Income:{t.Category.Name} ({t.Description}) Date:{t.Date} Amount:{t.Amount}":
                                                  $"Expense:{t.Category.Name} ({t.Description}) Date:{t.Date} Amount:{t.Amount}");
                }
            }
        }
        public void AllTimeSummary(ComboBox time, TextBlock income, TextBlock expense, TextBlock result, ListBox trans)
        {
            _currentTransactions = _transactions;
            time.Items.Clear();
            time.Text = "Select time";
            trans.Items.Clear();
            var incomes = _currentTransactions.Where(t => t is Income);
            var expenses = _currentTransactions.Where(t => t is Expense);
            decimal results = _currentTransactions.Sum(t => t.Amount);
            result.Text = results.ToString();
            income.Text = incomes.Sum(t => t.Amount).ToString();
            expense.Text = expenses.Sum(t => t.Amount).ToString();
            foreach (Transaction t in _currentTransactions)
            {
                trans.Items.Add(t is Income ? $"Income:{t.Category.Name} ({t.Description}) Date:{t.Date} Amount:{t.Amount}" :
                                              $"Expense:{t.Category.Name} ({t.Description}) Date:{t.Date} Amount:{t.Amount}");
            }

        }
        public void ShowExpense(TabControl expenseTabs)
        {
            if (_currentTransactions == null)
            {
                throw new InvalidOperationException("Time is not selected!!!");
            }
            expenseTabs.Items.Clear();
            List<Expense> expenses = _currentTransactions.Where(t => t is Expense).Select(t=>t as Expense).ToList();
            List<ExpenseCategory> categories = expenses.Select(t=>t.Category).Select(t=>t as ExpenseCategory).GroupBy(t=>t.Name).Select(group=>group.First()).ToList();
            foreach(ExpenseCategory category in categories)
            {
                TabItem item = new TabItem();
                ScrollViewer scroll = new ScrollViewer();
                ListBox list = new ListBox();
                List<Expense> temp = expenses.Where(t=>t.Category.Name == category.Name).ToList();
                foreach(Expense e in temp)
                {
                    list.Items.Add($"Expense:{e.Category.Name} ({e.Description}) Date:{e.Date} Amount:{e.Amount}");
                }
                scroll.Content = list;
                item.Header = category.Name;
                item.Content = scroll;
                expenseTabs.Items.Add(item);
            }
        }
        public void ShowIncome(TabControl incomeTabs)
        {
            if (_currentTransactions == null)
            {
                throw new InvalidOperationException("Time is not selected!!!");
            }
            incomeTabs.Items.Clear();
            List<Income> incomes= _currentTransactions.Where(t => t is Income).Select(t => t as Income).ToList();
            List<IncomeCategory> categories = incomes.Select(t => t.Category).Select(t => t as IncomeCategory).GroupBy(t => t.Name).Select(group => group.First()).ToList();
            foreach (IncomeCategory category in categories)
            {
                TabItem item = new TabItem();
                ScrollViewer scroll = new ScrollViewer();
                ListBox list = new ListBox();
                List<Income> temp = incomes.Where(t => t.Category.Name == category.Name).ToList();
                foreach (Income i in temp)
                {
                    list.Items.Add($"Income:{i.Category.Name} ({i.Description}) Date:{i.Date} Amount:{i.Amount}");
                }
                scroll.Content = list;
                item.Header = category.Name;
                item.Content = scroll;
                incomeTabs.Items.Add(item);
            }
        }

        public void TransactionsToFile()
        {
            if (_currentTransactions == null||_currentTransactions.Count==0)
            {
                throw new InvalidOperationException("Time is not selected or has no transaction!!!");
            }
            else
            {
                StreamWriter sw = new StreamWriter(savedTransactions, false, System.Text.Encoding.Default);
                try
                {
                    foreach (Transaction transaction in _currentTransactions)
                    {
                        if (transaction is Expense)
                        {
                            sw.WriteLine(
                                $"Expense:{transaction.Category.Name} ({transaction.Description}) Date:{transaction.Date} Amount:{transaction.Amount}");
                        }
                        else
                        {
                            sw.WriteLine(
                                $"Income:{transaction.Category.Name} ({transaction.Description}) Date:{transaction.Date} Amount:{transaction.Amount}");
                        }
                    }
                    sw.WriteLine($"\nTransactions sum: {_currentTransactions.Select(t=>t.Amount).Sum()}");
                    MessageBox.Show($"Selected time transactions is in file!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (IOException)
                {
                    throw new IOException("Error writing to file!!!");
                }
                finally
                {
                    sw.Close();
                }
            }
        }
    }
}
